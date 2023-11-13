using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Core.Characters;
using Core.Field;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;


namespace Core.Battler
{
    public class BattleHandler : MonoBehaviour
    {
        [SerializeField] private FieldPreset fieldPreset;
        [SerializeField] private FieldConstructor fieldConstructor;
        [SerializeField] private FinishLevelView _finishLevelView;
        [SerializeField] private UiLevelManager _uiLevelManager;
        [SerializeField] private FinishLevelController _finishLevelController;
        [SerializeField] private StorageVariable _storageVariable;


        
        private readonly List<ICharacter> _battlesCharacters = new List<ICharacter>();
        private readonly List<ICharacter> _playerCharacters = new List<ICharacter>();
        private readonly List<ICharacter> _enemyCharacters = new List<ICharacter>();
        private readonly List<ICharacter> _allCharactersOnTheField = new List<ICharacter>();
        
        public List<ICharacter> PlayerCharacters => _playerCharacters;
        
        private bool _isBattle;
        public bool IsBattle =>_isBattle;
        private bool _checkWinAndLose;
        public bool IsWin;
        public bool IsLose;
        private int _currentLevel;
        private ICharacter _auxiliaryVariable;
        private ICharacter _killedCharacter;

        public bool CheckWinAndLose => _checkWinAndLose;

        public event Action<bool> OnBattleComplete;

        
        private void OnEnable()
        {
            /*
            startBattleBtn.onClick.AddListener(StartBattle);
        */
        }

        private void OnDisable()
        {
            /*
            startBattleBtn.onClick.RemoveListener(StartBattle);
        */
        }
        
        public void FixedUpdate()
        {
            if (_isBattle == false)
                return;
            

            CheckMoving();
            /*FixOverlap();*/
        }
        

        private void Start()
        {
            Time.timeScale = 3.5f;

            SetActiveViewAndAnimator();
            /*
            StartCoroutine("OverwritePlayerCharacters");
        */
            Invoke("OverwritePlayerCharacters",1f);
        }

       
        public void OverwritePlayerCharacters()
        {
            _playerCharacters.Clear();
            foreach (var cell in fieldConstructor.Cells)
            {
                if(cell.IsBusy())
                    _playerCharacters.Add(cell.Character);
            }
        }
        private void Update()
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            _finishLevelController.GetCurrentLevel(_currentLevel);
            KillCharacters();
            SetActiveViewAndAnimator();
            FinishBattle();
            if (_isBattle == false)
                return;
            BattleLogic();
            FixOverlap();

            /*BattleLogic();*/
        }

        public void ResetLevel()
        {
            IsLose = false;
            IsWin = false;
            _checkWinAndLose = false;
            _allCharactersOnTheField.Clear();
            _battlesCharacters.Clear();
            _uiLevelManager.OnLevelButton();
            _finishLevelView.SetCurrentLevelText(_currentLevel);
        }

        private void SetActiveViewAndAnimator()
        {
            foreach (var cell in fieldPreset.Cells)
            {
                cell.Character?.View.SetActiveEnemyViewAndAnimator();
            }
            
            foreach (var cell in fieldConstructor.Cells)
            {
                cell.Character?.View.SetActivePlayerViewAndAnimator();
            }
        }
        public void StartBattle()
        {
            if (InputBlocker.IsLock())
            {
                return;
            }
            foreach (var cell in fieldConstructor.Cells)
            {
                if(cell.IsBusy())
                    _playerCharacters.Add(cell.Character);
            }
            
            if (_playerCharacters.Count == 0)
            {
                return;
            }
            fieldConstructor.FillAndSaveCharacters();
            _battlesCharacters.Clear();
            _uiLevelManager.OffLevelButton();
            _enemyCharacters.Clear();
            _playerCharacters.Clear();
         
            foreach (var cell in fieldPreset.Cells)
            {
                if(cell.IsBusy())
                    _enemyCharacters.Add(cell.Character);
            }
            
            foreach (var cell in fieldConstructor.Cells)
            {
                if(cell.IsBusy())
                    _playerCharacters.Add(cell.Character);
            }
            
            _battlesCharacters.Clear();

            foreach (var enemy in _enemyCharacters)
            {
                enemy.View.SetActiveHealthBar();
                _battlesCharacters.Add(enemy);
                enemy.Targets = _playerCharacters;
                enemy.View.IsButtler = true;
            }
            
            foreach (var player in _playerCharacters)
            {
                player.View.SetPlayerCharacter(true);
                player.View.SetActiveHealthBar();

                if (player.DamageType == DamageType.Range)
                {
                    player.View.SetActivePlayerViewAndAnimator();
                }
                _battlesCharacters.Add(player);
                player.Targets = _enemyCharacters;
                player.View.IsButtler = true;
            }

            
            _isBattle = true;
        }
        private void BattleLogic()
        {
            
            foreach (var enemy in _enemyCharacters)
            {
                if (enemy.Health <=0)
                    continue;

                if (enemy.CharacterType == CharacterType.FirstCharacterBoss || 
                    enemy.CharacterType == CharacterType.SecondCharacterBoss ||
                    enemy.CharacterType == CharacterType.FourthCharacterBoss ||
                    enemy.CharacterType == CharacterType.FifthBossCharacter ||
                    enemy.CharacterType == CharacterType.SixthBossCharacter
                    )
                {
                    _playerCharacters.Clear();
                    foreach (var cell in fieldConstructor.Cells)
                    {
                        if (cell.IsBusy())
                        {
                            if (cell.Character.Health <= 0) 
                                continue;
                            
                            _playerCharacters.Add(cell.Character);
                        }
                    }
                    enemy.View.ApplyMassAttackBosses(_playerCharacters);
                }
            }
            foreach (var character in _battlesCharacters)
            {
                if (character.Health <= 0)
                    continue;

                if (character.CurrentTarget == null
                    || character.CurrentTarget.Health <= 0)
                {
                    character.View.DistanceAttack = false;
                    character.View.SetCanAttack(false);

                    character.View.RefreshAttackSpeed(character.DamageSpeed);
                    character.View.SetIdleAnimation();
                    character.CurrentTarget = character.FindTarget();
                    if (character.CurrentTarget != null)
                    {
                        character.View.SetTargetPosition(character.CurrentTarget.View.gameObject.transform.position);

                    }
                    character.TimeLastDamage = 0;
                }
                
                if(character.CurrentTarget == null)
                    continue;

                if (character.CanAttack())
                {
                    character.TimeLastDamage = Time.time;
                    character.View.SetTargetPosition(character.CurrentTarget.View.GetPosition());
                    character.View.SetAttackAnimation();
                    character.View.SetCanAttack(true);
                    character.View.SetTargetCharacterAndDamage(character.CurrentTarget, character.Damage);
                    continue;
                }
                
                if (character.IsNearTarget() == false)
                {
                    if (character.View.GoRunBoss)
                    {
                        
                        var direction = character.CurrentTarget.Position - character.Position;
                        direction.Normalize();
                        character.Position += direction * character.MovementSpeed * Time.deltaTime;
                    }
                }
            }
        }
        
        private void FinishBattle()
        {
            
            if (_checkWinAndLose==false)
            {
                foreach (var enemy in _enemyCharacters)
                {
                    if (enemy.Health <= 0 && enemy.View.DethCharacter == false)
                    {
                        _finishLevelController.AddRewardKillsCharacters(enemy.KillReward);
                        enemy.View.DethCharacter = true;
                    }
                }
                AddAllCharactersOnTheField();
            }
            if (_isBattle == false)
                return;
            
            
            
            if (CheckWin())
            {
                _isBattle = false;
                _checkWinAndLose = true;
                IsWin = true;
                IsLose = false;
                _finishLevelController.GetRewardLoseAndeWin();

                foreach (var character in _allCharactersOnTheField)
                {
                    character.View.SetTargetCharacterAndDamage(null,0);
                }
                
                /*_allCharactersOnTheField.Clear();*/
                OnBattleComplete?.Invoke(true);
                
                if (_currentLevel != 199)
                {
                    PlayerPrefs.SetInt("CurrentLevel",_currentLevel + 1);
                }

                _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
                
                fieldPreset.SetCurrentLevel(_currentLevel);
                _finishLevelController.SetCurrentLevel(_currentLevel);
                _finishLevelView.OnFinishWinPanel();
                if (SaveCloudController.singleton.ICloud != null)
                {
                    SaveCloudController.singleton.ICloud.Save();
                }
            }

            if (CheckLose())
            {
                _isBattle = false;
                _checkWinAndLose = true;
                IsWin = false;
                IsLose = true;
                _finishLevelController.GetRewardLoseAndeWin();
                
                foreach (var character in _allCharactersOnTheField)
                {
                    character.View.SetTargetCharacterAndDamage(null,0);
                }
                
                /*_allCharactersOnTheField.Clear();*/
                OnBattleComplete?.Invoke(false);
                Debug.Log("Lose");
                _finishLevelView.OnFinishLosePanel();
                if (SaveCloudController.singleton.ICloud != null)
                {
                    SaveCloudController.singleton.ICloud.Save();
                }

            }
        }

        private void AddAllCharactersOnTheField()
        {
            if (_checkWinAndLose)
            {
                return;
            }
            _allCharactersOnTheField.Clear();
            foreach (var cell in fieldPreset.Cells)
            {
                if (cell.IsBusy())
                {
                    cell.Character.View._Character = cell.Character;
                    _allCharactersOnTheField.Add(cell.Character);
                }
                    
            }
            
            foreach (var cell in fieldConstructor.Cells)
            {
                if (cell.IsBusy())
                {
                    cell.Character.View._Character = cell.Character;
                    _allCharactersOnTheField.Add(cell.Character);
                }
                    
            }
            SetPositionInLayer();
        }

        public void SetPositionInLayer()
        {
            if (_checkWinAndLose)
            {
                return;
            }
            
            for (int i = 0; i < _allCharactersOnTheField.Count; i++)
            {
                for (int j = 0; j < _allCharactersOnTheField.Count-1-i; j++)
                {
                    if (_allCharactersOnTheField[j].View.GetYPosition()>_allCharactersOnTheField[j+1].View.GetYPosition())
                    {
                        _auxiliaryVariable = _allCharactersOnTheField[j];
                        _allCharactersOnTheField[j] = _allCharactersOnTheField[j + 1];
                        _allCharactersOnTheField[j + 1] = _auxiliaryVariable;
                    }
                }
            }

            float zPosition = 0;
            foreach (var character in _allCharactersOnTheField)
            {
                zPosition += 0.1f;
                character.View.SetLayerPosition(zPosition);
            }
        }




        private void FixOverlap()
        {
            foreach (var character in _battlesCharacters)
            {
                if(character.Health <= 0)
                    continue;

                foreach (var characterNear in _battlesCharacters)
                {
                    if(character == characterNear || character.Health <= 0)
                        continue;
                    
                    if (Vector3.Distance(character.Position, characterNear.Position) < 0.25f && 
                    Math.Abs(character.CurrentTarget.View.transform.position.y - character.View.transform.position.y) < 0.5f)
                    {
                        var direction = characterNear.Position - character.Position;
                        direction.Normalize();
                        
                        character.Position += -direction * character.MovementSpeed * Time.deltaTime;
                    }
                }
            }
        }

        private void KillCharacters()
        {
            foreach (var character in _battlesCharacters)
            {
                if (character.Health <= 0)
                {
                    /*character.View.InstantiateKillBonusText();*/
                    character.View.OnDeathParticle();
                    character.View.gameObject.SetActive(false);
                    _killedCharacter = character;
                }
            }

            _battlesCharacters.Remove(_killedCharacter);
        }

        private bool CheckWin()
        {
            foreach (var enemy in _enemyCharacters)
            {
                if (enemy.Health > 0)
                    return false;
            }
            foreach (var character in _battlesCharacters)
            {
                character.View.IsButtler = false;
                character.View.SetIdleAnimation();
            }

            Invoke("OffAllCharacters",2f);

            return true;
        }

        private bool CheckLose()
        {
            foreach (var player in _playerCharacters)
            {
                if (player.Health > 0)
                    return false;
            }
            foreach (var character in _battlesCharacters)
            {
                character.View.IsButtler = false;
                character.View.SetIdleAnimation();
            }
            Invoke("OffAllCharacters",2f);

            return true;
        }

        public void OffAllCharacters()
        {
            foreach (var character in _allCharactersOnTheField)
            {
                character.View.SetActiveFalse();
            }
        }
        public void OnAllCharacters()
        {
            foreach (var character in _allCharactersOnTheField)
            {
                character.View.SetActiveTrue();
            }
        }

        private void CheckMoving()
        {
            if (_isBattle == false) return;
            
            foreach (var сharacter in _battlesCharacters)
            {
                if (сharacter == null) continue;

                /*if (сharacter.CharacterType == CharacterType.FirstCharacterBoss)
                {
                    if (сharacter.View.GoRunBoss == false)
                    {
                        continue;
                    }
                }*/
                сharacter.Move();
            }
        }
    }
}
