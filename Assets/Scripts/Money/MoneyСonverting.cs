using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MoneyСonverting : MonoBehaviour
{
    private static string MoneyInTheStorage = "MoneyInTheStorage";

    private static string PriceMeleeS = "PriceMelee"; // Remove name
    private static string PriceRangeS = "PriceRange";
    
    private static string VictoryReward = "VictoryReward";
    private static string DefeatReward = "DefeatReward";
    
    
    private static string Units = "1";
    private static string Dozens = "10";
    private static string Hundreds = "100";

    private static string Thousands = "1000";
    private static string ThousandsTens = "10000";
    private static string ThousandsHundreds = "100000";
    private static string ThousandsMaxValue = "999999";

    private static string Million = "1000000";
    private static string MillionsTens = "10000000";
    private static string MillionsHundreds = "100000000";
    private static string MillionsMaxValue = "999999999";

    private static string Billions = "1000000000";
    private static string BillionsTens = "10000000000";
    private static string BillionsHundreds = "100000000000";
    private static string BillionsMaxValue = "999999999999";
    
    private static string Trillions = "1000000000000";
    private static string TrillionsTens = "10000000000000";
    private static string TrillionsHundreds = "100000000000000";
    private static string TrillionsMaxValue = "999999999999999";
    
    private int Gold;
    private int GoldType;

    private string _moneyLetterStorage;
    private string _moneyStorage;
    
    private string _priceMeleeLetter;
    private string _priceRangeLetter;
    
    private float _priceRange;
    private float _priceMelee;
    
    
    
    Dictionary<int,string> letterDictionary = new Dictionary<int,string>()
    {
        {0,""},
        {1,"K"},
        {2,"KK"},
        {3,"B"},
        {4,"KB"},
        {5,"BB"},
        {6,"T"},
        {7,"KT"},
        {8,"BT"},
        {9,"TT"},
        {10,"A"},
        {11,"AK"},
        {12,"AB"},
        {13,"AD"},
        {14,"AT"},
    };

    private void Update()
    {
        var a = PlayerPrefs.GetInt($"{PriceMeleeS}GoldConverting");
        var b = PlayerPrefs.GetInt($"{PriceRangeS}GoldConverting");
        var c = PlayerPrefs.GetInt($"{MoneyInTheStorage}GoldConverting");
        
        /*Debug.Log($"M || {a}");
        Debug.Log($"R || {b}");
        Debug.Log($"S || {c}");*/
    }

    public void RestartStorage()
    {
        PlayerPrefs.SetInt($"{MoneyInTheStorage}GoldConverting",0);
        PlayerPrefs.SetInt($"{PriceMeleeS}GoldConverting",0);
        PlayerPrefs.SetInt($"{PriceRangeS}GoldConverting",0);
        PlayerPrefs.SetInt("VictoryRewardGoldConverting",0);
        PlayerPrefs.SetInt("DefeatRewardGoldConverting",0);
        PlayerPrefs.SetInt("sumGoldConverting",0);
        PlayerPrefs.SetInt("_sumGoldConverting",0);
        PlayerPrefs.GetFloat(PriceRangeS,200);
        PlayerPrefs.GetFloat(PriceMeleeS,200);
        _moneyLetterStorage = string.Empty;
    }

    public float Multiplication(string indexNumber, float  multiplier)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexNumber}GoldConverting");

        firstNumber = firstNumber * multiplier;
        var numberReturn = GoldConverting(indexNumber, firstNumber);
        return numberReturn;
    }
    public float DividingNumber(string indexFirstNumber, int divider)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        firstNumber = firstNumber / divider;
        
        int numberInt = (int)firstNumber;
        if (numberInt <= 0)
        {
            firstNumber *= 1000.0f;
            if (letterFirstNumber != 0)
            {
                letterFirstNumber--;
            }

            if (indexFirstNumber == MoneyInTheStorage)
            {
                PlayerPrefs.SetInt($"{MoneyInTheStorage}GoldConverting",letterFirstNumber);
            }
            if (indexFirstNumber == PriceMeleeS)
            {
                PlayerPrefs.SetInt($"{PriceMeleeS}GoldConverting",letterFirstNumber);
            }
            if (indexFirstNumber == PriceRangeS)
            {
                PlayerPrefs.SetInt($"{PriceRangeS}GoldConverting",letterFirstNumber);
            }
            if (indexFirstNumber == VictoryReward)
            {
                PlayerPrefs.SetInt($"{VictoryReward}GoldConverting",letterFirstNumber);
            }
            if (indexFirstNumber == DefeatReward)
            {
                PlayerPrefs.SetInt($"{DefeatReward}GoldConverting",letterFirstNumber);
            }
        }

        return firstNumber;
    }
    
    public float AddMoney(string sum, string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");
        int letterNumber = 0;
        
        
        if (letterFirstNumber < letterSecondNumber)
        {
            firstNumber /= 1000.0f;
            firstNumber += secondNumber;
            letterFirstNumber = letterSecondNumber;
            PlayerPrefs.SetInt($"{sum}GoldConverting",letterFirstNumber);
        }
        else if (letterFirstNumber > letterSecondNumber)
        {
            secondNumber /= 1000.0f;
            firstNumber += secondNumber;
            PlayerPrefs.SetInt($"{sum}GoldConverting",letterFirstNumber);
        }
        else if (letterFirstNumber == letterSecondNumber)
        {
            firstNumber += secondNumber;
            PlayerPrefs.SetInt($"{sum}GoldConverting",letterFirstNumber);
        }
        
        
        /*if (letterFirstNumber <= letterSecondNumber)
        {
            letterNumber = letterSecondNumber;
            PlayerPrefs.SetInt($"{sum}GoldConverting",letterNumber);
        }*/
        
        var numberReturn = GoldConverting(sum, firstNumber);
        return numberReturn;
    }
    public float AddStorageMoney(string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");
        if (letterFirstNumber < letterSecondNumber)
        {
            firstNumber /= 1000.0f;
            firstNumber += secondNumber;
            letterFirstNumber = letterSecondNumber;
            PlayerPrefs.SetInt($"{indexFirstNumber}GoldConverting",letterFirstNumber);
        }
        else if (letterFirstNumber > letterSecondNumber)
        {
            secondNumber /= 1000.0f;
            firstNumber += secondNumber;
            PlayerPrefs.SetInt($"{indexFirstNumber}GoldConverting",letterFirstNumber);
        }
        else if (letterFirstNumber == letterSecondNumber)
        {
            firstNumber += secondNumber;
            PlayerPrefs.SetInt($"{indexFirstNumber}GoldConverting",letterFirstNumber);
        }
        
        
        var numberReturn = GoldConverting(indexFirstNumber, firstNumber);
        return numberReturn;
    }
    
    
    public float GoldConverting(string index, float gold)
    {
        string oldGold = gold.ToString("000");
        float newGold = 0;
        var scoreGoldConverting = PlayerPrefs.GetInt($"{index}GoldConverting");
        
        if (oldGold.Length > 3)
        {
            while (gold >= 1000.0f)
            {
                
                newGold = gold / 1000.0f;
                gold = newGold;
                scoreGoldConverting++;
            }
            PlayerPrefs.SetInt($"{index}GoldConverting",scoreGoldConverting);
        }
        else
        {
            newGold = gold;
            PlayerPrefs.SetInt($"{index}GoldConverting",scoreGoldConverting);
        }
        PlayerPrefs.SetFloat(index, newGold);

        return newGold;
    }
    
    
    public string GetMoney(string index)
    {
        var moneyLetter = PlayerPrefs.GetInt($"{index}GoldConverting");
        var money = PlayerPrefs.GetFloat(index);
        return ((int)money).ToString() + letterDictionary[moneyLetter];
    }
    public string GetStorageMoney(float moneyStorage)
    {
        var moneyLetter = PlayerPrefs.GetInt($"{MoneyInTheStorage}GoldConverting");
        if (moneyLetter == 0)
        {
            return ((int)moneyStorage).ToString();

        }
        return ((int)moneyStorage).ToString() + letterDictionary[moneyLetter];
    }


    
    public bool Equal(string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");

        if (letterFirstNumber == letterSecondNumber)
        {
            if (Math.Abs(firstNumber - secondNumber) < 0.1f)
            {
                return true;
            }
        }
        
        return false;
    }
    
    public bool LessOrEqual(string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");

        
        if (letterFirstNumber < letterSecondNumber)
        {
            return true;
        }
        if (letterFirstNumber == letterSecondNumber)
        {
            if (firstNumber <= secondNumber)
            {
                return true;
            }
        }
        return false;
    }
    public bool MoreOrEqual(string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");

        if (letterFirstNumber > letterSecondNumber)
        {
            return true;
        }
        if (letterFirstNumber == letterSecondNumber)
        {
            if (firstNumber >= secondNumber)
            {
                return true;
            }
        }
        
        return false;
    }
    
    public bool More(string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");

        if (letterFirstNumber > letterSecondNumber)
        {
            return true;
        }
        if (letterFirstNumber == letterSecondNumber)
        {
            if (firstNumber > secondNumber)
            {
                return true;
            }
        }
        return false;
    }
    
    public bool Less(string indexFirstNumber, string indexSecondNumber)
    {
        var firstNumber = PlayerPrefs.GetFloat(indexFirstNumber);
        var letterFirstNumber =  PlayerPrefs.GetInt($"{indexFirstNumber}GoldConverting");

        var secondNumber = PlayerPrefs.GetFloat(indexSecondNumber);
        var letterSecondNumber = PlayerPrefs.GetInt($"{indexSecondNumber}GoldConverting");

        if (letterFirstNumber < letterSecondNumber)
        {
            return true;
        }
        if (letterFirstNumber == letterSecondNumber)
        {
            if (firstNumber < secondNumber)
            {
                return true;
            }
        }
        return false;
    }
    
    
    public float RemoveMoney(float moneyStorage, string indexRemoveMoney)
    {
        var scoreLetterStore = PlayerPrefs.GetInt($"{MoneyInTheStorage}GoldConverting");
        var scoreLetterRemove = PlayerPrefs.GetInt($"{indexRemoveMoney}GoldConverting");
        var removeMoney = PlayerPrefs.GetFloat(indexRemoveMoney);
        if (scoreLetterStore - scoreLetterRemove == 1)
        {
            removeMoney /= 1000f;
        }
        if (scoreLetterStore - scoreLetterRemove == 2)
        {
            removeMoney /= 1000f;
        }
        
        
        moneyStorage -= removeMoney;
        int moneyStorageInt = (int)moneyStorage;
        if (moneyStorageInt <= 0)
        {
            moneyStorage *= 1000.0f;
            if (scoreLetterStore != 0)
            {
                scoreLetterStore--;
            }
            PlayerPrefs.SetInt($"{MoneyInTheStorage}GoldConverting",scoreLetterStore);
        }
        _moneyLetterStorage = letterDictionary[scoreLetterStore];

        /*PlayerPrefs.SetFloat($"MoneyInTheStorage",moneyStorage);*/


        return moneyStorage;
    }
    
    public void SetPriceMeleeAndRangeCharacter(float priceMelee, float priceRange)
    {
        
    }
    private void OnEnable()
    {
        _moneyStorage = PlayerPrefs.GetString("MoneyStorage");
        _moneyLetterStorage = PlayerPrefs.GetString("MoneyLitterStorage");
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("MoneyStorage", _moneyStorage);
        PlayerPrefs.SetString("MoneyLitterStorage", _moneyLetterStorage);
    }


    
}
