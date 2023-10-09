using System;
using System.Collections;
using System.Collections.Generic;
using Core.Battler;
using Core.Characters;
using Core.Field;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelManager : MonoBehaviour
{
    private static string MoneyInTheStorage = "MoneyInTheStorage";

    private static string PriceMeleeS = "PriceMelee"; // Remove name
    private static string PriceRangeS = "PriceRange";
    
    private static string Attack = "Attack";
    private static string Out = "Out";

    [SerializeField] private Image _buyMelee;
    [SerializeField] private Image _buyRange;
    [SerializeField] private Image _startGameButton;
    [SerializeField] private Sprite[] _notEnoughMoney = new Sprite[2]; 
    [SerializeField] private Sprite[] _enoughMoney = new Sprite[2]; 
    [SerializeField] private Sprite[] _rewardEnoughMoney = new Sprite[2]; 
    [SerializeField] private Button _boxRewardButton;
    [SerializeField] private AudioSource _moneySound;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private GameObject _libraryPanel;
    [SerializeField] private GameObject _settingBackground;
    [SerializeField] private GameObject _libraryBackground;
    [SerializeField] private GameObject _offMusicGameObject;
    [SerializeField] private GameObject _offSoundGameObject;
    [SerializeField] private GameObject _moneyParticles;

    [SerializeField] private CharacterSeller _characterSeller;
    [SerializeField] private FieldConstructor _fieldConstructor;
    [SerializeField] private TextMeshProUGUI _priceMeleeCharacterText;
    [SerializeField] private TextMeshProUGUI _priceMeleeCharacterTextBackground;
    [SerializeField] private TextMeshProUGUI _priceRangeCharacterText;
    [SerializeField] private TextMeshProUGUI _priceRangeCharacterTextBackgrpund;
    [SerializeField] private TextMeshProUGUI _currentLevelText;   
    [SerializeField] private TextMeshProUGUI _currentLevelBackground;   
    [SerializeField] private TextMeshProUGUI _currentGoldText;    //KOLHOZ
    [SerializeField] private TextMeshProUGUI _currentGoldTextBackground;    //KOLHOZ
    [SerializeField] private TextMeshProUGUI _boxRewardText;
    [SerializeField] private TextMeshProUGUI _boxRewardTextBackground;
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private Animator _axeMusicAnimator;
    [SerializeField] private Animator _axeSoundAnimator;
    [SerializeField] private GameObject  _checkMarkMusic;
    [SerializeField] private GameObject _checkMarkSound;
    [SerializeField] private SoundController _soundController;
    
    [SerializeField] private Button _boxReward;
    [SerializeField] private int _sec;
    [SerializeField] private int _min;
    [SerializeField] private int _delta = 1;
    /*
    [SerializeField] private TextMeshProUGUI _timerText;
    */
    [SerializeField] private StorageVariable _storageVariable;
    [SerializeField] private FormattingMoney _formattingMoney;
    [SerializeField] private MoneyСonverting _moneyСonverting;


    [SerializeField] private List<Button> _levelButton;
    [SerializeField] private List<Image> _levelButtonsSnow;

    private bool _onSetting;
    private bool _onLibrary;
    public bool OnSetting => _onSetting;
    public bool OnLibrary => _onLibrary;
    
    private bool _onMusic;
    private bool _onSound;
    private bool _stopTimer;
    private bool _stopBoxAnimationCoroutine;
    
    private bool _noMoneyStateMelee;
    private bool _noMoneyStateRange;
    public bool NoMoneyStateMelee =>_noMoneyStateMelee; 
    public bool NoMoneyStateRange =>_noMoneyStateRange;

    private void Start()
    {
        _boxReward.interactable = false;
        _delta = 1;/*PlayerPrefs.GetInt("Delta");*/
        _min = 2;/*PlayerPrefs.GetInt("Min");*/
        _sec = 59;/*PlayerPrefs.GetInt("Sec");*/

        StartCoroutine(AnimationStartButton());
        StartCoroutine(TimeFlow());
    }

    private void Update()
    {
        CheckAxeAnimation();
        
        if (_stopTimer == true)
        {
            if (_stopBoxAnimationCoroutine == false)
            {
                /*StartCoroutine(AnimationBoxReward());*/
            }
            _stopBoxAnimationCoroutine = true;
        }

        IncludeCellsFilled();

    }

    public void IncludeCellsFilled()
    {
        if (offLevelButton == true)
            return;

        var freeCell = _fieldConstructor.GetFreeCell();
        if(freeCell == null)
        {
            _buyMelee.DOColor(new Color(1f,1f,1f,0.5f),0.5f);
            _buyRange.DOColor(new Color(1f,1f,1f,0.5f),0.5f);
        }
        else
        {
            _buyMelee.DOColor(new Color(1f,1f,1f,1f),0.5f);
            _buyRange.DOColor(new Color(1f,1f,1f,1f),0.5f);
        }
    }
    
    IEnumerator TimeFlow()
    {
        if (_stopTimer==false)
        {
            while (true)
            {
                if (_sec == 0)
                {
                    _min--;
                    PlayerPrefs.SetInt("Min", _min);

                    if (_min <= -1 && _sec <= 0)
                    {
                        _delta = 0;
                        _min = 0;
                        PlayerPrefs.SetInt("Delta", _delta);

                        _boxReward.interactable = true; 
                        _stopTimer = true;
                    }
                    else
                    {
                        _sec = 59;
                    }
                }
                _sec -= _delta;
                PlayerPrefs.SetInt("Sec", _sec);
                _boxRewardText.text = $"{_min.ToString("D2")}:{_sec.ToString("D2")}";
                _boxRewardTextBackground.text = $"{_min.ToString("D2")}:{_sec.ToString("D2")}";
                yield return new WaitForSeconds(1);
            }
        }
    }

    

    public void TakeRewardBox()
    {
        if (InputBlocker.IsLock())
        {
            return;
        }
        InputBlocker.InputLock(this,0.5f);
        StopCoroutine(AnimationBoxReward());
        StartCoroutine(RewardBox());
    }
    
    

    public void OnLibraryPanel()
    {
        if (_onLibrary == false)
        {
            OffLevelButton();
            _onLibrary = true;
            _battleHandler.OffAllCharacters();
            _libraryPanel.SetActive(true);
            _libraryPanel.transform.GetComponent<Image>().DOColor(new Color(0.3f, 0.3f, 0.3f, 0.5f), 0.3f)
                .SetLink(gameObject);
            
            _libraryBackground.transform.DOScale(new Vector3(0.85f, 0.85f, 0.85f), 0.6f).
                SetEase(Ease.OutBack).SetLink(gameObject);
        }
        else
        {
            Invoke("OnLevelButton", 0.8f);
            
            
            _libraryPanel.transform.GetComponent<Image>().DOColor(new Color(0.3f, 0.3f, 0.3f, 0f), 0.4f)
                .SetLink(gameObject);

            _libraryBackground.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.8f).
                SetEase(Ease.InBack).OnComplete(() =>
                {
                    _onLibrary = false;

                    _libraryPanel.SetActive(false);
                    _battleHandler.OnAllCharacters();
                }).SetLink(gameObject);
        }
    }
    
    private void CheckAxeAnimation()
    {
        /*_axeMusicAnimator.speed = 1000000f;
        _axeSoundAnimator.speed = 1000000f;*/

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            _onMusic = true;
            
            _checkMarkMusic.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.08f).SetUpdate(UpdateType.Normal,true)
                .OnComplete(() =>
                {
                    _checkMarkMusic.SetActive(false);
                });
            
            /*_axeMusicAnimator.SetBool(Attack,false);
            _axeMusicAnimator.SetBool(Out,true);*/
        }
        else
        {            
            _onMusic = false;

            _checkMarkMusic.SetActive(true);
            _checkMarkMusic.transform.DOScale(Vector3.one, 0.08f).SetUpdate(UpdateType.Normal, true);
            
            /*_axeMusicAnimator.SetBool(Attack,true);
            _axeMusicAnimator.SetBool(Out,false);*/

        }
        
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            _onSound = true;
            _checkMarkSound.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.08f).SetUpdate(UpdateType.Normal,true)
                .OnComplete(() =>
                {
                    _checkMarkSound.SetActive(false);
                });
            
            /*_axeSoundAnimator.SetBool(Attack,false);
            _axeSoundAnimator.SetBool(Out,true);*/
        }
        else
        {
            _onSound = false;
            
            _checkMarkSound.SetActive(true);
            _checkMarkSound.transform.DOScale(Vector3.one, 0.08f).SetUpdate(UpdateType.Normal, true);
            
            /*_axeSoundAnimator.SetBool(Attack,true);
            _axeSoundAnimator.SetBool(Out,false);*/
        }
    }
    public void OnSettingPanel()
    {
        if (_onSetting == false)
        {
            _settingPanel.SetActive(true);
            _settingPanel.transform.GetComponent<Image>().DOColor(new Color(0.3f, 0.3f, 0.3f, 0.5f), 0.3f)
                .SetLink(gameObject).SetUpdate(UpdateType.Normal, true);
            
            _settingBackground.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).
                SetEase(Ease.OutBack).OnComplete(() =>
            {

                _offMusicGameObject.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack)
                    .SetLink(gameObject).SetUpdate(UpdateType.Normal,true);;
                _offSoundGameObject.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBack)
                    .SetLink(gameObject).SetUpdate(UpdateType.Normal,true);
                _onSetting = true;
            }).SetLink(gameObject).SetUpdate(UpdateType.Normal,true);

        }
        else
        {
            Time.timeScale = 1.35f;

            _settingPanel.transform.GetComponent<Image>().DOColor(new Color(0.3f, 0.3f, 0.3f, 0f), 0.4f)
                .SetLink(gameObject);

            _settingBackground.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f).
                SetEase(Ease.InBack).OnComplete(() =>
            {
                _onSetting = false;
                _settingPanel.SetActive(false);
                _battleHandler.OnAllCharacters();
                _offMusicGameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.15f).
                    SetEase(Ease.Flash).SetLink(gameObject);
                _offSoundGameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.15f).
                    SetEase(Ease.Flash).SetLink(gameObject);
            }).SetLink(gameObject);
            
        }
    }

    public void OnClickMusicButton()
    {
        if (_onMusic == false)
        {
            PlayerPrefs.SetInt("Music",1);
            /*if (_axeMusicAnimator.GetCurrentAnimatorStateInfo(0).IsName("Out") == false) return;*/
        }
        else
        {
            PlayerPrefs.SetInt("Music",0);
            /*if (_axeMusicAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;*/
        }
    }
    public void OnClickSoundButton()
    {
        if (_onSound == false)
        {
            PlayerPrefs.SetInt("Sound",1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound",0);
        }
    }

    private bool offLevelButton;
    public void OffLevelButton()
    {
        offLevelButton = true;
        _currentGoldText.DOColor(new Color(_currentGoldText.color.r,
            _currentGoldText.color.g, _currentGoldText.color.b, 0f), 1f).SetLink(gameObject);
        _currentGoldTextBackground.DOColor(new Color(_currentGoldTextBackground.color.r,
            _currentGoldTextBackground.color.g, _currentGoldTextBackground.color.b, 0f), 1f).SetLink(gameObject);
        
        _currentLevelText.DOColor(new Color(_currentLevelText.color.r,
            _currentLevelText.color.g, _currentLevelText.color.b, 0f), 1f).SetLink(gameObject);
        _currentLevelBackground.DOColor(new Color(_currentLevelBackground.color.r,
            _currentLevelBackground.color.g, _currentLevelBackground.color.b, 0f), 1f).SetLink(gameObject);
        
        _priceMeleeCharacterText.DOColor(new Color(_priceMeleeCharacterText.color.r,
            _priceMeleeCharacterText.color.g, _priceMeleeCharacterText.color.b, 0f), 1f).SetLink(gameObject);
        _priceMeleeCharacterTextBackground.DOColor(new Color(_priceMeleeCharacterTextBackground.color.r,
            _priceMeleeCharacterTextBackground.color.g, _priceMeleeCharacterTextBackground.color.b, 0f), 1f).SetLink(gameObject);
            
        _priceRangeCharacterText.DOColor(new Color(_priceRangeCharacterText.color.r,
            _priceRangeCharacterText.color.g, _priceRangeCharacterText.color.b, 0f), 1f).SetLink(gameObject);
        _priceRangeCharacterTextBackgrpund.DOColor(new Color(_priceRangeCharacterTextBackgrpund.color.r,
            _priceRangeCharacterTextBackgrpund.color.g, _priceRangeCharacterTextBackgrpund.color.b, 0f), 1f).SetLink(gameObject);
            
        _boxRewardButton.GetComponent<Image>().DOColor(new Color(_boxRewardButton.GetComponent<Image>().color.r,
            _boxRewardButton.GetComponent<Image>().color.g, _boxRewardButton.GetComponent<Image>().color.b, 0f), 1f).SetLink(gameObject);
        
        _boxRewardText.DOColor(new Color(_boxRewardText.color.r,
            _boxRewardText.color.g, _boxRewardText.color.b, 0f), 1f).SetLink(gameObject);
        _boxRewardTextBackground.DOColor(new Color(_boxRewardTextBackground.color.r,
            _boxRewardTextBackground.color.g, _boxRewardTextBackground.color.b, 0f), 1f).SetLink(gameObject);
        foreach (var button in _levelButton)
        {
            button.interactable = false;
            button.image.DOColor(new Color(button.image.color.r, 
                    button.image.color.g, button.image.color.b, 0f), 1f).SetLink(gameObject);;
        }

        foreach (var image in _levelButtonsSnow)
        {
            image.DOColor(new Color(image.color.r, 
                image.color.g, image.color.b, 0f), 1f).SetLink(gameObject);
        }
    }
    public void OnLevelButton()
    {
        offLevelButton = false;
        _currentGoldText.DOColor(new Color(_currentGoldText.color.r,
            _currentGoldText.color.g, _currentGoldText.color.b, 1f), 0.5f).SetLink(gameObject);
        _currentGoldTextBackground.DOColor(new Color(_currentGoldTextBackground.color.r,
            _currentGoldTextBackground.color.g, _currentGoldTextBackground.color.b, 1f), 0.5f).SetLink(gameObject);
        
        _currentLevelText.DOColor(new Color(_currentLevelText.color.r,
            _currentLevelText.color.g, _currentLevelText.color.b, 1f), 0.5f).SetLink(gameObject);
        _currentLevelBackground.DOColor(new Color(_currentLevelBackground.color.r,
            _currentLevelBackground.color.g, _currentLevelBackground.color.b, 1f), 0.5f).SetLink(gameObject);
        
        _priceMeleeCharacterText.DOColor(new Color(_priceMeleeCharacterText.color.r,
            _priceMeleeCharacterText.color.g, _priceMeleeCharacterText.color.b, 1f), 0.5f).SetLink(gameObject);
        _priceMeleeCharacterTextBackground.DOColor(new Color(_priceMeleeCharacterTextBackground.color.r,
            _priceMeleeCharacterTextBackground.color.g, _priceMeleeCharacterTextBackground.color.b, 1f), 0.5f).SetLink(gameObject);    
        
        _priceRangeCharacterText.DOColor(new Color(_priceRangeCharacterText.color.r,
            _priceRangeCharacterText.color.g, _priceRangeCharacterText.color.b, 1f), 0.5f).SetLink(gameObject);
        _priceRangeCharacterTextBackgrpund.DOColor(new Color(_priceRangeCharacterTextBackgrpund.color.r,
            _priceRangeCharacterTextBackgrpund.color.g, _priceRangeCharacterTextBackgrpund.color.b, 1f), 0.5f).SetLink(gameObject);
            
        _boxRewardButton.GetComponent<Image>().DOColor(new Color(_boxRewardButton.GetComponent<Image>().color.r,
            _boxRewardButton.GetComponent<Image>().color.g, _boxRewardButton.GetComponent<Image>().color.b, 1f), 0.5f).SetLink(gameObject);
        
        _boxRewardText.DOColor(new Color(_boxRewardText.color.r,
            _boxRewardText.color.g, _boxRewardText.color.b, 1f), 0.5f).SetLink(gameObject);
        _boxRewardTextBackground.DOColor(new Color(_boxRewardTextBackground.color.r,
            _boxRewardTextBackground.color.g, _boxRewardTextBackground.color.b, 1f), 0.5f).SetLink(gameObject);
        
        
        foreach (var button in _levelButton)
        {
            button.interactable = true;
            button.image.DOColor(new Color(button.image.color.r,
                button.image.color.g, button.image.color.b, 1f), 0.5f).SetLink(gameObject);;
        }
        foreach (var image in _levelButtonsSnow)
        {
            image.DOColor(new Color(image.color.r,
                image.color.g, image.color.b, 1f), 0.5f).SetLink(gameObject);
        }
    }

    
    public IEnumerator CheckStorageAndPriceMelee()
    {
        if (_battleHandler.IsBattle)
        {
            yield break;
        }
        if (_moneyСonverting.MoreOrEqual(MoneyInTheStorage, PriceMeleeS))
        {
            _buyMelee.sprite = _enoughMoney[0];
            _priceMeleeCharacterText.gameObject.SetActive(true);
            _priceMeleeCharacterTextBackground.gameObject.SetActive(true);
            _noMoneyStateMelee = false;
        }
        else
        {
            if (_noMoneyStateMelee == false)
            {
                _buyMelee.GetComponent<Button>().interactable = false;
                _noMoneyStateMelee = true;
                if (Application.isMobilePlatform)       ///////   7
                {
                    _buyMelee.transform.DOScale(new Vector3(1.27f,1.27f, 1.27f),
                        0.3f);
                }
                else
                {
                    _buyMelee.transform.DOScale(new Vector3(0.8f,0.8f, 0.8f),
                        0.3f);
                }
                
                _buyMelee.DOColor(
                    new Color(_buyMelee.color.r, _buyMelee.color.g, _buyMelee.color.b, 0.8f), 0.3f).OnComplete(() =>
                {
                    _priceMeleeCharacterText.gameObject.SetActive(false);
                    _priceMeleeCharacterTextBackground.gameObject.SetActive(false);
                    _buyMelee.sprite = _notEnoughMoney[0];
                    _buyMelee.GetComponent<Button>().interactable = false;
                });
            
                yield return new WaitForSeconds(2f);
                if (_battleHandler.IsBattle)
                {
                    if (Application.isMobilePlatform)      ///////
                    {
                        _buyMelee.transform.DOScale(new Vector3(1.47f,1.47f, 1.47f), 0.3f);
                    }
                    else
                    {
                        _buyMelee.transform.DOScale(new Vector3(1f,1f, 1f), 0.3f);
                    }
                    _buyMelee.GetComponent<Button>().interactable = true;
                    yield break;
                }
                if (Application.isMobilePlatform)      ///////
                {
                    _buyMelee.transform.DOScale(new Vector3(1.47f,1.47f, 1.47f), 0.3f);
                }
                else
                {
                    _buyMelee.transform.DOScale(new Vector3(1f,1f, 1f), 0.3f);
                }
                _buyMelee.DOColor(
                    new Color(_buyMelee.color.r, _buyMelee.color.g, _buyMelee.color.b, 1f), 0.4f).OnComplete(() =>
                {
                    _buyMelee.GetComponent<Button>().interactable = true;
                    _buyMelee.sprite = _rewardEnoughMoney[0];
                });
            }
        }
    }
    public IEnumerator CheckStorageAndPriceRange()
    {
        if (_moneyСonverting.MoreOrEqual(MoneyInTheStorage, PriceRangeS))
        {
            _priceRangeCharacterText.gameObject.SetActive(true);
            _priceRangeCharacterTextBackgrpund.gameObject.SetActive(true);
            _buyRange.sprite = _enoughMoney[1];
            _noMoneyStateRange = false;

        }
        else
        {
            if (_noMoneyStateRange == false)
            {
                _noMoneyStateRange = true;
                _buyRange.GetComponent<Button>().interactable = false;
                if (Application.isMobilePlatform)      ///////
                {
                    _buyRange.transform.DOScale(new Vector3(1.27f,1.27f, 1.27f),
                        0.3f);
                }
                else
                {
                    _buyRange.transform.DOScale(new Vector3(0.8f,0.8f, 0.8f),
                        0.3f);
                }
                _buyRange.DOColor(
                    new Color(_buyRange.color.r, _buyRange.color.g, _buyRange.color.b, 0.8f), 0.3f).OnComplete(() =>
                {
                    _priceRangeCharacterText.gameObject.SetActive(false);
                    _priceRangeCharacterTextBackgrpund.gameObject.SetActive(false);
                    _buyRange.sprite = _notEnoughMoney[1];
                    _buyRange.GetComponent<Button>().interactable = false;
                });
            
                yield return new WaitForSeconds(2f);

                if (_battleHandler.IsBattle)
                {
                    if (Application.isMobilePlatform)   ///////
                    {
                        _buyRange.transform.DOScale(new Vector3(1.47f,1.47f, 1.47f), 0.3f);
                    }
                    else
                    {
                        _buyRange.transform.DOScale(new Vector3(1f,1f, 1f), 0.3f);
                    }
                    _buyRange.GetComponent<Button>().interactable = true;
                    yield break;
                }
                if (Application.isMobilePlatform)     ///////
                {
                    _buyRange.transform.DOScale(new Vector3(1.47f,1.47f, 1.47f), 0.3f);
                }
                else
                {
                    _buyRange.transform.DOScale(new Vector3(1f,1f, 1f), 0.3f);
                }
                
                _buyRange.DOColor(
                    new Color(_buyRange.color.r, _buyRange.color.g, _buyRange.color.b, 1f), 0.4f).OnComplete(() =>
                {
                    _buyRange.GetComponent<Button>().interactable = true;
                    _buyRange.sprite = _rewardEnoughMoney[1];
                });
            }
        }
    }
    
    public IEnumerator AnimationStartButton()
    {
        if (Application.isMobilePlatform)  ////////
        {
            if (_startGameButton.GetComponent<UiAnimation>()._OnPointerEnters == false)
            {
                _startGameButton.transform.DOScale(1.47f, 0.4f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    _startGameButton.transform.DOScale(1.45f, 0.6f).SetEase(Ease.OutBack);
                });
                yield return new WaitForSeconds(4.5f);
                StartCoroutine(AnimationStartButton());
            }
            else
            {
                yield return new WaitForSeconds(4.5f);
                StartCoroutine(AnimationStartButton());
            }
        }
        else
        {
            if (_startGameButton.GetComponent<UiAnimation>()._OnPointerEnters == false)
            {
                _startGameButton.transform.DOScale(1.05f, 0.4f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    _startGameButton.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
                });
                yield return new WaitForSeconds(6.5f);
                StartCoroutine(AnimationStartButton());
            }
            else
            {
                yield return new WaitForSeconds(6.5f);
                StartCoroutine(AnimationStartButton());
            }
        }
        
    }
    
    
    private IEnumerator AnimationBoxReward()
    {
        yield return new WaitForSeconds(1f);
        
        while (_stopTimer)
        {
            if (_boxReward.GetComponent<UiAnimation>()._OnPointerEnters == false)
            {
                _stopBoxAnimationCoroutine = true;
                /*_boxReward.transform.DOScale(new Vector3(1.35f,
                        1.4f, _boxReward.transform.localScale.z), 0.5f).SetEase(Ease.InBack).
                    OnComplete(() =>
                    {
                        _boxReward.transform.DOScale(new Vector3(1.3f,
                                1.3f, _boxReward.transform.localScale.z), 0.5f).
                            SetEase(Ease.OutBack);
                    });*/
                yield return new WaitForSeconds(6f);
            }
            else
            {
                yield return new WaitForSeconds(6f);
            }
        }
    }
    public IEnumerator RewardBox()
    {
        if (_stopTimer)
        {
            /*_boxReward.transform.DOScale(new Vector3(1.25f,
                1.25f, _boxReward.transform.localScale.z), 0.7f);*/

            yield return new WaitForSeconds(0.7f);
            _stopTimer = false;
            _stopBoxAnimationCoroutine = false;
            
            _min = 2;
            /*
            PlayerPrefs.SetInt("Min", _min);
            */

            _sec = 59;
            /*
            PlayerPrefs.SetInt("Sec", _sec);
            */

            _delta = 1;
            /*
            PlayerPrefs.SetInt("Delta", _delta);
            */
            _boxReward.interactable = false;

            float _sum = 0;
            PlayerPrefs.SetFloat("_sum",_sum);
            _sum = _moneyСonverting.AddMoney("_sum" ,PriceMeleeS, PriceRangeS);
            PlayerPrefs.SetFloat("_sum",_sum);


            var addStorage = _moneyСonverting.Multiplication("_sum",1.5f);
            PlayerPrefs.SetFloat("_sum",addStorage);

            _storageVariable.AddMoney("_sum");

            Instantiate(_moneyParticles,new Vector3(_boxReward.transform.position.x,
                _boxReward.transform.position.y + 0.5f, 2f),Quaternion.identity);
                    
                    
            /*_boxReward.transform.DOScale(new Vector3(1.3f,
                    1.3f, _boxReward.transform.localScale.z), 0.5f).
                SetEase(Ease.OutBack).SetLink(gameObject);*/

            StartCoroutine(CheckStorageAndPriceMelee());
            StartCoroutine(CheckStorageAndPriceRange());
            /*_boxReward.transform.DOScale(new Vector3(1.4f,
                    1.5f, _boxReward.transform.localScale.z), 0.3f).SetEase(Ease.InBack).
                OnComplete(() =>
                {
                    
                    
                });*/
            
            yield return new WaitForSeconds(0.95f);
            _moneySound.Play();

        }
    }
}
