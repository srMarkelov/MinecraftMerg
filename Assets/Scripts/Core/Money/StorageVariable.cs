using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class StorageVariable : MonoBehaviour
{
    private static string MoneyInTheStorage = "MoneyInTheStorage";
    
    private static string PriceMeleeS = "PriceMelee"; // Remove name
    private static string PriceRangeS = "PriceRange";
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _moneyTextBackground;
    [SerializeField] private FormattingMoney _formattingMoney;
    [SerializeField] private MoneyСonverting _moneyСonverting;
    [SerializeField] private UiLevelManager _uiLevelManager;

    
    private string _moneyInTheStorageStr;

    
    private float _moneyInTheStorage;
    private float _money;
    
    private int _thousands;
    private int _millions;
    private int _billions;
    private int _trillions;
    private int _quadrillions;
    private int _quintillions;
    private int _sextillions;

    private string indexMoney;
    

    public void TESTIK()
    {
        var a = PlayerPrefs.GetString("TEST");
        
        a += 200;
        /*var b = _moneyСonverting.GoldConverting(a, "TEST");*/
        Debug.Log( $"RESULT || {a}");
    }

    public void StartGame()
    {
        _moneyInTheStorage = PlayerPrefs.GetFloat(MoneyInTheStorage);
        _moneyText.text = _moneyСonverting.GetStorageMoney(_moneyInTheStorage);
        _moneyTextBackground.text = _moneyСonverting.GetStorageMoney(_moneyInTheStorage);
        StartCoroutine(_uiLevelManager.CheckStorageAndPriceMelee());
        StartCoroutine(_uiLevelManager.CheckStorageAndPriceRange());
    }
    
    public void Add1000Gold()
    {
        AddMoney("PriceRange");
    }

    public void Remove1000Gold()
    {
        RemoveMoney("PriceRange");
    }

    public void SetStartCountMoney(int startMoney)
    {
        /*
        PlayerPrefs.SetString(MoneyInTheStorage,"400");
        */
        PlayerPrefs.SetFloat(MoneyInTheStorage,startMoney);
        _moneyСonverting.RestartStorage();
        /*PlayerPrefs.SetInt(MoneyInTheStorage,startMoney);*/
    }
    

    public float GetMoneyInTheStorage()
    {
        return PlayerPrefs.GetFloat(MoneyInTheStorage);
    }

    public void AddMoney(string indexAddMoney)
    {
        float summ = 0;
        PlayerPrefs.SetFloat("summ", summ);
        summ = _moneyСonverting.AddStorageMoney(MoneyInTheStorage, indexAddMoney);
        _moneyInTheStorage = summ;

        PlayerPrefs.SetFloat(MoneyInTheStorage,_moneyInTheStorage);

        _moneyText.text = _moneyСonverting.GetStorageMoney(_moneyInTheStorage);
        _moneyTextBackground.text = _moneyСonverting.GetStorageMoney(_moneyInTheStorage);
        SaveCloudController.singleton.ICloud.Save();
    }
    public void RemoveMoney(string indexRemoveMoney)
    {
        var sumInStorage  = _moneyСonverting.RemoveMoney(_moneyInTheStorage ,indexRemoveMoney);
        _moneyInTheStorage = sumInStorage;
        PlayerPrefs.SetFloat(MoneyInTheStorage,_moneyInTheStorage);
        _moneyText.text = _moneyСonverting.GetStorageMoney(_moneyInTheStorage);
        _moneyTextBackground.text = _moneyСonverting.GetStorageMoney(_moneyInTheStorage);
    }
}



