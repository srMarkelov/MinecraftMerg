using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Spine.Unity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace Core.Characters
{
    public class CharacterView : MonoBehaviour
    {
        private static string Idle = "Idle";
        private static string Attack = "Attack";
        private static string Run = "Run";
        
        [SerializeField] private Image _healthBarPlayer;
        [SerializeField] private Image _healthBarEnemy;
        [SerializeField] private GameObject _healthBarGO;
        [SerializeField] private GameObject _particlesDeath;
        [SerializeField] private GameObject _killReward;
        [SerializeField] private ParticleSystem _damageParticleSystem;
        [SerializeField] private Transform _spawnParticlesDeath;
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _animatorEnemy;
        [SerializeField] private AnimationClip _attackClip;
        [SerializeField] private AnimationClip _attackEnemyClip;
        [SerializeField] private float timeBeforeAttack;
        [SerializeField] private float timeBeforeEnemyAttack;
        [SerializeField] private float _skaleTakeDamage;
        [SerializeField] private float _skaleDefolt;
        [SerializeField] private ShellsController shellsController;
        [SerializeField] private ShellsController shellsControllerEnemy;
        [SerializeField] private bool _itsTheBoss;
        [SerializeField] private float _timerRunBoss;
        [SerializeField] private float _timerAudioRunBoss;
        [SerializeField] private Vector3 _killContainer;

        [SerializeField] private AudioSource _audioAttackMelee; 
        [SerializeField] private AudioSource _audioRunMelee; 
        [SerializeField] private AudioSource _audioStartShellsAttack; 
        [SerializeField] private AudioSource _audioFinishShellsAttack; 
        [SerializeField] private AudioSource _audioInvisible;

        public bool DistanceAttack;

        public ICharacter _Character;
        private Vector3 _targetPosition;
        private ICharacter _targetCharacter;
        private ICharacter _oldTargetCharacter;
        private List<ICharacter> _сharactersNearby = new List<ICharacter>();
        public Vector3 SpawnShellsPosition;
        private bool _thisCharacterPlayer;

        private float _currentRunBoss;
        private bool _goRunBoss;
        private bool _goRunBossAudio;
        public bool GoRunBoss => _goRunBoss;

        private float timerAttack;
        private float timerEnemyAttack;
        private float CurrentTimerAttack;

        private float _damage;
        private float _atcSpeed;
        
        public bool IsButtler;
        private bool _canAttack;
        private bool restartShellPlayer = true;
        private bool restartShellEnemy = true;
        private bool _onMoveAnimation;
        private bool restartDamageMeleePlayer = true;
        private bool restartDamageMeleeEnemy = true;
        public bool DethCharacter;
        private bool OnDeath;
        
        public bool ItsTheBoss=> _itsTheBoss;
        private const float ZOrder = 0;
        private float OldPosX;
        private float OldPosY;

        public void SetActiveTrue()
        {
            gameObject.SetActive(true);
            StartSpeedIdleAnimation();
        }

        public void SetActiveFalse()
        {
            gameObject.SetActive(false);
        }
        private void Start()
        {
            StartSpeedIdleAnimation();
            /*
            SetIdleAnimation();
            */
            SetCharacterStartingPosition();
            SetStartingTimerForAttack();
        }

        public void StartSpeedIdleAnimation()
        {
            var rnd = new Random();
            float min = 0.8f;
            float max = 1.5f;
            float randomFloatInRange = min + (float)rnd.NextDouble() * (max - min);
            if (_animator != null)
            {
                _animator.SetFloat("IdleSpeed", randomFloatInRange);
            }

            if (_animatorEnemy != null)
            {
                _animatorEnemy.SetFloat("IdleSpeed", randomFloatInRange);
            }
        }
        private void FixedUpdate()
        {
            if (_attackClip != null && shellsController != null)
            {
                SpawnShellsPosition = shellsController.transform.position;
            }

            if (_animatorEnemy && shellsControllerEnemy != null)
            {
                SpawnShellsPosition = shellsControllerEnemy.transform.position;
            }
            var position = transform.position;
            OldPosX = position.x;
            OldPosY = position.y;
        }

        private void Update()
        {
            TimerBossStartRun();
            TimerAttack();
            TakeDamageMelee();
            InstantiateShill();
            /*SetTargetCharactersForMassBossesAttacks();*/
            SetTargetPositionForShells();
        }

        public void ApplyMassAttackBosses(List<ICharacter> characters)
        {
            _сharactersNearby.Clear();
            foreach (var player in characters)
            {
                if (_Character.DamageType == DamageType.Melee)
                {
                    var distance = Vector3.Distance(transform.position, player.View.transform.position);
                    if (distance <= 1.5f)
                    {
                        /*if (player == _targetCharacter ) continue;*/
                    
                        _сharactersNearby.Add(player);
                    }
                }

                if (_Character.DamageType == DamageType.Range)
                {
                    /*if (player == _targetCharacter ) continue;*/
                    if (_сharactersNearby.Count > 3)
                    {
                        continue;
                    }
                    _сharactersNearby.Add(player);
                    if (_сharactersNearby.Count>=1 &&  shellsControllerEnemy != null)
                    {
                        shellsControllerEnemy.SetView(_сharactersNearby);
                    }
                }

                
            }
        }

        private void TimerBossStartRun()
        {
            if (_oldTargetCharacter != _targetCharacter)
            {
                _goRunBoss = false;
                _onMoveAnimation = false;
            }
            if (_onMoveAnimation)
            {
                _currentRunBoss += Time.deltaTime;
                if (_timerAudioRunBoss <= _currentRunBoss && _goRunBossAudio == false)
                {
                    if (_audioInvisible != null)
                    {
                        _goRunBossAudio = true;
                        _audioInvisible.Play();
                    }
                }
                if (_timerRunBoss <= _currentRunBoss)
                {
                    restartDamageMeleePlayer = true;
                    restartDamageMeleeEnemy = true;
                    _goRunBoss = true;
                    _goRunBossAudio = false;
                    /*_currentRunBoss = 0;*/

                    CurrentTimerAttack = 0;
                }
            }
            _oldTargetCharacter = _targetCharacter;
        }
        private void TimerAttack()
        {
            if (_animator != null)
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Run") ||
                    _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    return;
                }
            }
            if (_animatorEnemy != null)
            {
                if (_animatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Run") ||
                    _animatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    return;
                }
            }
            if (_oldTargetCharacter != null && _oldTargetCharacter != _targetCharacter)
            {
                if (_animator != null)
                    _animator.SetFloat("AttackSpeed", 0);
                
                if (_animatorEnemy != null) 
                    _animatorEnemy.SetFloat("AttackSpeed", 0);
                
                SetIdleAnimation();
            }
            if (_attackClip != null)
            {
                if (_canAttack)
                {
                    CurrentTimerAttack += Time.deltaTime;
                    if (CurrentTimerAttack >= timerAttack)
                    {
                        if (_animator != null)
                            _animator.SetFloat("AttackSpeed", _atcSpeed);
                        
                        restartDamageMeleePlayer = true;
                        restartShellPlayer = true;
                        CurrentTimerAttack = 0;
                    }
                }
            }
            if (_attackEnemyClip != null)
            {
                if (_canAttack)
                {
                    CurrentTimerAttack += Time.deltaTime;
                    if (CurrentTimerAttack >= timerEnemyAttack)
                    {
                        if (_animatorEnemy != null)
                            _animatorEnemy.SetFloat("AttackSpeed", _atcSpeed);
                        
                        restartDamageMeleeEnemy = true;
                        restartShellEnemy = true;
                        CurrentTimerAttack = 0;
                    }
                }
            }

            _oldTargetCharacter = _targetCharacter;
        }
        
        public void TakeDamageMelee()
        {
            if (_Character == null) return;
            
            if (_Character.DamageType == DamageType.Range) return;

            
            if (_attackClip != null)
            {
                if (restartDamageMeleePlayer)
                {
                    if (CurrentTimerAttack>=timeBeforeAttack)
                    {
                        restartDamageMeleePlayer = false;
                        
                        if (_audioAttackMelee != null)
                        {
                            _audioAttackMelee.pitch = UnityEngine.Random.Range(0.85f, 1.2f);
                            _audioAttackMelee.Play();
                        }
                        if (_targetCharacter != null) 
                            _targetCharacter.TakeDamage(_damage);
                        
                        if (_сharactersNearby != null)
                        {
                            foreach (var character in _сharactersNearby)
                            {
                                character.TakeDamage(_damage);
                            }
                        }
                    }
                }
            }
            if (_attackEnemyClip != null)
            {
                if (restartDamageMeleeEnemy)
                {
                    if (CurrentTimerAttack >= timeBeforeEnemyAttack)
                    {
                        restartDamageMeleeEnemy = false;
                        
                        if (_audioAttackMelee != null)
                        {
                            _audioAttackMelee.pitch = UnityEngine.Random.Range(0.85f, 1.2f);
                            _audioAttackMelee.Play();
                        }
                        if (_targetCharacter != null)
                        {
                            _targetCharacter.TakeDamage(_damage);
                        }
                        if (_сharactersNearby != null)
                        {
                            foreach (var character in _сharactersNearby)
                            {
                                character.TakeDamage(_damage);
                            }
                        }
                    }
                    
                }
            }
        }

        private bool OnRefreshAttackSpeed;
        public void RefreshAttackSpeed(float damageSpeed)
        {
            if (OnRefreshAttackSpeed) return;
            OnRefreshAttackSpeed = true;
            var atcSpeed = 1.0f+(damageSpeed/10);
            _atcSpeed = atcSpeed;
            
            if (_attackClip != null)
            {
                timeBeforeAttack = timeBeforeAttack / atcSpeed;
                timerAttack = timerAttack / atcSpeed;
                _animator.SetFloat("AttackSpeed", atcSpeed);
            }
            if (_attackEnemyClip != null)
            {
                timeBeforeEnemyAttack = timeBeforeEnemyAttack / atcSpeed;
                timerEnemyAttack = timerEnemyAttack / atcSpeed;
                if (_animatorEnemy!=null)
                {
                    _animatorEnemy.SetFloat("AttackSpeed", atcSpeed);
                }
            }
            
        }
        
        public void InstantiateShill()
        {
            if (_attackClip != null)
            {
                if (restartShellPlayer)
                {
                    if (CurrentTimerAttack>timeBeforeAttack)
                    {
                        restartShellPlayer = false;
                        if (shellsController != null)
                        {
                            shellsController.SetView(this);
                            if (_targetCharacter != null)
                            {
                                if (shellsController != null)
                                {
                                    if (_audioStartShellsAttack != null)
                                    {
                                        _audioStartShellsAttack.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                                        _audioStartShellsAttack.Play();
                                    }
                                    shellsController.InstantiateShills();
                                }
                            }
                        }
                    }
                }
            }
            
            if (_attackEnemyClip != null)
            {
                if (restartShellEnemy)
                {
                    if (CurrentTimerAttack>timeBeforeEnemyAttack)
                    {
                        restartShellEnemy = false;
                        if (shellsControllerEnemy != null)
                        {
                            shellsControllerEnemy.SetView(this);
                            if (_сharactersNearby != null && shellsControllerEnemy != null && 
                                _Character.CharacterType == CharacterType.SecondCharacterBoss ||
                                _Character.CharacterType == CharacterType.FourthCharacterBoss ||
                                _Character.CharacterType == CharacterType.SixthBossCharacter)
                            {
                                for (int i = 0; i < _сharactersNearby.Count ; i++)
                                {
                                    if (_targetCharacter != _сharactersNearby)
                                    {
                                        if (_audioStartShellsAttack != null)
                                        {
                                            _audioStartShellsAttack.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                                            _audioStartShellsAttack.Play();
                                        }
                                        shellsControllerEnemy.InstantiateShillsMass();
                                    }
                                } 
                            }
                            else if (shellsControllerEnemy != null)
                            {
                                if (_audioStartShellsAttack != null)
                                {
                                    _audioStartShellsAttack.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                                    _audioStartShellsAttack.Play();
                                }
                                shellsControllerEnemy.InstantiateShills();
                            }
                        }
                    }
                }
            }
        }
        
        public void SetTargetCharacterAndDamage(ICharacter character, float damage)
        {
            _targetCharacter = character;
            
            if (character != null)
            {
                _targetPosition = character.Position;
            }
            _damage = damage;
        }
        
        public void SetActivePlayerViewAndAnimator()
        {
            if (_animatorEnemy != null)
            {
                _animatorEnemy.gameObject.SetActive(false);
            }
            _animator.gameObject.SetActive(true);
            _healthBarPlayer.gameObject.SetActive(true);
            _healthBarEnemy.gameObject.SetActive(false);
            if (shellsControllerEnemy != null)
            {
                Destroy(shellsControllerEnemy.gameObject);
            }
            _animatorEnemy = null;
            _attackEnemyClip = null;
        }
        
        public void SetActiveEnemyViewAndAnimator()
        {
            _healthBarEnemy.gameObject.SetActive(true);
            _healthBarPlayer.gameObject.SetActive(false);
            if (_animatorEnemy == null) return;
            
            _animatorEnemy.gameObject.SetActive(true);

            if (_animator != null)
            {
                _animator.gameObject.SetActive(false);
            }
            

            if (shellsController != null)
            {
                Destroy(shellsController.gameObject);
            }
            _animator = null;
            _attackClip = null;
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
        public float GetYPosition()
        {
            return transform.position.y;
        }
        
        public void SetLayerPosition(float zValue)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
        }

        private void SetRotation(Vector3 pos)
        {
            if (_Character.CharacterType == CharacterType.FirstCharacterBoss || 
                _Character.CharacterType == CharacterType.SecondCharacterBoss ||
            _Character.CharacterType == CharacterType.FourthCharacterBoss ||
                _Character.CharacterType == CharacterType.SixthBossCharacter) return;
            
            if (pos.x < _targetPosition.x)
            {
                transform.DORotate(new Vector3(0, 0, 0), 0.1f).SetLink(gameObject);
                
            }
            else if(pos.x >= _targetPosition.x)
            {
                transform.DORotate(new Vector3(0, -180f, 0), 0.1f).SetLink(gameObject);
            }
            _healthBarGO.transform.DORotate(new Vector3(0, 0, 0), 0).SetLink(gameObject);
        }

        public void SetCanAttack(bool canAttack)
        {
            _canAttack = canAttack;
        }
        
        public void SetPosition(Vector3 pos)
        {
            if (_Character.DamageType == DamageType.Melee)
            {
                if (Math.Abs(pos.x - OldPosX) < 0.05f && IsButtler && _canAttack)
                {
                    _onMoveAnimation = false;
                    if (_audioRunMelee!=null)
                    {
                        _audioRunMelee.Stop();
                    }
                    
                    SetAttackAnimation();
                    SetRotation(pos);
                }
                if (IsButtler && _canAttack == false)
                {
                    if (_audioRunMelee != null &&  _onMoveAnimation == false)
                    {
                        _audioRunMelee.pitch  = UnityEngine.Random.Range(0.9f,1.3f);
                        _audioRunMelee.Play();
                        _onMoveAnimation = true;
                        _currentRunBoss = 0;

                    }
                    
                    SetMoveAnimation();
                    SetRotation(pos);
                    
                }
            }

            if (_Character.DamageType == DamageType.Range)
            {
                if (IsButtler && _canAttack)
                {

                    SetAttackAnimation();
                    SetRotation(pos);
                }
                else if(IsButtler && _canAttack == false)
                {
                    SetIdleAnimation();
                }
            }
            if (_Character.CharacterType == CharacterType.FirstCharacterBoss)
            {
                if (_goRunBoss == false) return;
                if (_animator!=null)
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
                        _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        _goRunBoss = false;
                        return;
                    }
                }
                else if (_animatorEnemy!=null)
                {
                    if (_animatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
                        _animatorEnemy.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        _goRunBoss = false;
                        return;
                    }
                }
            }
            pos = new Vector3(pos.x, pos.y, ZOrder);
            transform.position = pos;
            OldPosX = transform.position.x;
        }

        public void TakeDamageForShells(ICharacter targetCharacter)
        {
            if (_audioFinishShellsAttack != null)
            {
                _audioFinishShellsAttack.pitch = UnityEngine.Random.Range(0.85f, 1.2f);
                _audioFinishShellsAttack.Play();
            }
            if (targetCharacter == null) return;
            targetCharacter.TakeDamage(_damage);
            
        }
        public void TakeMassDamageForShells(ICharacter iCharacter)
        {
            if (_audioFinishShellsAttack != null)
            {
                _audioFinishShellsAttack.Play();
            }
            iCharacter.TakeDamage(_damage);
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public void SetTargetPositionForShells()
        {
            if (_targetCharacter == null) return;
            
            if (shellsController!=null)
            {
                shellsController.SetTargetPosition(_targetCharacter.Position,_targetCharacter);
            }
            
            if (shellsControllerEnemy!=null)
            {
                shellsControllerEnemy.SetTargetPosition(_targetCharacter.Position,_targetCharacter);
            }
        }
        
        private void SetCharacterStartingPosition()
        {
            var position = transform.position;
            OldPosX = position.x;
            OldPosY = position.y;
            transform.DORotate(new Vector3(0, 0, 0), 0);
        }
        
        public void SetStartingTimerForAttack()
        {
            if (_attackClip != null)
            {
                timerAttack = _attackClip.length;
            }
            if (_attackEnemyClip != null)
            {
                timerEnemyAttack = _attackEnemyClip.length;
            }
            CurrentTimerAttack = 0;
        }
        
        public void TakeDamageView()
        {
            if (_Character.CharacterType != CharacterType.FirstCharacterBoss && 
                _Character.CharacterType != CharacterType.SecondCharacterBoss &&
                _Character.CharacterType != CharacterType.ThirdCharacterBoss && 
                _Character.CharacterType != CharacterType.FourthCharacterBoss && 
                _Character.CharacterType != CharacterType.EighthBossCharacter) 
            {
                _damageParticleSystem.gameObject.SetActive(true);
                transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.1f).OnComplete(() =>
                {
                    transform.DOScale(new Vector3(0.55f, 0.55f, 0.55f), 0.15f);
                }).SetLink(gameObject);
                _damageParticleSystem.Play();
            }
            else
            {
                _damageParticleSystem.gameObject.SetActive(true);
                transform.DOScale(new Vector3(_skaleTakeDamage, _skaleTakeDamage, _skaleTakeDamage), 0.1f).OnComplete(
                    () =>
                    {
                        transform.DOScale(new Vector3(_skaleDefolt, _skaleDefolt, _skaleDefolt), 0.15f);
                    }).SetLink(gameObject);
                _damageParticleSystem.Play();
            }

            
        }

        public void InstantiateKillBonusText()
        {
            if (OnDeath) return;
            if (_thisCharacterPlayer) return;
            
            var KillBonus = Instantiate(_killReward,_spawnParticlesDeath.position,Quaternion.identity);
            var text = KillBonus.transform.GetChild(0);
            text.GetComponent<TextMeshProUGUI>().text = $"+{/*_Character.KillReward.ToString()*/"$"}";
            KillBonus.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1,
                transform.position.z), 2f).OnComplete(
                () =>
                {
                    Destroy(KillBonus);
                });
        }

        private GameObject instantiateParticlesDeath;
        public void OnDeathParticle()
        {
            if (OnDeath) return;
            
            instantiateParticlesDeath = Instantiate(_particlesDeath,new Vector3(_spawnParticlesDeath.position.x,_spawnParticlesDeath.position.y, 2f),Quaternion.identity);
            OnDeath = true;
            Invoke("DestroyParticlesDeath", 3f);
        }

        private void DestroyParticlesDeath()
        {
            Destroy(instantiateParticlesDeath);
        }
        
        
        public void SetHealthBar(float value)
        {
            if (_healthBarPlayer != null)
            {
                DOVirtual.Float(_healthBarPlayer.fillAmount, value, 0.2f,
                    (x) => _healthBarPlayer.fillAmount = x);
            }
            
            if (_healthBarEnemy != null)
            {
                DOVirtual.Float(_healthBarEnemy.fillAmount, value, 0.2f,
                    (x) => _healthBarEnemy.fillAmount = x);
            }
        }
        
        public void SetIdleAnimation()
        {
            if (_animator != null)
            {
                _animator.SetBool(Idle,true);
                _animator.SetBool(Attack,false);
                _animator.SetBool(Run,false);
            }
            if (_animatorEnemy != null)
            {
                _animatorEnemy.SetBool(Idle,true);
                _animatorEnemy.SetBool(Attack,false);
                _animatorEnemy.SetBool(Run,false);
            }
        }
        
        public void SetMoveAnimation()
        {
            if (_animator != null)
            {
                _animator.SetBool(Idle,false);
                _animator.SetBool(Attack,false);
                _animator.SetBool(Run,true);
            }
            if (_animatorEnemy != null)
            {
                _animatorEnemy.SetBool(Idle,false);
                _animatorEnemy.SetBool(Attack,false);
                _animatorEnemy.SetBool(Run,true);
            }
        }
        
        public void SetAttackAnimation()
        {
            if (_animator != null)
            {
                _animator.SetBool(Idle,false);
                _animator.SetBool(Attack,true);
                _animator.SetBool(Run,false);
            }
            if (_animatorEnemy != null)
            {
                _animatorEnemy.SetBool(Idle,false);
                _animatorEnemy.SetBool(Attack,true);
                _animatorEnemy.SetBool(Run,false);
            }
        }

        public void SetPlayerCharacter(bool playerCharacter)
        {
            _thisCharacterPlayer = playerCharacter;
        }

        public void SetActiveHealthBar()
        {
            _healthBarGO.SetActive(true);
        }
    }
}
