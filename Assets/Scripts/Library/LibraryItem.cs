using System;
using System.Collections;
using System.Collections.Generic;
using Core.Characters;
using Core.Field;
using DG.Tweening;
using TMPro;
/*
using UnityEditor.U2D.Animation;
*/
using UnityEngine;
/*
using UnityEngine.Serialization;
*/
using UnityEngine.UI;
/*
using YG;
*/

public class LibraryItem : MonoBehaviour
{
    [SerializeField] private GameObject _notAvailable;
    [SerializeField] private Button _rewardVideoButton;
    [SerializeField] private MoneyСonverting _moneyСonverting;
    [SerializeField] public CharacterType _characterType;
    [SerializeField] private TextMeshProUGUI _hp;
    [SerializeField] private TextMeshProUGUI _hpBackground;
    [SerializeField] private TextMeshProUGUI _dmg;
    [SerializeField] private TextMeshProUGUI _dmgBakground;
    [SerializeField] private TextMeshProUGUI _priceBuy;
    [SerializeField] private TextMeshProUGUI _priceBuyBackground;
    [SerializeField] private Image _buyButton;
    [SerializeField] private  CharactersConfig _charactersConfig;
    [SerializeField] private  CharacterConfig _characterConfig;
    [SerializeField] private  CharacterSeller _characterSeller;
    [SerializeField] private  LibraryController _libraryController; 
    private  FieldConstructor _fieldConstructor;

    public int _currentLevel;
    public int _countBuyClick;

    private float hp;
    private float dmg;
    private string hpStr;
    private string dmgStr;
    
    
    private void Start()
    {
        _characterConfig = _charactersConfig.GetConfig(_characterType);
        IncludeCellsFilled();
        Init();
    }

    private void Update()
    {
        _priceBuy.text = $"{_countBuyClick}/3";
        _priceBuyBackground.text = $"{_countBuyClick}/3";
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        ControlAvailableCharacter();
        
        /*if (_fieldConstructor.GetFreeCell() == false)
        {
            _buyButton.DOColor(new Color(1f, 1f, 1f, 0.45f), 0.3f);
            _priceBuy.DOColor(new Color(1f, 1f, 1f, 0.45f), 0.3f);
        }
        else
        {
            _buyButton.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f);
            _priceBuy.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f);

        }*/
        IncludeCellsFilled();
    }

    public void ControlAvailableCharacter()
    {
        if (_currentLevel < 9)
        {
            
        }
        else if (_currentLevel < 19)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);
            }
        }
        else if (_currentLevel < 29)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 39)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 49)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 59)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange||
                _characterType == CharacterType.Shovel || _characterType == CharacterType.LittleBowRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 69)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange||
                _characterType == CharacterType.Shovel || _characterType == CharacterType.LittleBowRange||
                _characterType == CharacterType.Knife || _characterType == CharacterType.BigBowRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 79)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange||
                _characterType == CharacterType.Shovel || _characterType == CharacterType.LittleBowRange||
                _characterType == CharacterType.Knife || _characterType == CharacterType.BigBowRange||
                _characterType == CharacterType.TwoKnives || _characterType == CharacterType.CrossbowRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 89)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange||
                _characterType == CharacterType.Shovel || _characterType == CharacterType.LittleBowRange||
                _characterType == CharacterType.Knife || _characterType == CharacterType.BigBowRange||
                _characterType == CharacterType.TwoKnives || _characterType == CharacterType.CrossbowRange||
                _characterType == CharacterType.Dagger || _characterType == CharacterType.MagicBallRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 99)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange||
                _characterType == CharacterType.Shovel || _characterType == CharacterType.LittleBowRange||
                _characterType == CharacterType.Knife || _characterType == CharacterType.BigBowRange||
                _characterType == CharacterType.TwoKnives || _characterType == CharacterType.CrossbowRange||
                _characterType == CharacterType.Dagger || _characterType == CharacterType.MagicBallRange||
                _characterType == CharacterType.TwoDaggers || _characterType == CharacterType.BigMagicBallRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            }
        }
        else if (_currentLevel < 109)
        {
            if (_characterType == CharacterType.Fist || _characterType == CharacterType.SlingshotRange||
                _characterType == CharacterType.Stick || _characterType == CharacterType.BigSlingshotRange||
                _characterType == CharacterType.Cudgel || _characterType == CharacterType.AxeRange||
                _characterType == CharacterType.Hammer || _characterType == CharacterType.SpearRange||
                _characterType == CharacterType.Shovel || _characterType == CharacterType.LittleBowRange||
                _characterType == CharacterType.Knife || _characterType == CharacterType.BigBowRange||
                _characterType == CharacterType.TwoKnives || _characterType == CharacterType.CrossbowRange||
                _characterType == CharacterType.Dagger || _characterType == CharacterType.MagicBallRange||
                _characterType == CharacterType.TwoDaggers || _characterType == CharacterType.BigMagicBallRange||
                _characterType == CharacterType.Mace || _characterType == CharacterType.MagicWandRange)
            {
                _rewardVideoButton.gameObject.SetActive(false);
                _notAvailable.SetActive(true);            
            }
        }
    }
    public void ClickRewardAds()
    {
        _libraryController.ClickRewardAds(gameObject.GetComponent<LibraryItem>());
    }
    
    
    public void SetCharacterType(CharacterType characterType)
    {
        _characterType = characterType;
        _characterConfig = _charactersConfig.GetConfig(_characterType);
        Init();
    }

    public void Init()
    {
        hp = _characterConfig.Health;
        dmg = _characterConfig.Damage;

        PlayerPrefs.SetFloat($"{hp}{_characterType}", hp);
        PlayerPrefs.SetFloat($"{dmg}{_characterType}", dmg);
        
        _moneyСonverting.GoldConverting($"{hp}{_characterType}", hp);
        _moneyСonverting.GoldConverting($"{dmg}{_characterType}", dmg);
 
        string HP = _moneyСonverting.GetMoney($"{hp}{_characterType}");
        string DMG = _moneyСonverting.GetMoney($"{dmg}{_characterType}");

        string hpDight = string.Empty;
        string hpLetter = string.Empty;
        
        string dmgDight = string.Empty;
        string dmgLetter = string.Empty;
        
        foreach (char ch in HP)
        {
            if (char.IsDigit(ch))
                hpDight += ch;
            
            if (char.IsLetter(ch))
                hpLetter += ch;
        }
        
        foreach (char ch in DMG)
        {
            if (char.IsDigit(ch))
                dmgDight += ch;
            
            if (char.IsLetter(ch))
                dmgLetter += ch;
        }

        _hp.text = $"{hpDight}\n{hpLetter}";
        if (_hpBackground != null)
        {
            _hpBackground.text = $"{hpDight}\n{hpLetter}";
        }
        
        _dmg.text = $"{dmgDight}\n{dmgLetter}";
        if (_dmgBakground != null)
        {
            _dmgBakground.text = $"{dmgDight}\n{dmgLetter}";
        }
    }

    private bool offBuyButton;
    public void OffText()
    {
        offBuyButton = true;
        _hp.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 0f)),0.5f);
        if (_hpBackground != null)
        {
            _hpBackground.DOColor((new Color(_hpBackground.color.r, _hpBackground.color.g, _hpBackground.color.b, 0f)),0.5f);
        }
        
        _dmg.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 0f)),0.5f);
        if (_dmgBakground != null)
        {
            _dmgBakground.DOColor((new Color(_dmgBakground.color.r, _dmgBakground.color.g, _dmgBakground.color.b, 0f)),0.5f);
        }
        
        _priceBuy.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 0f)),0.5f);
        _priceBuyBackground.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 0f)),0.5f);
        _buyButton.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 0f)),0.5f);
        _notAvailable.gameObject.GetComponent<Image>().DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 0f)),0.5f);
        
    }
    public void OnText()
    {
        _hp.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 1f)),0.8f);
        if (_hpBackground != null)
        {
            _hpBackground.DOColor((new Color(_hpBackground.color.r, _hpBackground.color.g, _hpBackground.color.b, 1f)),0.8f);
        }
        _dmg.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 1f)),0.8f);
        if (_dmgBakground != null)
        {
            _dmgBakground.DOColor((new Color(_dmgBakground.color.r, _dmgBakground.color.g, _dmgBakground.color.b, 1f)),0.8f);
        }
        /*_priceBuy.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 1f)),0.8f);
        _buyButton.DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 1f)),0.8f);*/
        _notAvailable.gameObject.GetComponent<Image>().DOColor((new Color(_hp.color.r, _hp.color.g, _hp.color.b, 1f)),0.8f);
        IncludeCellsFilled();
        offBuyButton = false;
    }
    
    public void IncludeCellsFilled()
    {
        if (offBuyButton)
            return;
            
        if (_fieldConstructor.GetFreeCell() == false)
        {
            _buyButton.DOColor(new Color(1f, 1f, 1f, 0.45f), 0.3f);
            _priceBuy.DOColor(new Color(1f, 1f, 1f, 0.45f), 0.3f);
            _priceBuyBackground.DOColor(new Color(0f, 0f, 0f, 0.45f), 0.3f);
        }
        else
        {
            _buyButton.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f);
            _priceBuy.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f);
            _priceBuyBackground.DOColor(new Color(0f, 0f, 0f, 1f), 0.3f);
        }
    }
    
    private void OnDisable()
    {
        PlayerPrefs.SetFloat($"{hp}{_characterType}GoldConverting", 0);
        PlayerPrefs.SetFloat($"{dmg}{_characterType}GoldConverting", 0);
    }

    public void SetCharacterSeller(CharacterSeller characterSeller)
    {
        _characterSeller = characterSeller;
    }
    public void SetLibraryController(LibraryController libraryController)
    {
        _libraryController = libraryController;
    }
    public void SetFieldConstructor(FieldConstructor fieldConstructor)
    {
        _fieldConstructor = fieldConstructor;
        IncludeCellsFilled();
    }

    public void ShowBuyButton()
    {
        _rewardVideoButton.gameObject.SetActive(true);
    }
}
