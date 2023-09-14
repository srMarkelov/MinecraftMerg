using System;
using System;
using System.Collections;
using System.Collections.Generic;
using Ads;
using Core.Battler;
using Core.Characters;
using Core.Field;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class LibraryController : MonoBehaviour
{
    [SerializeField] private List<Image> _characterMelee;
    [SerializeField] private List<Image> _characterRange;
    [SerializeField] private List<Sprite> _normalCharcterMelee; 
    [SerializeField] private List<Sprite> _shadowlCharcterMelee;
    [SerializeField] private List<Sprite> _normalCharcterRange;
    [SerializeField] private List<Sprite> _shadowlCharcterRange;
    [SerializeField] private List<LibraryItem> _libraryItems;
    
    [SerializeField] private FieldConstructor _fieldConstructor;
    [SerializeField] private Button _meleeButon;
    [FormerlySerializedAs("_ramgeButon")] [SerializeField] private Button _rangeButon;
    [SerializeField] private Button _libraryBtn;
    [SerializeField] private GameObject _scrollMelee;
    [SerializeField] private GameObject _scrollRange;
    [SerializeField] private Image _newCharacterPanel;
    [SerializeField] private GameObject _startPositionNewCharacterPanel;
    [SerializeField] private LibraryItem _newlibraryItem;
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private CharacterSeller _characterSeller;
    [SerializeField] private AdsYandex _adsYandex;
    [SerializeField] private AudioSource _newCharacterSound;
    
    private LibraryItem _libraryItem;

    private int _currentMaxCharacterMelee;
    private int _currentMaxCharacterRange;
    
    private bool OnMeleeLibrary;
    private bool OnRangeLibrary;
    public bool ShowNewCharacter;

    
    private void Reward(int idReward)
    {
        if (idReward == (int) AdsRewardType.IdAddCharacterFromLibrary)
        {
            RewardAds();
        }
    }

    private void Start()
    {
        SetFieldConstructorInLibraryItem();

        OnMeleeLibrary = true;
        _meleeButon.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.3f);
        _rangeButon.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
        
        for (int i = 0; i < _characterMelee.Count; i++)
        {
            _characterMelee[i].sprite = _shadowlCharcterMelee[i];
        }
        
        for (int i = 0; i < _characterRange.Count; i++)
        {
            _characterRange[i].sprite = _shadowlCharcterRange[i];
        }
        
        for (int i = 0; i < _characterRange.Count; i++)
        {
            _characterRange[i].GetComponent<LibraryItem>().OffText();
        }

        _scrollMelee.SetActive(true);
        _scrollRange.SetActive(false);
        SetCharacterSellerInLibraryItem();
        SetLibraryControllerInLibraryItem();
    }

    public void StartGame()
    {
        _currentMaxCharacterMelee = PlayerPrefs.GetInt("CurrentMaxCharacterMelee");
        _currentMaxCharacterRange = PlayerPrefs.GetInt("CurrentMaxCharacterRange");
    }

    public void SetCharacterSellerInLibraryItem()
    {
        foreach (var item in _libraryItems)
        {
            item.SetCharacterSeller(_characterSeller);
        }
    }
    public void SetFieldConstructorInLibraryItem()
    {
        foreach (var item in _libraryItems)
        {
            item.SetFieldConstructor(_fieldConstructor);
        }
    }
    public void ClickMeleeButton()
    {
        if (OnRangeLibrary)
        {
            _meleeButon.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.3f);
            _rangeButon.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
            for (int i = 0; i < _characterRange.Count; i++)
            {
                if (i == _characterRange.Count - 1)
                {
                    _scrollMelee.SetActive(true);
                }
                _characterRange[i].GetComponent<LibraryItem>().OffText();

                _characterRange[i].DOColor(new Color(_characterRange[i].color.r, _characterRange[i].color.g,
                    _characterRange[i].color.b, 0f), 0.8f).OnComplete(() =>
                {
                    if (i == _characterRange.Count)
                    {
                        _scrollRange.SetActive(false);
                    }
                    foreach (var melee in _characterMelee)
                    {
                        melee.GetComponent<LibraryItem>().OnText();

                        melee.DOColor(new Color(melee.color.r, melee.color.g, melee.color.b, 1f), 0.8f).OnComplete(() =>
                        {
                        });
                    }
                    OnRangeLibrary = false;
                    OnMeleeLibrary = true;
                });
            
            }
        }
    }

    public void ClickRangeButton()
    {
        if (OnMeleeLibrary)
        {
            _rangeButon.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.3f);
            _meleeButon.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
            for (int i = 0; i < _characterMelee.Count; i++)
            {
                if (i == _characterMelee.Count - 1)
                {
                    _scrollRange.SetActive(true);
                }
                _characterMelee[i].GetComponent<LibraryItem>().OffText();

                _characterMelee[i].DOColor(new Color(_characterMelee[i].color.r, _characterMelee[i].color.g,
                    _characterMelee[i].color.b, 0f), 0.8f).OnComplete(() =>
                {
                    if (i == _characterMelee.Count)
                    {
                        _scrollMelee.SetActive(false);
                    }
                    foreach (var renge in _characterRange)
                    {
                        renge.GetComponent<LibraryItem>().OnText();

                        renge.DOColor(new Color(renge.color.r, renge.color.g, renge.color.b, 1f), 0.8f).OnComplete(() =>
                        {
                        });
                    }
                    OnMeleeLibrary = false;
                    OnRangeLibrary = true;
                });
            }
        }
    }

    
    public void SetSpriteCharactersLibrary(int currentBuyCharacter, CharacterType characterType)
    {
        _currentMaxCharacterMelee = currentBuyCharacter;
        PlayerPrefs.SetInt("CurrentMaxCharacterMelee", _currentMaxCharacterMelee);
        ShowNewCharacterMelee(currentBuyCharacter, characterType);
    }
    public void SetSpriteCharactersLibraryRange(int currentBuyCharacter, CharacterType characterType)
    {
        _currentMaxCharacterRange = currentBuyCharacter;
        PlayerPrefs.SetInt("CurrentMaxCharacterRange", _currentMaxCharacterRange);
        ShowNewCharacterRange(currentBuyCharacter, characterType);

    }

    private void ShowNewCharacterMelee(int numbCharacter, CharacterType characterType)
    {
        ShowNewCharacter = true;
        _newCharacterSound.Play();
        _newlibraryItem.GetComponent<Image>().sprite = _normalCharcterMelee[numbCharacter];
        _newlibraryItem.SetFieldConstructor(_fieldConstructor);
        _newlibraryItem.SetCharacterType(characterType);
        _newCharacterPanel.gameObject.SetActive(true);
        _newCharacterPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutBack).
            OnComplete(() =>
            {
                Invoke("OutNewCharacterMelee", 0.8f);
            });
    }

    private void OffAllCharacters()
    {
        _battleHandler.OffAllCharacters();
    }
    private void OutNewCharacterMelee()
    {
        _newCharacterPanel.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.8f).SetEase(Ease.InBack);
        _newCharacterPanel.transform.DOMove(_libraryBtn.transform.position, 0.8f).OnComplete(() =>
        {
            _newCharacterPanel.gameObject.SetActive(false);
            _newCharacterPanel.transform.position = _startPositionNewCharacterPanel.transform.position;
            ShowNewCharacter = false;
        });
    }
    
    private void ShowNewCharacterRange(int numbCharacter, CharacterType characterType)
    {
        ShowNewCharacter = true;
        _newCharacterSound.Play();
        _newlibraryItem.GetComponent<Image>().sprite = _normalCharcterRange[numbCharacter];
        _newlibraryItem.SetFieldConstructor(_fieldConstructor);
        _newlibraryItem.SetCharacterType(characterType);
        _newCharacterPanel.gameObject.SetActive(true);
        _newCharacterPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutBack).
            OnComplete(() =>
            {
                Invoke("OutNewCharacterRange", 0.8f);
            });
    }

    private void OutNewCharacterRange()
    {
        _newCharacterPanel.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.8f).SetEase(Ease.InBack);
        _newCharacterPanel.transform.DOMove(_libraryBtn.transform.position, 0.8f).OnComplete(() =>
        {
            _newCharacterPanel.gameObject.SetActive(false);
            _newCharacterPanel.transform.position = _startPositionNewCharacterPanel.transform.position;
            ShowNewCharacter = false;
        });
    }
    
    public void SetSpriteCharactersLibrary()
    {
        for (int i = 0; i < _currentMaxCharacterMelee + 1; i++)
        {
            _characterMelee[i].sprite = _normalCharcterMelee[i];
            _characterMelee[i].GetComponent<LibraryItem>().ShowBuyButton();
        }

        for (int i = 0; i < _currentMaxCharacterRange + 1; i++)
        {
            _characterRange[i].sprite = _normalCharcterRange[i];
            _characterRange[i].GetComponent<LibraryItem>().ShowBuyButton();

        }
    }

    public void ClickMeleeBtn()
    {
        _meleeButon.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f);
        _rangeButon.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
    }
    public void ClickRangeBtn()
    {
        _rangeButon.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f);
        _meleeButon.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
    }

    public void SetLibraryControllerInLibraryItem()
    {
        var libraryController = gameObject.GetComponent<LibraryController>();
        foreach (var item in _libraryItems)
        {
            item.SetLibraryController(libraryController);
        }
    }
    
    public void ClickRewardAds(LibraryItem libraryItem)
    {
        if (_characterSeller.TakeCellsFilled)
            return;
            
        var freeCell = _fieldConstructor.GetFreeCell();
        if (freeCell == null)
        {
            _characterSeller.IncludeCellsFilled();
            return;
        }
        
        if (_fieldConstructor.GetFreeCell() == false)
            return;
        
        _libraryItem = libraryItem;
        AdsController.singleton.iAds.RewardedShow((int)AdsRewardType.IdAddCharacterFromLibrary,Reward);
    }
    public void RewardAds()
    {
        _libraryItem._countBuyClick++;
        if (_libraryItem._countBuyClick >= 3)
        {
            _characterSeller.SpawnCharacter(_libraryItem._characterType);
            _libraryItem._countBuyClick = 0;
        }

        _libraryItems = null;
    }
}
