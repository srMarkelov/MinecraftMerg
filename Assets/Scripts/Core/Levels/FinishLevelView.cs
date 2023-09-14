using System;
using System.Collections;
using System.Collections.Generic;
using Core.Characters;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class FinishLevelView : MonoBehaviour
{
    [SerializeField] private FortunaTrigger _fortunaWinTrigger;
    [SerializeField] private StorageVariable _storageVariable;
    [SerializeField] private FormattingMoney _formattingMoney;
    [SerializeField] private MoneyСonverting _moneyСonverting;

    [SerializeField] private CharacterSeller _characterSeller;
    [SerializeField] private Button _rewardBtn;
    [SerializeField] private Image _arrowWin;
    [SerializeField] private TextMeshProUGUI _GoldMultiplierWinText;
    [SerializeField] private TextMeshProUGUI _GoldRewardText;
    [SerializeField] private GameObject _GoldRewardTextGO;
    [SerializeField] private TextMeshProUGUI _GoldRewardTextFromButton;
    [SerializeField] private TextMeshProUGUI _GoldMultiplierLoseText;
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private GameObject _finishPanelBacground;
    [SerializeField] private GameObject _FinishWinPanel;
    [SerializeField] private GameObject _FinishLosePanel;
    [SerializeField] private GameObject _fortuna;
    [SerializeField] private GameObject _plateWin;
    [SerializeField] private GameObject _plateLose;
    [SerializeField] private int _checkPont;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _loseSound;
    private bool _onLeft;
    private bool _onRight;
    private bool accessOn;
    private bool _onFinishPanel;
    public bool OnFinishPanel => _onFinishPanel;


    private void OnEnable()
    {
        RestartPlayerPrefs();
    }

    public void RestartPlayerPrefs()
    {
        PlayerPrefs.SetInt("CountMelee",0);
        PlayerPrefs.SetInt("CountRange",0);
        PlayerPrefs.SetInt("OldLevel",0);
        _storageVariable.SetStartCountMoney(400);
        /*
        PlayerPrefs.SetInt("CurrentLevel", 0);
        */
        PlayerPrefs.SetFloat("PriceMelee",200f);
        PlayerPrefs.SetFloat("PriceRange",200f);
        PlayerPrefs.SetInt("OldCharacterTypeMelee",0);
        PlayerPrefs.SetInt("OldCharacterTypeRange",0);
        PlayerPrefs.SetInt("CurrentMaxCharacterMelee", 0);
        PlayerPrefs.SetInt("CurrentMaxCharacterRange", 0);
        PlayerPrefs.SetInt("StartGame", 0);

        for (int i = 0; i < 15; i++)
        {
            PlayerPrefs.SetInt($"SaveCellWidth{i}",0);
            PlayerPrefs.SetInt($"SaveCellHeight{i}",0);
            PlayerPrefs.SetInt($"CharacterType{i}",0);
        }

        YandexGame.savesData.CharacterTypeFOrCell = new int[15];
        YandexGame.savesData.CellWidth = new int[15];
        YandexGame.savesData.CellHeight = new int[15];
        if (accessOn)
        {
            YandexGame.ResetSaveProgress();
        }
    }

    public void RestartCloudSave()
    {
        accessOn = true;
    }
    
    public void SetCheckPoint()
    {
        PlayerPrefs.SetInt("CurrentLevel", _checkPont);
    }
    public void SetGoldRewardWinAndLoseText(string goldReward, bool f)
    {
        _GoldRewardText.text = goldReward;
    }
    public void SetGoldRewardWinAndLoseText(string goldRewardIdex)
    {
        _GoldRewardText.text = _moneyСonverting.GetMoney(goldRewardIdex);
        _GoldRewardTextFromButton.text = _moneyСonverting.GetMoney(goldRewardIdex);
    }
    
    public void StartGame()
    {
        _currentLevel.text = $"{PlayerPrefs.GetInt("CurrentLevel")+1}";
    }
    
    private void Update()
    {
        _GoldMultiplierWinText.text = $"x{_fortunaWinTrigger.GoldMultiplier.ToString()}";
        StopArrow();
    }

    private void StopArrow()
    {
        if (ArrowMove == false)
        {
            var eulerAnglesZ =_arrowWin.transform.rotation.eulerAngles.z;
            _arrowWin.transform.DORotate(new Vector3(0f, 0f, eulerAnglesZ),0.1f);
        }
    }
    
    
    
    public void SetCurrentLevelText(int level)
    {
        _currentLevel.text = $"{level+1}";
    }
    
    public void OnFinishWinPanel()
    {
        Invoke("OnFinishWinPanelInvoke",2f);
    }

    public void OnFinishWinPanelInvoke()
    {
        _onFinishPanel = true;
        _winSound.Play();
        _finishPanelBacground.SetActive(true);
        ArrowMove = true;
        

        var finishPanelColor = _finishPanelBacground.transform.GetComponent<Image>();
        _finishPanelBacground.transform.GetComponent<Image>()
            .DOColor(new Color(finishPanelColor.color.r,finishPanelColor.color.g,finishPanelColor.color.b,0.85f), 0.2f)
            .SetLink(gameObject);

        _GoldRewardText.gameObject.SetActive(true);
        _GoldRewardTextGO.SetActive(true);
        _FinishWinPanel.SetActive(true);
        _fortuna.SetActive(true);
        _rewardBtn.gameObject.SetActive(true);
        _FinishWinPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f).
            SetEase(Ease.OutBack).OnComplete(() =>
            {
                _plateWin.SetActive(true);
                _plateWin.transform.DOScale(new Vector3(1f, 1f, 1f), 0.0f).
                    SetEase(Ease.OutBack).SetLink(gameObject);
                StartArrow();
            }).SetLink(gameObject);
        
        _fortuna.transform.DOScale(new Vector3(0.9f,0.9f, 0.9f), 0.8f).
            SetEase(Ease.OutBack).SetLink(gameObject);
            
    }
    public void OnFinishLosePanel()
    {
        Invoke("OnFinishLosePanelInvoke",2f);

    }
    public void OnFinishLosePanelInvoke()
    {

        _onFinishPanel = true;

        _loseSound.Play();
        _finishPanelBacground.SetActive(true);
        ArrowMove = true;

        var finishPanelColor = _finishPanelBacground.transform.GetComponent<Image>();
        _finishPanelBacground.transform.GetComponent<Image>()
            .DOColor(new Color(finishPanelColor.color.r,finishPanelColor.color.g,finishPanelColor.color.b,0.85f), 0.2f)
            .SetLink(gameObject);

        _GoldRewardText.gameObject.SetActive(true);
        _FinishLosePanel.SetActive(true);
        _fortuna.SetActive(true);
        _rewardBtn.gameObject.SetActive(true);
        _FinishLosePanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f).
            SetEase(Ease.OutBack).OnComplete(() =>
            {
                _plateLose.SetActive(true);
                _plateLose.transform.DOScale(new Vector3(1f, 1f, 1f), 0.0f).
                    SetEase(Ease.OutBack).SetLink(gameObject);
                StartArrow();

            }).SetLink(gameObject);
        
        _fortuna.transform.DOScale(new Vector3(0.9f,0.9f, 0.9f), 0.8f).
            SetEase(Ease.OutBack).SetLink(gameObject);
    }
    
    public void OffFinishWinPanel()
    {
        Invoke("FalseBoolOnFinishPanel", 0.7f);
        var finishPanelColor = _finishPanelBacground.transform.GetComponent<Image>();
        _finishPanelBacground.transform.GetComponent<Image>()
            .DOColor(new Color(finishPanelColor.color.r,finishPanelColor.color.g,finishPanelColor.color.b,0.0f), 1f)
            .SetLink(gameObject);
        
        _plateWin.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.9f).
            SetEase(Ease.InBack).OnComplete(() =>
            {
                _plateWin.SetActive(false);
            }).SetLink(gameObject);
        
        _FinishWinPanel.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.8f).
            SetEase(Ease.InBack).OnComplete(() =>
            {
                _GoldRewardText.gameObject.SetActive(false);
                _FinishWinPanel.SetActive(false);
                _fortuna.SetActive(false);
                _rewardBtn.gameObject.SetActive(false);
                _finishPanelBacground.gameObject.SetActive(false);
            }).SetLink(gameObject);
        _fortuna.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.6f).
            SetEase(Ease.InBack).OnComplete(() =>
            {
                _fortuna.SetActive(false);

            }).SetLink(gameObject);
    }
    public void OffFinishLosePanel()
    {
        Invoke("FalseBoolOnFinishPanel", 0.7f);

        var finishPanelColor = _finishPanelBacground.transform.GetComponent<Image>();
        _finishPanelBacground.transform.GetComponent<Image>()
            .DOColor(new Color(finishPanelColor.color.r,finishPanelColor.color.g,finishPanelColor.color.b,0.0f), 1f)
            .SetLink(gameObject);
        
        _plateLose.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.9f).
            SetEase(Ease.InBack).OnComplete(() =>
            {
                _plateLose.SetActive(false);
            }).SetLink(gameObject);
        
        _FinishLosePanel.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.8f).
            SetEase(Ease.InBack).OnComplete(() =>
            {
                _GoldRewardText.gameObject.SetActive(false);
                _FinishLosePanel.SetActive(false);
                _rewardBtn.gameObject.SetActive(false);
                _finishPanelBacground.gameObject.SetActive(false);
            }).SetLink(gameObject);
        _fortuna.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.6f).
            SetEase(Ease.InBack).OnComplete(() =>
            {
                _fortuna.SetActive(false);

            }).SetLink(gameObject);
    }

    private void FalseBoolOnFinishPanel()
    {
        _onFinishPanel = false;
    }

    public bool ArrowMove;
    public void ArrowWinMoveLeft()
    {
        if (ArrowMove == false)
        {
            return;
        }
        _arrowWin.transform.DORotate(new Vector3(0, 0, 80f),2f).SetEase(Ease.Flash).OnComplete(() =>
        {
            ArrowWinMoveRight();
        }).SetLink(gameObject);
    }
    public void ArrowWinMoveRight()
    {
        if (ArrowMove == false)
        {
            return;
        }
        _arrowWin.transform.DORotate(new Vector3(0, 0, -80f),2f).SetEase(Ease.Flash).OnComplete(() =>
        {
            ArrowWinMoveLeft();
        }).SetLink(gameObject);
    }
    
    private void StartArrow()
    {
        _arrowWin.transform.DORotate(new Vector3(0, 0, 80f),1f).SetEase(Ease.Flash).OnComplete(() =>
        {
            ArrowWinMoveRight();
        }).SetLink(gameObject);
    }
}
