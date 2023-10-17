using System;
using System.Collections;
using System.Collections.Generic;
using Ads;
using Core.Battler;
using Core.Characters;
using Core.Levels;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using YG;
using YG.Example;

public class FinishLevelController : MonoBehaviour
{
    private static string MoneyInTheStorage = "MoneyInTheStorage";
    private static string VictoryReward = "VictoryReward";
    private static string DefeatReward = "DefeatReward";

    private static string PriceMeleeS = "PriceMelee"; // Remove name
    private static string PriceRangeS = "PriceRange";
    
    private static string CurrentWinRewardMoney = "CurrentWinRewardMoney";
    private static string CurrentLoseRewardMoney = "CurrentLoseRewardMoney";
    
    [SerializeField] private FinishLevelView _finishLevelView;
    [SerializeField] private FortunaTrigger _fortunaTrigger;
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private LevelsDatabase _levelsDatabase;
    [SerializeField] private StorageVariable _storageVariable;
    [SerializeField] private CharacterSeller _characterSeller;
    [SerializeField] private MoneyСonverting _moneyСonverting;
    [SerializeField] private UiLevelManager _uiLevelManager;
    [SerializeField] private AdsYandex _adsYandex;

    private NewResultLeaderboard _newResultLeaderboard;
    private LevelData _levelData;
    private int _currentLevel;
    private float _rewardKills;

    private float _currentWinRewardMoney;
    private float _currentLoseRewardMoney;
    public Action restartLevel;

    public void RestartLevel()
    {
        Time.timeScale = 1.3f;
        _rewardKills = 0f;
        _finishLevelView.OffFinishLosePanel();
        _finishLevelView.OffFinishWinPanel();
        StartCoroutine(_uiLevelManager.CheckStorageAndPriceMelee());
        StartCoroutine(_uiLevelManager.CheckStorageAndPriceRange());
        restartLevel.Invoke();
        /*
        AdsController.singleton.iAds.FullscreenShow();
    */
    }

    private void Reward(int idReward)
    {
        if (idReward == (int)AdsRewardType.IdIncreasedGoldReward)
        {
            AdsdReward();
        }
        
    }
    

    private void Update()
    {
        if (_battleHandler.IsWin)
        {
            _currentWinRewardMoney = _levelData.VictoryReward;
            _finishLevelView.SetGoldRewardWinAndLoseText("VictoryReward" );
        }
        if (_battleHandler.IsLose)
        {
            _currentLoseRewardMoney = _levelData.DefeatReward;
            _finishLevelView.SetGoldRewardWinAndLoseText("DefeatReward" );
        }
        /*PlayerPrefs.SetFloat("currentWinRewardMoney", _currentWinRewardMoney);
        PlayerPrefs.SetFloat("currentLoseRewardMoney", _currentLoseRewardMoney);*/
    }

    public void SetNewResultLeaderboard(NewResultLeaderboard newResultLeaderboard)
    {
        _newResultLeaderboard = newResultLeaderboard;
    }
    public void GetRewardLoseAndeWin()
    {
        float VictoryReward = 0;
        VictoryReward = _moneyСonverting.AddMoney("VictoryReward",PriceMeleeS, PriceRangeS);
        PlayerPrefs.SetFloat("VictoryReward", VictoryReward);
        
        VictoryReward = _moneyСonverting.DividingNumber("VictoryReward",2);
        PlayerPrefs.SetFloat("VictoryReward", VictoryReward);


        float DefeatReward = 0;
        DefeatReward = _moneyСonverting.AddMoney("DefeatReward",PriceMeleeS, PriceRangeS);
        PlayerPrefs.SetFloat("DefeatReward", DefeatReward);

        DefeatReward = _moneyСonverting.DividingNumber("DefeatReward",3);
        PlayerPrefs.SetFloat("DefeatReward", DefeatReward);


        var randomPercentageVictory = UnityEngine.Random.Range(2f, 3.2f);
        var randomPercentageDefeat = UnityEngine.Random.Range(1.8f, 2.4f);
        if (_currentLevel == 19 || _currentLevel == 39 ||_currentLevel == 59)
        {
           randomPercentageVictory = UnityEngine.Random.Range(4f, 5.5f);
           randomPercentageDefeat = UnityEngine.Random.Range(3f, 4f);
        }
        _levelData.VictoryReward = _moneyСonverting.Multiplication("VictoryReward", randomPercentageVictory);                    // VictoryReward + VictoryReward / 100 * randomPercentageVictory;

        _levelData.DefeatReward = _moneyСonverting.Multiplication("DefeatReward", randomPercentageDefeat);                // DefeatReward + DefeatReward / 100 * randomPercentageDefeat;

    }
    
    public void AddRewardKillsCharacters(int value)
    {
        _rewardKills += value;
    }
    

    public void GetReward()
    {
        if (_battleHandler.IsWin)
        {
            /*GetRewardLoseAndeWin();*/
            _storageVariable.AddMoney("VictoryReward");
        }

        if (_battleHandler.IsLose)
        {
            /*GetRewardLoseAndeWin();*/
            _storageVariable.AddMoney("DefeatReward");
        }
        _characterSeller.CorrectedPriceAllCharacters();
        RestartLevel();
        _battleHandler.ResetLevel();
        YandexGame.FullscreenShow();
        if (_newResultLeaderboard != null)
        {
            _newResultLeaderboard.NewName();
            _newResultLeaderboard.NewScore();
        }
        InputBlocker.InputLock(this, 1.2f);
    }

    public void GetCurrentLevel(int currentLevel)
    {
        _currentLevel = currentLevel;
        SetCurrentLevel(currentLevel);
    }
    public float GetMultiplicationOfMoney()
    {
        return _fortunaTrigger.GoldMultiplier;
    }

    

    public void SetCurrentLevel(int levelData)
    {
        if (_battleHandler.CheckWinAndLose) return;
        
        _levelData = _levelsDatabase.LevelDatas[levelData];
    }

    private bool dontClickRewardBuyButton;
    public void GetIncreasedReward()
    {
        if (InputBlocker.IsLock())
        {
            return;
        }
        if (dontClickRewardBuyButton)
            return;
        dontClickRewardBuyButton = true;
        InputBlocker.InputLock(this,1.5f);
        Invoke("GetIncreasedRewardInvoke",0.4f);
        /*_finishLevelView.ArrowMove = false;
        if (_newResultLeaderboard != null)
        {
            _newResultLeaderboard.NewName();
            _newResultLeaderboard.NewScore();
        }
        AdsController.singleton.iAds.RewardedShow((int)AdsRewardType.IdIncreasedGoldReward,Reward);*/
        
    }
    
    public void GetIncreasedRewardInvoke()
    {
        _finishLevelView.ArrowMove = false;
        if (_newResultLeaderboard != null)
        {
            _newResultLeaderboard.NewName();
            _newResultLeaderboard.NewScore();
        }
        AdsController.singleton.iAds.RewardedShow((int)AdsRewardType.IdIncreasedGoldReward,Reward);
        dontClickRewardBuyButton = false;
    }
    public void AdsdReward()
    {
        if (_battleHandler.IsWin)
        {
            var add = _moneyСonverting.Multiplication("VictoryReward", GetMultiplicationOfMoney());
            PlayerPrefs.SetFloat("VictoryReward",add);
            _storageVariable.AddMoney("VictoryReward");
            /*if (_currentLevel == 20 || _currentLevel == 40 ||
                _currentLevel == 60 || _currentLevel == 80)
            {
                var addX10 = _moneyСonverting.Multiplication("VictoryReward", 10);
                PlayerPrefs.SetFloat("VictoryReward",addX10);
                _storageVariable.AddMoney("VictoryReward");
            }
            else
            {
                var add = _moneyСonverting.Multiplication("VictoryReward", GetMultiplicationOfMoney());
                PlayerPrefs.SetFloat("VictoryReward",add);
                _storageVariable.AddMoney("VictoryReward");
            }*/
            
        }

        if (_battleHandler.IsLose)
        {
            var add = _moneyСonverting.Multiplication("DefeatReward", GetMultiplicationOfMoney());
            PlayerPrefs.SetFloat("DefeatReward",add);

            _storageVariable.AddMoney("DefeatReward");
            /*if (_currentLevel == 20 || _currentLevel == 40 ||
                _currentLevel == 60 || _currentLevel == 80)
            {
                var add = _moneyСonverting.Multiplication("DefeatReward", 10);
                PlayerPrefs.SetFloat("DefeatReward",add);

                _storageVariable.AddMoney("DefeatReward");
            }
            else
            {
                var add = _moneyСonverting.Multiplication("DefeatReward", 10);
                PlayerPrefs.SetFloat("DefeatReward",add);

                _storageVariable.AddMoney("DefeatReward");
            }*/
            
        }
        _characterSeller.CorrectedPriceAllCharacters();
        RestartLevel();
        _battleHandler.ResetLevel();
    }
}
