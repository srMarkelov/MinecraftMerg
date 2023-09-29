using System;
using System.Collections;
using System.Collections.Generic;
using Core.Field;
using TMPro;
using UnityEngine;
using YG;
using YG.Example;

public class SaveCloudYandex : ICloud
{
    private static string MoneyInTheStorage = "MoneyInTheStorage";
    private static string PriceMelee = "PriceMelee";
    private static string PriceRange = "PriceRange";

    private TextMeshProUGUI STR;
    private FieldConstructor _fieldConstructor;
    private SaveCloudController _saveCloudController;
    public int Count;

    private int[] characterTypeFOrCell = new int[15];
    private int[] cellWidth = new int[15];
    private int[] cellHeight = new int[15];
    
    
    public void OnEnable()
    {
        YandexGame.GetDataEvent += GetDate;
    }

    public void OnDisable()
    {
        Save();
        YandexGame.GetDataEvent -= GetDate;
    }
    public void Awake()
    {
        if (YandexGame.SDKEnabled)
        {
            GetDate();
        }
    }

    public void SetFieldConstructor(FieldConstructor fieldConstructor)
    {
        _fieldConstructor = fieldConstructor;
    }

    public void Save()
    {
        var moneyInTheStorage = PlayerPrefs.GetFloat(MoneyInTheStorage);
        YandexGame.savesData.MoneyInTheStorage = moneyInTheStorage;
        
        var moneyLetter = PlayerPrefs.GetInt($"{MoneyInTheStorage}GoldConverting");
        YandexGame.savesData.MoneyLetter = moneyLetter;

        var priceMeleeLetter = PlayerPrefs.GetInt($"{PriceMelee}GoldConverting");
        YandexGame.savesData.PriceMeleeLetter = priceMeleeLetter;
        
        var priceRangeLetter = PlayerPrefs.GetInt($"{PriceRange}GoldConverting");
        YandexGame.savesData.PriceRangeLette = priceRangeLetter;
        
        _fieldConstructor.FillAndSaveCharacters();
        
        YandexGame.savesData.CharacterTypeFOrCell = _fieldConstructor.CharacterTypeFOrCell;
        YandexGame.savesData.CellWidth = _fieldConstructor.CellWidth;
        YandexGame.savesData.CellHeight = _fieldConstructor.CellHeight;

        var currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        YandexGame.savesData.currentLevel = currentLevel;
        
        var oldCharacterTypeMelee = PlayerPrefs.GetInt("OldCharacterTypeMelee");
        YandexGame.savesData.OldCharacterTypeMelee = oldCharacterTypeMelee;
        var oldCharacterTypeRange = PlayerPrefs.GetInt("OldCharacterTypeRange");
        YandexGame.savesData.OldCharacterTypeRange = oldCharacterTypeRange;

        var currentMaxCharacterMelee = PlayerPrefs.GetInt("CurrentMaxCharacterMelee");
        YandexGame.savesData.CurrentMaxCharacterMelee = currentMaxCharacterMelee;
        var currentMaxCharacterRange = PlayerPrefs.GetInt("CurrentMaxCharacterRange");
        YandexGame.savesData.CurrentMaxCharacterRange = currentMaxCharacterRange;
        
        var priceMelee = PlayerPrefs.GetFloat(PriceMelee);
        YandexGame.savesData.PriceMelee = priceMelee;
        var priceRange = PlayerPrefs.GetFloat(PriceRange);
        YandexGame.savesData.PriceRange = priceRange;

        PlayerPrefs.GetInt("OldCharacterTypeRange");

        YandexGame.SaveProgress();
    }
    
    public void GetDate()
    {
        var moneyInTheStorage = YandexGame.savesData.MoneyInTheStorage;
        PlayerPrefs.SetFloat(MoneyInTheStorage, moneyInTheStorage);

        var moneyLetter = YandexGame.savesData.MoneyLetter;
        PlayerPrefs.SetInt($"{MoneyInTheStorage}GoldConverting", moneyLetter);
        
        var priceMeleeLetter = YandexGame.savesData.PriceMeleeLetter;
        PlayerPrefs.SetInt($"{PriceMelee}GoldConverting", priceMeleeLetter);
        
        var priceRangeLetter = YandexGame.savesData.PriceRangeLette;
        PlayerPrefs.SetInt($"{PriceRange}GoldConverting", priceRangeLetter);
        

        var priceMelee = YandexGame.savesData.PriceMelee;
        PlayerPrefs.SetFloat(PriceMelee, priceMelee);
        var priceRange = YandexGame.savesData.PriceRange;
        PlayerPrefs.SetFloat(PriceRange, priceRange);
        
        var currentLevel = YandexGame.savesData.currentLevel;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        
        var oldCharacterTypeMelee = YandexGame.savesData.OldCharacterTypeMelee;
        PlayerPrefs.SetInt("OldCharacterTypeMelee", oldCharacterTypeMelee);
        
        var oldCharacterTypeRange = YandexGame.savesData.OldCharacterTypeRange;
        PlayerPrefs.SetInt("OldCharacterTypeRange", oldCharacterTypeRange);

        var currentMaxCharacterMelee = YandexGame.savesData.CurrentMaxCharacterMelee;
        PlayerPrefs.SetInt("CurrentMaxCharacterMelee", currentMaxCharacterMelee);

        var currentMaxCharacterRange = YandexGame.savesData.CurrentMaxCharacterRange;
        PlayerPrefs.SetInt("CurrentMaxCharacterRange", currentMaxCharacterRange);
        

        characterTypeFOrCell = YandexGame.savesData.CharacterTypeFOrCell;
        cellWidth = YandexGame.savesData.CellWidth;
        cellHeight = YandexGame.savesData.CellHeight;
        
        _fieldConstructor.SetCharacter(characterTypeFOrCell);
        _fieldConstructor.SetCellWidth(cellWidth);
        _fieldConstructor.SetCellHeight(cellHeight);
        
        _fieldConstructor.SpawnCharactersOnTheField();
        _saveCloudController.LoadedSave();
        
    }

    public void ResetSaveProgress()
    {
        YandexGame.ResetSaveProgress();
    }

    public void SetSaveCloudController(SaveCloudController saveCloudController)
    {
        _saveCloudController = saveCloudController;
    }
}
