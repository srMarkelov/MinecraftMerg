/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GoldConversion : MonoBehaviour
{
    private static string Units = "1";
    private static string Dozens = "10";
    private static string Hundreds = "100";

    private static string Thousands = "1000";
    private static string ThousandsMaxValue = "999999";
    private static string TensOfThousands = "10000";
    private static string HundredsOfThousands = "100000";

    private static string Million = "1000000";
    private static string MillionsMaxValue = "999999999";
    private static string TensOfMillions = "10000000";
    private static string HundredsOfMillions = "100000000";

    private static string Billions = "1000000000";
    private static string BillionsMaxValue = "999999999999";
    private static string TensOfBillions = "10000000000";
    private static string HundredsOfBillions = "100000000000";

    private static string TrillionsMaxValue = "1000000000000";
    private static string TensOfTrillions = "100000000000000";
    private static string HundredsOfTrillions = "100000000000000";

    private static string QuadrillionsMaxValue = "1000000000000000";
    private static string TensOfQuadrillions = "10000000000000000";
    private static string HundredsOfQuadrillions = "100000000000000000";

    private static string QuintillionsMaxValue = "1000000000000000000";
    private static string TensOfQuintillions = "10000000000000000000";
    private static string HundredsOfQuintillions = "100000000000000000000";

    private static string SextillionsMaxValue = "1000000000000000000000";
    private static string TensOfSextillions = "10000000000000000000000";
    private static string HundredsOfSextillions = "100000000000000000000000";


    /*private int _units;
    private int _dozens;
    private int _hundreds;

    private int _thousands;
    private int _tensOfThousands;
    private int _hundredsOfThousands;

    private int _millions;
    private int _tensOfMillions;
    private int _hundredsOfMillions;

    private int _billions;
    private int _tensOfBillions;
    private int _hundredsOfBillions;

    private int _trillions;
    private int _tensOfTrillions;
    private int _hundredsOfTrillions;

    private int _quadrillions;
    private int _tensOfQuadrillions;
    private int _hundredsOfQuadrillions;

    private int _quintillions;
    private int _tensOfQuintillions;
    private int _hundredsOfQuintillions;

    private int _sextillions;
    private int _tensOfSextillions;
    private int _hundredsOfSextillions;#1#




    private void Update()
    {
        /*PlayerPrefs.SetInt("MoneyInTheStorage", 0);#1#
        /*Debug.Log(PlayerPrefs.GetInt("MoneyInTheStorage"));#1#
    }

    public string ParsGold(string gold, string index)
    {
        /*int _units = 0;
        int _dozens = 0;
        int _hundreds = 0;
        int _thousands = 0;
        int _millions = 0;#1#
        int _billions = 0;
        int _trillions = 0;
        int _quadrillions = 0;
        int _quintillions = 0;
        int _sextillions = 0;

        if (gold.Length <= BillionsMaxValue.Length)
        {
            if (gold.Length == 12)
            {
                _billions = int.Parse(gold.Substring(0, 3));
                PlayerPrefs.SetInt($"{index}Billions", _billions);
                gold = gold.Remove(0, 3);
            }
            if (gold.Length == 11)
            {
                _billions = int.Parse(gold.Substring(0, 2));
                PlayerPrefs.SetInt($"{index}Billions", _billions);
                gold = gold.Remove(0, 2);
            }
            if (gold.Length == 10)
            {
                _billions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Billions", _billions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Billions", 0);

        }
        if (gold.Length <= MillionsMaxValue.Length)
        {
            if (gold.Length == 9)
            {
                _millions = int.Parse(gold.Substring(0, 3));
                PlayerPrefs.SetInt($"{index}Millions", _millions);
                gold = gold.Remove(0, 3);
            }
            else if (gold.Length == 8)
            {
                _millions = int.Parse(gold.Substring(0, 2));
                PlayerPrefs.SetInt($"{index}Millions", _millions);
                gold = gold.Remove(0, 2);
            }
            else if (gold.Length == 7)
            {
                _millions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Millions", _millions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Millions", 0);
        }

        if (gold.Length <= ThousandsMaxValue.Length)
        {
            if (gold.Length == 6)
            {
                _thousands = int.Parse(gold.Substring(0, 3));
                PlayerPrefs.SetInt($"{index}Thousands", _thousands);
                gold = gold.Remove(0, 3);
            }
            else if (gold.Length == 5)
            {
                _thousands = int.Parse(gold.Substring(0, 2));
                PlayerPrefs.SetInt($"{index}Thousands", _thousands);
                gold = gold.Remove(0, 2);
            }
            else if (gold.Length == 4)
            {
                _thousands = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Thousands", _thousands);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Thousands", 0);
        }

        if (gold.Length == Hundreds.Length)
        {
            _hundreds = int.Parse(gold.Substring(0, 1));
            PlayerPrefs.SetInt($"{index}Hundreds", _hundreds);
            gold = gold.Remove(0, 1);
        }
        else PlayerPrefs.SetInt($"{index}Hundreds", 0);

        if (gold.Length == Dozens.Length)
        {
            _dozens = int.Parse(gold.Substring(0, 1));
            PlayerPrefs.SetInt($"{index}Dozens", _dozens);
            gold = gold.Remove(0, 1);
        }
        else PlayerPrefs.SetInt($"{index}Dozens", 0);

        if (gold.Length == Units.Length)
        {
            _units = int.Parse(gold.Substring(0, 1));
            PlayerPrefs.SetInt($"{index}Units", _units);
            gold = gold.Remove(0, 1);
        }
        else PlayerPrefs.SetInt($"{index}Units", 0);

        var ReternString = RestoreNumbers($"{_billions}{_millions}{_millions}{_thousands}{_hundreds}{_dozens}{(_units)}");
        return ReternString;
    }

    public string AddMoney(string storageIndex, string addMoneyIndex)
    {
        int BillionsStorage = PlayerPrefs.GetInt($"{storageIndex}Billions",0);
        int MillionsStorage = PlayerPrefs.GetInt($"{storageIndex}Millions",0);
        int ThousandsStorage = PlayerPrefs.GetInt($"{storageIndex}Thousands", 0);
        int HundredsStorage = PlayerPrefs.GetInt($"{storageIndex}Hundreds", 0);
        int DozensStorage = PlayerPrefs.GetInt($"{storageIndex}Dozens", 0);
        int UnitsStorage = PlayerPrefs.GetInt($"{storageIndex}Units", 0);

        int BillionsAdd = PlayerPrefs.GetInt($"{addMoneyIndex}Billions",0);
        int MillionsAdd = PlayerPrefs.GetInt($"{addMoneyIndex}Millions",0);
        int ThousandsAdd = PlayerPrefs.GetInt($"{addMoneyIndex}Thousands",0);
        int HundredsAdd = PlayerPrefs.GetInt($"{addMoneyIndex}Hundreds",0);
        int DozensAdd = PlayerPrefs.GetInt($"{addMoneyIndex}Dozens",0);
        int UnitsAdd = PlayerPrefs.GetInt($"{addMoneyIndex}Units",0);

        while (UnitsAdd != 0)
        {
            UnitsStorage++;
            UnitsAdd--;

            if (UnitsStorage.ToString().Length >= Dozens.Length)
            {
                DozensStorage++;
                UnitsStorage = 0;
            }
        }

        while (DozensAdd != 0)
        {
            DozensStorage++;
            DozensAdd--;
            if (DozensStorage.ToString().Length >= Dozens.Length)
            {
                HundredsStorage++;
                DozensStorage = 0;
            }
        }

        while (HundredsAdd != 0)
        {
            HundredsStorage++;
            HundredsAdd--;
            if (HundredsStorage.ToString().Length > Hundreds.Length)
            {
                ThousandsStorage++;
                HundredsStorage = 0;
            }
        }

        while (ThousandsAdd != 0)
        {
            ThousandsStorage++;
            ThousandsAdd--;
            if (ThousandsStorage.ToString().Length > Hundreds.Length)
            {
                MillionsStorage++;
                ThousandsStorage = 0;
            }
        }
        while (MillionsAdd != 0)
        {
            MillionsStorage++;
            MillionsAdd--;
            if (MillionsStorage.ToString().Length > Hundreds.Length)
            {
                BillionsStorage++;
                MillionsStorage = 0;
            }
        }

        /*if (DozensStorage != 0)
        {
            DozensStorage *= 10;
        }
        if (HundredsStorage != 0)
        {
            HundredsStorage *= 100;
        }
        if (ThousandsStorage != 0)
        {
            ThousandsStorage *= 100;
        }
        if (MillionsStorage != 0)
        {
            MillionsStorage *= 100;
        }
        if (BillionsStorage != 0)
        {
            BillionsStorage *= 100;
        }#1#
        
        
        
        var ReternString = RestoreNumbers($"{BillionsStorage.ToString("D3")}{MillionsStorage.ToString("D3")}" +
                                          $"{ThousandsStorage.ToString("D3")}{HundredsStorage}{DozensStorage}{UnitsStorage}");

        ParsGold(ReternString, storageIndex);
        return ReternString;
        PlayerPrefs.SetString(storageIndex, ReternString);
    }

    public string RemoveMoney(string storageIndex, string removeMoney)
    {
        int BillionsStorage = PlayerPrefs.GetInt($"{storageIndex}Billions",0);
        int MillionsStorage = PlayerPrefs.GetInt($"{storageIndex}Millions");
        int ThousandsStorage = PlayerPrefs.GetInt($"{storageIndex}Thousands", 0);
        int HundredsStorage = PlayerPrefs.GetInt($"{storageIndex}Hundreds", 0);
        int DozensStorage = PlayerPrefs.GetInt($"{storageIndex}Dozens", 0);
        int UnitsStorage = PlayerPrefs.GetInt($"{storageIndex}Units", 0);

        int BillionsRemove = PlayerPrefs.GetInt($"{removeMoney}Billions");
        int MillionsRemove = PlayerPrefs.GetInt($"{removeMoney}Millions");
        int ThousandsRemove = PlayerPrefs.GetInt($"{removeMoney}Thousands");
        int HundredsRemove = PlayerPrefs.GetInt($"{removeMoney}Hundreds");
        int DozensRemove = PlayerPrefs.GetInt($"{removeMoney}Dozens");
        int UnitsRemove = PlayerPrefs.GetInt($"{removeMoney}Units");

        UnitsStorage -= UnitsRemove;
        DozensStorage -= DozensRemove;
        HundredsStorage -= HundredsRemove;
        ThousandsStorage -= ThousandsRemove;
        MillionsStorage -= MillionsRemove;
        BillionsStorage -= BillionsRemove;

        if (UnitsStorage < 0)
        {
            DozensStorage--;
            UnitsStorage += 10;
        }

        if (DozensStorage < 0)
        {
            HundredsStorage--;
            DozensStorage += 10;
        }

        if (HundredsStorage < 0)
        {
            ThousandsStorage--;
            HundredsStorage += 10;
        }

        if (ThousandsStorage < 0)
        {
            MillionsStorage--;
            ThousandsStorage += 1000;
        }
        if (MillionsStorage < 0)
        {
            BillionsStorage--;
            MillionsStorage += 1000;
        }

        var ReternString = RestoreNumbers($"{BillionsStorage}{MillionsStorage}{ThousandsStorage}{HundredsStorage}{DozensStorage}{UnitsStorage}");
        ParsGold(ReternString, storageIndex);
        return ReternString;
    }

    public string AddInterest(string indexNumber, int divider, int multiplier)
    {
        PlayerPrefs.SetString("Interest", ParsGold("0", "Interest"));
        
        List<long> MillionList = new List<long>();
        List<long> MagnifiedMillionLost = new List<long>();
        int billions = 0;
        long million = 0;
        int thousands = 0;
        int hundreds = 0;
        int dozens = 0;
        int units;
        
        int BillionsNumber = PlayerPrefs.GetInt($"{indexNumber}Billions",0);
        int MillionsNumber = PlayerPrefs.GetInt($"{indexNumber}Millions",0);
        int ThousandsNumber = PlayerPrefs.GetInt($"{indexNumber}Thousands", 0);
        int HundredsNumber = PlayerPrefs.GetInt($"{indexNumber}Hundreds", 0);
        int DozensNumber = PlayerPrefs.GetInt($"{indexNumber}Dozens", 0);
        int UnitsNumber = PlayerPrefs.GetInt($"{indexNumber}Units", 0);
        
        if (BillionsNumber != 0) 
        {
            while (BillionsNumber!=0)
            {
                MillionList.Add(999000000);
                BillionsNumber--;
            }
            million = MillionsNumber * long.Parse(Million);
        }
        if (MillionsNumber != 0) 
        { 
            million = MillionsNumber * long.Parse(Million);
        }
        if (ThousandsNumber != 0)
        {
            thousands = ThousandsNumber * int.Parse(Thousands);
        }
        if (HundredsNumber != 0)
        {
            hundreds = HundredsNumber * int.Parse(Hundreds);
        }
        if (DozensNumber != 0)
        {
            dozens = DozensNumber * int.Parse(Dozens);
        }

        units = UnitsNumber;

        long sumNumbers = million + thousands + hundreds + dozens + units;
        
        MillionList.Add(sumNumbers);

        foreach (var value in MillionList)
        {
            var newValue = value + value / divider * multiplier;
            MagnifiedMillionLost.Add(newValue);
        }
        
        for (int i = 0; i < MagnifiedMillionLost.Count; i++)
        {
            string sum  = ParsGold(MagnifiedMillionLost[i].ToString(),"Interest");
            AddMoney("Interest", sum);
            PlayerPrefs.SetString(indexNumber,sum);
        }
        return PlayerPrefs.GetString(indexNumber);
    }

    public string MultiplyNumber(string indexNumber, int multiplier)
    {
        PlayerPrefs.SetString("Multiplication", ParsGold("0", "Multiplication"));
        
        List<long> MillionList = new List<long>();
        List<long> MagnifiedMillionLost = new List<long>();
        int billions = 0;
        long million = 0;
        int thousands = 0;
        int hundreds = 0;
        int dozens = 0;
        int units;
        
        int BillionsNumber = PlayerPrefs.GetInt($"{indexNumber}Billions",0);
        int MillionsNumber = PlayerPrefs.GetInt($"{indexNumber}Millions",0);
        int ThousandsNumber = PlayerPrefs.GetInt($"{indexNumber}Thousands", 0);
        int HundredsNumber = PlayerPrefs.GetInt($"{indexNumber}Hundreds", 0);
        int DozensNumber = PlayerPrefs.GetInt($"{indexNumber}Dozens", 0);
        int UnitsNumber = PlayerPrefs.GetInt($"{indexNumber}Units", 0);
        
        if (BillionsNumber != 0) 
        {
            while (BillionsNumber!=0)
            {
                MillionList.Add(999000000);
                BillionsNumber--;
            }
            million = MillionsNumber * long.Parse(Million);
        }
        if (MillionsNumber != 0) 
        { 
            million = MillionsNumber * long.Parse(Million);
        }
        if (ThousandsNumber != 0)
        {
            thousands = ThousandsNumber * int.Parse(Thousands);
        }
        if (HundredsNumber != 0)
        {
            hundreds = HundredsNumber * int.Parse(Hundreds);
        }
        if (DozensNumber != 0)
        {
            dozens = DozensNumber * int.Parse(Dozens);
        }

        units = UnitsNumber;

        long sumNumbers = million + thousands + hundreds + dozens + units;
        
        MillionList.Add(sumNumbers);

        foreach (var value in MillionList)
        {
            var newValue = value * multiplier;
            MagnifiedMillionLost.Add(newValue);
        }
        
        for (int i = 0; i < MagnifiedMillionLost.Count; i++)
        {
            string sum  = ParsGold(MagnifiedMillionLost[i].ToString(),"Multiplication");
            AddMoney("Multiplication", sum);
            PlayerPrefs.SetString(indexNumber,sum);
        }
        return PlayerPrefs.GetString(indexNumber);
    }

    public string RestoreNumbers(string moneyString)
    {
        int countRemoveChars = 0;
        for (int i = 0; i < moneyString.Length; i++)
        {
            if (moneyString[i]=='0')
            {
                countRemoveChars++;
            }
            else
            {
                break;
            }
        }

        var reterr = moneyString.Remove(0, countRemoveChars);
        if (reterr=="")
        {
            reterr = "0";
        }
        return reterr;
    }
    
    public string ConversionStorage(string gold)
    {
        int countCharInString = gold.Length;
        
        if (countCharInString < Thousands.Length)
        {
            return gold;
        }
        if (countCharInString < TensOfThousands.Length)      
        {
            return $"{gold.Substring(0,1)}K";
        }
        if (countCharInString < HundredsOfThousands.Length) 
        {
            return $"{gold.Substring(0,2)}K";
        }
        if(countCharInString < MillionsMaxValue.Length) 
        {
            return $"{gold.Substring(0,3)}K";
        }
        if (countCharInString < Million.Length)
        {
            return $"{gold.Substring(0,1)}KK";
        }
        if (countCharInString < TensOfMillions.Length)
        {
            return $"{gold.Substring(0,2)}KK";
        }
        if(countCharInString < HundredsOfMillions.Length)
        {
            return $"{gold.Substring(0,3)}KK";
        }
        if (countCharInString < TensOfBillions.Length)
        {
            return $"{gold.Substring(0,1)}KKK";
        }
        if (countCharInString < HundredsOfBillions.Length)
        {
            return $"{gold.Substring(0,2)}KKK";
        }
        if(countCharInString < TrillionsMaxValue.Length)
        {
            return $"{gold.Substring(0,3)}KKK";
        }
        if (countCharInString < TensOfTrillions.Length)
        {
            return $"{gold.Substring(0,1)}D";
        }
        if (countCharInString < HundredsOfTrillions.Length)
        {
            return $"{gold.Substring(0,2)}D";
        }
        if(countCharInString < QuadrillionsMaxValue.Length)
        {
            return $"{gold.Substring(0,3)}D";
        }
        if (countCharInString < TensOfQuadrillions.Length)
        {
            return $"{gold.Substring(0,1)}DD";
        }
        if (countCharInString < HundredsOfQuadrillions.Length)
        {
            return $"{gold.Substring(0,2)}DD";
        } 
        if(countCharInString < SextillionsMaxValue.Length)
        {
            return $"{gold.Substring(0,3)}DD";
        }

        return "0";
    }
}*/