/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


enum GoldNumbers
{
    Units = 0,
    Dozens=1,
    Hundreds=2,
    Thousands=3,
    ThousandsTens=4,
    ThousandsHundred=5,
    Millions=6,
    MillionsTens=7,
    MillionsHundreds=8,
    
    Billions=9,
    BillionsTens=10,
    BillionsHundreds=11,
    
    Trillions=12,
    TrillionsTens=13,
    TrillionsHundreds=14,
    
    Quadrillions=15,
    QuadrillionsTens=16,
    QuadrillionsHundreds=17,
    
    Quintillions=18,
    QuintillionsTens=19,
    QuintillionsHundreds=20,
    
    Sextillions=21,
    SextillionsTens=22,
    SextillionsHundreds=23
    
}
public class GoldConversion : MonoBehaviour
{
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


    private static string Quadrillions = "1000000000000000";
    private static string QuadrillionsTens = "10000000000000000";
    private static string QuadrillionsHundreds = "100000000000000000";
    private static string QuadrillionsMaxValue = "999999999999999999";


    private static string Quintillions = "1000000000000000000";
    private static string QuintillionsTens = "10000000000000000000";
    private static string QuintillionsHundreds = "100000000000000000000";
    private static string QuintillionsMaxValue = "999999999999999999999";


    private static string Sextillions = "1000000000000000000000";
    private static string SextillionsTens = "10000000000000000000000";
    private static string SextillionsHundreds = "100000000000000000000000";
    private static string SextillionsMaxValue = "999999999999999999999999";


    

    [SerializeField] private OptimizationGoldConversion _optimization;
    
    
    /*private int _units;
    private int _dozens;
    private int _hundreds;

    private int _thousands = 0;
    private int _thousandsTens = 0;
    private int _thousandsHundreds = 0;
    private int _thousandsMaxValue = 0;
    
    private int _million = 0;
    private int _millionsTens = 0;
    private int _millionsHundreds = 0;
    private int _millionsMaxValue = 0;
    
    private int _billions = 0;
    private int _billionsTens = 0;
    private int _billionsHundreds = 0;
    private int _billionsMaxValue = 0;

    private int _trillions = 0;
    private int _trillionsTens = 0;
    private int _trillionsHundreds = 0;
    private int _trillionsMaxValue = 0;
    
    private int _quadrillionsTens = 0;
    private int _quadrillionsHundreds = 0;
    private int _quadrillionsMaxValue = 0;

    private int _quintillions = 0;
    private int _quintillionsTens = 0;
    private int _quintillionsHundreds = 0;
    private int _quintillionsMaxValue = 0;

   private int _sxtillions = 0;
   private int _sxtillionsTens = 0;
   private int _sxtillionsHundreds = 0;
   private int _sxtillionsMaxValue = 0;#1#




    

    /*p#1#/*ublic string ParsGold(string gold, string index)                                     //ParsGold
    {
        int _units = 0;
        int _dozens = 0;
        int _hundreds = 0;
        
        
        int _thousands = 0;
        int _thousandsTens = 0;
        int _thousandsHundred = 0;
        
        
        int _millions = 0;
        int _millionsTens = 0;
        int _millionsHundreds = 0;
        
        
        int _billions = 0;
        int _billionsTens = 0;
        int _billionsHundreds = 0;

        int _trillions = 0;
        int _trillionsTens = 0;
        int _trillionsHundreds = 0;

        int _quadrillions = 0;
        int _quadrillionsTens = 0;
        int _quadrillionsHundreds = 0;

        int _quintillions = 0;
        int _quintillionsTens = 0;
        int _quintillionsHundreds = 0;

        int _sxtillions = 0;
        int _sxtillionsTens = 0;
        int _sxtillionsHundreds = 0;

        if (gold.Length <= SextillionsMaxValue.Length)
        {
            if (gold.Length == 24)
            {
                _sxtillionsHundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}SextillionsHundreds", _sxtillionsHundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}SextillionsHundreds", 0);

            if (gold.Length == 23)
            {
                _sxtillionsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}SextillionsTens", _sxtillionsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}SextillionsTens", 0);

            if (gold.Length == 22)
            {
                _sxtillions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Sextillions", _sxtillions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Sextillions", 0);
        }
        if (gold.Length <= QuintillionsMaxValue.Length)
        {
            if (gold.Length == 21)
            {
                _quintillionsHundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}QuintillionsHundreds", _quintillionsHundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}QuintillionsHundreds", 0);

            if (gold.Length == 20)
            {
                _quintillionsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}QuintillionsTens", _quintillionsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}QuintillionsTens", 0);

            if (gold.Length == 19)
            {
                _quintillions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Quintillions", _quintillions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Quintillions", 0);
        }
        if (gold.Length <= QuadrillionsMaxValue.Length)
        {
            if (gold.Length == 18)
            {
                _quadrillionsHundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}QuadrillionsHundreds", _quadrillionsHundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}QuadrillionsHundreds", 0);

            if (gold.Length == 17)
            {
                _quadrillionsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}QuadrillionsTens", _quadrillionsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}QuadrillionsTens", 0);

            if (gold.Length == 16)
            {
                _quadrillions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Quadrillions", _quadrillions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Quadrillions", 0);
        }
        if (gold.Length <= TrillionsMaxValue.Length)
        {
            if (gold.Length == 15)
            {
                _trillionsHundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}TrillionsHundreds", _trillionsHundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}TrillionsHundreds", 0);

            if (gold.Length == 14)
            {
                _trillionsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}TrillionsTens", _trillionsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}TrillionsTens", 0);

            if (gold.Length == 13)
            {
                _trillions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Trillions", _trillions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Trillions", 0);
        }
        if (gold.Length <= BillionsMaxValue.Length)
        {
            if (gold.Length == 12)
            {
                _billionsHundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}BillionsHundreds", _billionsHundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}BillionsHundreds", 0);

            if (gold.Length == 11)
            {
                _billionsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}BillionsTens", _billionsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}BillionsTens", 0);
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
                _millionsHundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}MillionsHundreds",_millionsHundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}MillionsHundreds", 0);

            if (gold.Length == 8)
            {
                _millionsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}MillionsTens",_millionsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}MillionsTens", 0);

            if (gold.Length == 7)
            {
                _millions = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Millions",_millions);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Millions", 0);

            if (gold.Length == 6)
            {
                _thousandsHundred = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}ThousandsHundreds",_thousandsHundred);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}ThousandsHundreds", 0);

            if (gold.Length == 5)
            {
                _thousandsTens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}ThousandsTens",_thousandsTens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}ThousandsTens", 0);

            if (gold.Length == 4)
            {
                _thousands = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Thousands",_thousands);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Thousands", 0);

            if (gold.Length == 3)
            {
                _hundreds = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Hundreds",_hundreds);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Hundreds", 0);
            
            if (gold.Length == 2)
            {
                _dozens = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Dozens",_dozens);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Dozens", 0);

            if (gold.Length == 1)
            {
                _units = int.Parse(gold.Substring(0, 1));
                PlayerPrefs.SetInt($"{index}Units",_units);
                gold = gold.Remove(0, 1);
            }
            else PlayerPrefs.SetInt($"{index}Units", 0);
        }#1#
        
        
        /*var ReternString = RestoreNumbers($"{_sxtillionsHundreds}{_sxtillionsTens}{_sxtillions}" +
                                          $"{_quintillionsHundreds}{_quintillionsTens}{_quintillions}" +
                                          $"{_quadrillionsHundreds}{_quadrillionsTens}{_quadrillions}" +
                                          $"{_trillionsHundreds}{_trillionsTens}{_trillions}" +
                                          $"{_billionsHundreds}{_billionsTens}{_billions}" +
                                          $"{_millionsHundreds}{_millionsTens}{_millions}"+
                                          $"{_thousandsHundred}{_thousandsTens}{_thousands}"+
                                          $"{_hundreds}{_dozens}{_units}");
        return ReternString;
    }#1#

    
    public string AddMoney(string storageIndex, string addMoneyIndex) //AddMoney
    {
        List<int> storageList = _optimization.ParsGold(storageIndex);
        List<int> addMoneyList = _optimization.ParsGold(addMoneyIndex);
        
        Dictionary<string, int> storage = new Dictionary<string, int>();

        int UnitsStorage = 0;
        int DozensStorage = 0;
        int HundredsStorage = 0;

        int ThousandsStorage = 0;
        int ThousandsTensStorage = 0;
        int ThousandsHundredStorage = 0;

        int MillionsStorage = 0;
        int MillionsTensStorage = 0;
        int MillionsHundredsStorage = 0;
        
        int BillionsStorage =0;
        int BillionsTensStorage = 0;
        int BillionsHundredsStorage = 0;

        int TrillionsStorage = 0;
        int TrillionsTensStorage = 0;
        int TrillionsHundredsStorage= 0;

        int QuadrillionsStorage = 0;
        int QuadrillionsTensStorage = 0;
        int QuadrillionsHundredsStorage = 0;

        int QuintillionsStorage = 0;
        int QuintillionsTensStorage = 0;
        int QuintillionsHundredsStorage = 0;

        int SextillionsStorage = 0;
        int SextillionsTensStorage = 0;
        int SextillionsHundredsStorage = 0;
        
        
        int correctEnumNumber = -1;
        
        for (int i = addMoneyList.Count-1; i >= 0; i--)
        {
            while (addMoneyList[i] != 0)
            {
                storageList[i]++;
                addMoneyList[i]--;
                if (storageList[i].ToString().Length >= Dozens.Length)
                {
                    addMoneyList[i - 1]++;
                    storageList[i] = 0;
                }
            }
            correctEnumNumber++;
            
            var goldNumbers = GoldNumbers.Units + correctEnumNumber;
            storage.Add(goldNumbers.ToString(), storageList[i]);
        }
        
        foreach (var value in storage)
        {
            if (value.Key == GoldNumbers.Units.ToString())
            {
                UnitsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Dozens.ToString())
            {
                DozensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Hundreds.ToString())
            {
                HundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Thousands.ToString())
            {
                ThousandsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.ThousandsTens.ToString())
            {
                ThousandsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.ThousandsHundred.ToString())
            {
                ThousandsHundredStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Millions.ToString())
            {
                MillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.MillionsTens.ToString())
            {
                MillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.MillionsHundreds.ToString())
            {
                MillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Billions.ToString())
            {
                BillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.BillionsTens.ToString())
            {
                BillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.BillionsHundreds.ToString())
            {
                BillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Trillions.ToString())
            {
                TrillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsTens.ToString())
            {
                TrillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsHundreds.ToString())
            {
                TrillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsHundreds.ToString())
            {
                TrillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Quadrillions.ToString())
            {
                QuadrillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuadrillionsTens.ToString())
            {
                QuadrillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuadrillionsHundreds.ToString())
            {
                QuadrillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Quintillions.ToString())
            {
                QuintillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuintillionsTens.ToString())
            {
                QuintillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuintillionsHundreds.ToString())
            {
                QuintillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Sextillions.ToString())
            {
                SextillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.SextillionsTens.ToString())
            {
                SextillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.SextillionsHundreds.ToString())
            {
                SextillionsHundredsStorage = value.Value;
            }
        }
        
        var _fullSxtillion =  $"{(SextillionsHundredsStorage*100 + SextillionsTensStorage*10 + SextillionsStorage):D3}";
        var _fullQuintillion =  $"{(QuintillionsHundredsStorage*100 + QuintillionsTensStorage*10 + QuintillionsStorage):D3}";
        var _fullQuadrillion =  $"{(QuadrillionsHundredsStorage*100 + QuadrillionsTensStorage*10 + QuadrillionsStorage):D3}";
        var _fullTrillion =  $"{(TrillionsHundredsStorage*100 + TrillionsTensStorage*10 + TrillionsStorage):D3}";
        var _fullBillion =  $"{(BillionsHundredsStorage*100 + BillionsTensStorage*10 + BillionsStorage):D3}";
        var _fullMillion =  $"{(MillionsHundredsStorage*100 +MillionsTensStorage*10 + MillionsStorage):D3}";
        var _fullThousands =  $"{(ThousandsHundredStorage*100 +ThousandsTensStorage*10 + ThousandsStorage):D3}";
        var _fullHundred =  $"{(HundredsStorage*100 +DozensStorage*10 + UnitsStorage):D3}";
        
        var ReternString = RestoreNumbers($"{_fullSxtillion}" +
                                          $"{_fullQuintillion}" +
                                          $"{_fullQuadrillion}" +
                                          $"{_fullTrillion}" +
                                          $"{_fullBillion}" +
                                          $"{_fullMillion}"+
                                          $"{_fullThousands}"+
                                          $"{_fullHundred}");
        return ReternString;
    }   
    

    public string RemoveMoney(string storageIndex, string removeMoney)
    {
        List<int> storageList = _optimization.ParsGold(storageIndex);
        List<int> removeMoneyList = _optimization.ParsGold(removeMoney);
        
        Dictionary<string, int> storage = new Dictionary<string, int>();

        int UnitsStorage = 0;
        int DozensStorage = 0;
        int HundredsStorage = 0;

        int ThousandsStorage = 0;
        int ThousandsTensStorage = 0;
        int ThousandsHundredStorage = 0;

        int MillionsStorage = 0;
        int MillionsTensStorage = 0;
        int MillionsHundredsStorage = 0;
        
        int BillionsStorage =0;
        int BillionsTensStorage = 0;
        int BillionsHundredsStorage = 0;

        int TrillionsStorage = 0;
        int TrillionsTensStorage = 0;
        int TrillionsHundredsStorage= 0;

        int QuadrillionsStorage = 0;
        int QuadrillionsTensStorage = 0;
        int QuadrillionsHundredsStorage = 0;

        int QuintillionsStorage = 0;
        int QuintillionsTensStorage = 0;
        int QuintillionsHundredsStorage = 0;

        int SextillionsStorage = 0;
        int SextillionsTensStorage = 0;
        int SextillionsHundredsStorage = 0;

        for (int i = 0; i < removeMoneyList.Count; i++)
        {
            storageList[i] -= removeMoneyList[i];
        }

        int correctEnumNumber = -1;
        for (int i = storageList.Count-1; i >= 0; i--)
        {
            if (storageList[i]<0)
            {
                storageList[i] += 10;

                if (i==storageList.Count) continue;
                storageList[i - 1]--;
            }

            correctEnumNumber++;
            var goldNumbers = GoldNumbers.Units + correctEnumNumber;
            storage.Add(goldNumbers.ToString(), storageList[i]);
        }
        
        
        foreach (var value in storage)
        {
            if (value.Key == GoldNumbers.Units.ToString())
            {
                UnitsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Dozens.ToString())
            {
                DozensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Hundreds.ToString())
            {
                HundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Thousands.ToString())
            {
                ThousandsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.ThousandsTens.ToString())
            {
                ThousandsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.ThousandsHundred.ToString())
            {
                ThousandsHundredStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Millions.ToString())
            {
                MillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.MillionsTens.ToString())
            {
                MillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.MillionsHundreds.ToString())
            {
                MillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Billions.ToString())
            {
                BillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.BillionsTens.ToString())
            {
                BillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.BillionsHundreds.ToString())
            {
                BillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Trillions.ToString())
            {
                TrillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsTens.ToString())
            {
                TrillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsHundreds.ToString())
            {
                TrillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsHundreds.ToString())
            {
                TrillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Quadrillions.ToString())
            {
                QuadrillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuadrillionsTens.ToString())
            {
                QuadrillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuadrillionsHundreds.ToString())
            {
                QuadrillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Quintillions.ToString())
            {
                QuintillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuintillionsTens.ToString())
            {
                QuintillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.QuintillionsHundreds.ToString())
            {
                QuintillionsHundredsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.Sextillions.ToString())
            {
                SextillionsStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.SextillionsTens.ToString())
            {
                SextillionsTensStorage = value.Value;
            }
            else if (value.Key == GoldNumbers.SextillionsHundreds.ToString())
            {
                SextillionsHundredsStorage = value.Value;
            }
        }


        var _fullSxtillion =  $"{(SextillionsHundredsStorage*100 + SextillionsTensStorage*10 + SextillionsStorage):D3}";
        var _fullQuintillion =  $"{(QuintillionsHundredsStorage*100 + QuintillionsTensStorage*10 + QuintillionsStorage):D3}";
        var _fullQuadrillion =  $"{(QuadrillionsHundredsStorage*100 + QuadrillionsTensStorage*10 + QuadrillionsStorage):D3}";
        var _fullTrillion =  $"{(TrillionsHundredsStorage*100 + TrillionsTensStorage*10 + TrillionsStorage):D3}";
        var _fullBillion =  $"{(BillionsHundredsStorage*100 + BillionsTensStorage*10 + BillionsStorage):D3}";
        var _fullMillion =  $"{(MillionsHundredsStorage*100 +MillionsTensStorage*10 + MillionsStorage):D3}";
        var _fullThousands =  $"{(ThousandsHundredStorage*100 +ThousandsTensStorage*10 + ThousandsStorage):D3}";
        var _fullHundred =  $"{(HundredsStorage*100 +DozensStorage*10 + UnitsStorage):D3}";
        
        var ReternString = RestoreNumbers($"{_fullSxtillion}" +
                                          $"{_fullQuintillion}" +
                                          $"{_fullQuadrillion}" +
                                          $"{_fullTrillion}" +
                                          $"{_fullBillion}" +
                                          $"{_fullMillion}"+
                                          $"{_fullThousands}"+
                                          $"{_fullHundred}");
        return ReternString;
    }

    public string AddInterest(string number, int divider, int multiplier)
    {

        string result = MultiplyNumber(DividingNumber(number, divider), multiplier);
        return result;
    }

    public string MultiplyNumber(string indexNumber, int multiplier)
    {
        List<int> numberList = _optimization.ParsGold(indexNumber);
        Dictionary<string, int> storage = new Dictionary<string, int>();

        string result = "0";
        for (int i = 0; i < multiplier; i++)
        {
            result = AddMoney(result ,indexNumber);
        }
        return result;
    }

    public string DividingNumber(string indexNumber, int divider)
    {

        List<long>TrillionsTenList = new List<long>();
        List<int> MagnifiedMillionLost = new List<int>();
        
        List<int> numberList = _optimization.ParsGold(indexNumber);
        Dictionary<string, int> storage = new Dictionary<string, int>();
        
        int millionHundreds = 0;
        int millionTens = 0;
        int million = 0;
        int thousands = 0;
        int thousandsTens = 0;
        int thousandsHundred = 0;
        int hundreds = 0;
        int dozens = 0;
        int units;
        
        int UnitsNumber = 0;
        int DozensNumber  = 0;
        int HundredsNumber  = 0;
        int ThousandsNumber  = 0;
        int ThousandsTensNumber  = 0;
        int ThousandsHundredNumber  = 0;
        int MillionsNumber  = 0;
        int MillionsTensNumber = 0;
        int MillionsHundredsNumber  = 0;
        long BillionsNumber = 0;
        long BillionsTensNumber = 0;
        long BillionsHundredsNumber = 0;
        long TrillionsNumber = 0;
        long TrillionsTensNumber= 0;
        long TrillionsHundredsNumber = 0;
        long QuadrillionsNumber = 0;
        long QuadrillionsTensNumber = 0;
        long QuadrillionsHundredsNumber = 0;
        long QuintillionsNumber = 0;
        long QuintillionsTensNumber = 0;
        long QuintillionsHundredsANumber = 0;
        long SxtillionsNumber = 0;
        long SxtillionsTensNumber = 0;
        long SxtillionsHundredsNumber = 0;


        int correctEnumNumber = -1;
        for (int i = numberList.Count-1; i >= 0; i--)
        {
            correctEnumNumber++;
            
            var goldNumbers = GoldNumbers.Units + correctEnumNumber;
            storage.Add(goldNumbers.ToString(), numberList[i]);
        }
        
        foreach (var value in storage)
        {
            if (value.Key == GoldNumbers.Units.ToString())
            {
                UnitsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Dozens.ToString())
            {
                DozensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Hundreds.ToString())
            {
                HundredsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Thousands.ToString())
            {
                ThousandsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.ThousandsTens.ToString())
            {
                ThousandsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.ThousandsHundred.ToString())
            {
                ThousandsHundredNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Millions.ToString())
            {
                MillionsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.MillionsTens.ToString())
            {
                MillionsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.MillionsHundreds.ToString())
            {
                MillionsHundredsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Billions.ToString())
            {
                BillionsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.BillionsTens.ToString())
            {
                BillionsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.BillionsHundreds.ToString())
            {
                BillionsHundredsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Trillions.ToString())
            {
                TrillionsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsTens.ToString())
            {
                TrillionsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsHundreds.ToString())
            {
                TrillionsHundredsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.TrillionsHundreds.ToString())
            {
                TrillionsHundredsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Quadrillions.ToString())
            {
                QuadrillionsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.QuadrillionsTens.ToString())
            {
                QuadrillionsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.QuadrillionsHundreds.ToString())
            {
                QuadrillionsHundredsNumber= value.Value;
            }
            else if (value.Key == GoldNumbers.Quintillions.ToString())
            {
                QuintillionsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.QuintillionsTens.ToString())
            {
                QuintillionsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.QuintillionsHundreds.ToString())
            {
                QuintillionsHundredsANumber = value.Value;
            }
            else if (value.Key == GoldNumbers.Sextillions.ToString())
            {
                SxtillionsNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.SextillionsTens.ToString())
            {
                SxtillionsTensNumber = value.Value;
            }
            else if (value.Key == GoldNumbers.SextillionsHundreds.ToString())
            {
                SxtillionsHundredsNumber = value.Value;
            }
        }
        
        
        if (SxtillionsHundredsNumber != 0)
        {
            SxtillionsTensNumber += SxtillionsHundredsNumber *10;
        }
        if (SxtillionsTensNumber != 0)
        {
            SxtillionsNumber += SxtillionsTensNumber *10;
        }
        if (SxtillionsNumber != 0)
        {
            QuintillionsHundredsANumber += SxtillionsNumber *10;
        }
        if (QuintillionsHundredsANumber != 0)
        {
            QuintillionsTensNumber += QuintillionsHundredsANumber *10;
        }
        if (QuintillionsTensNumber != 0)
        {
            QuintillionsNumber += QuintillionsTensNumber *10;
        }
        if (QuintillionsNumber != 0)
        {
            QuadrillionsHundredsNumber += QuintillionsNumber *10;
        }
        if (QuadrillionsHundredsNumber != 0)
        {
            QuadrillionsTensNumber += QuadrillionsHundredsNumber *10;
        }
        if (QuadrillionsTensNumber != 0)
        {
            QuadrillionsNumber += QuadrillionsTensNumber *10;
        }
        if (QuadrillionsNumber != 0)
        {
            TrillionsHundredsNumber += QuadrillionsNumber *10;
        }
        if (TrillionsHundredsNumber != 0)
        {
            TrillionsTensNumber += TrillionsHundredsNumber *10;
        }
        if (TrillionsTensNumber != 0)
        {
            /*for (int i = 0; i < TrillionsTensNumber; i++)
            {
                TrillionsTenList.Add(long.Parse(TrillionsTens));
                /*BillionsNumber--;#2#
            }#1#
            /*TrillionsTensNumber *= int.Parse(TrillionsTens);#1#
        }
        if (TrillionsNumber != 0)
        {
            TrillionsNumber *= long.Parse(Trillions);
        }
        //-------------------------------------------------------------------//
        if (BillionsHundredsNumber != 0)
        {
            BillionsHundredsNumber *= long.Parse(BillionsHundreds);
        }
        if (BillionsTensNumber != 0)
        {
            BillionsTensNumber *= long.Parse(BillionsTens);
        }
        if (BillionsNumber != 0)
        {
            BillionsNumber *= long.Parse(Billions);
            
        }
        //-------------------------------------------------------------------//
        if (MillionsHundredsNumber != 0) 
        { 
            MillionsHundredsNumber *= int.Parse(MillionsHundreds);
        }
        if (MillionsTensNumber != 0) 
        { 
            MillionsTensNumber *= int.Parse(MillionsTens);
        }
        if (MillionsNumber != 0) 
        { 
            MillionsNumber *= int.Parse(Million);
        }
        //-------------------------------------------------------------------//
        if (ThousandsHundredNumber != 0)
        {
            ThousandsHundredNumber *= int.Parse(ThousandsHundreds);
        }
        if (ThousandsTensNumber != 0)
        {
            ThousandsTensNumber *= int.Parse(ThousandsTens);
        }
        if (ThousandsNumber != 0)
        {
            ThousandsNumber *= int.Parse(Thousands);
        }
        //-------------------------------------------------------------------//
        if (HundredsNumber != 0)
        {
            HundredsNumber *= int.Parse(Hundreds);
        }
        if (DozensNumber != 0)
        {
            DozensNumber *= int.Parse(Dozens);
        }
        
        long sumNumbers = TrillionsNumber + 
                          BillionsHundredsNumber + BillionsTensNumber + BillionsNumber +
                          MillionsHundredsNumber + MillionsTensNumber + MillionsNumber + 
                          ThousandsHundredNumber +ThousandsTensNumber + ThousandsNumber + 
                          HundredsNumber + DozensNumber + UnitsNumber;
        
        TrillionsTenList.Add(sumNumbers);
        
        string result = "0";
        string sum = string.Empty;
        for (int i = 0; i < TrillionsTensNumber; i++)
        {
            var newValue = 10000000000000 / divider;
            if (sum == string.Empty)
            {
                sum = newValue.ToString();
                continue;
            }
            sum = SumString(sum, newValue.ToString());
        }

        sumNumbers /= divider;
        sum = SumString(sum, sumNumbers.ToString());
        result = AddMoney(result, sum);
        /*result = AddMoney(result, newValue.ToString());#1#
        
        
        /*result = AddMoney(result, newValue.ToString());#1#

        /*foreach (var value in TrillionsTenList)
        {
            var newValue = value / divider;
            result = AddMoney(result, newValue.ToString());
        }#1#
        return result;
    }
    
    private string SumString(string number1, string number2)
    {
        var lengthNum1 = number1.Length;
        var lengthNum2 = number2.Length;
        var maxLength = lengthNum1 > lengthNum2 ? lengthNum1 : lengthNum2;

        string result = "";
        int[] inverse1 = new int[number1.Length];
        int[] inverse2 = new int[number2.Length];

        for (var i = 0; i < number1.Length; i++)
        {
            inverse1[i] = (int)Char.GetNumericValue(number1[number1.Length - 1 - i]);
        }
        for (var i = 0; i < number2.Length; i++)
        {
            inverse2[i] = (int)Char.GetNumericValue(number2[number2.Length - 1 - i]);
        }

        var tempAdd = 0;
        for (var i = 0; i < maxLength; i++)
        {
            var num1 = number1.Length <= i ? 0 : inverse1[i];
            var num2 = number2.Length <= i ? 0 : inverse2[i];

            var newI = num1 + num2 + tempAdd;
            tempAdd = newI >= 10 ? 1 : 0;
            newI = newI >= 10 ? newI - 10 : newI; 
            result = $"{newI}{result}";
        }

        if (tempAdd > 0)
        {
            result = $"{tempAdd}{result}";
        }

        return result;
    }
    
    public bool MoreOrEqual(string firstNumber, string secondNumber)
    {
        List<int> firstNumberList = _optimization.ParsGold(firstNumber);
        List<int> secondNumberList = _optimization.ParsGold(secondNumber);

        if (firstNumberList.Count >  secondNumberList.Count)
            return true;
        
        int oldFirstNumber = 0;
        int oldSecondNumber = 0;
        for (int i = 0; i < firstNumberList.Count+1; i++)
        {
            if (i == firstNumberList.Count && oldFirstNumber == oldSecondNumber)
                return true;
            
            if (firstNumberList[i] > secondNumberList[i])
                return true;
            
            if (firstNumberList[i] < secondNumberList[i])
                return false;
            if (firstNumberList[i] == 0 && secondNumberList[i] != 0)
                return false;

            if (firstNumberList[i] == secondNumberList[i])
            {
                oldFirstNumber = firstNumberList[i];
                oldSecondNumber = secondNumberList[i];
            }
        }
        return false;
    }
    public bool LessOrEqual(string firstNumber, string secondNumber)
    {
        List<int> firstNumberList = _optimization.ParsGold(firstNumber);
        List<int> secondNumberList = _optimization.ParsGold(secondNumber);

        if (firstNumberList.Count <  secondNumberList.Count)
            return true;
        
        
        
        int oldFirstNumber = 0;
        int oldSecondNumber = 0;
        for (int i = 0; i < firstNumberList.Count+1; i++)
        {
            if (i == firstNumberList.Count && oldFirstNumber == oldSecondNumber)
                return true;
            
            if (firstNumberList[i] < secondNumberList[i])
                return true;
            
            if (firstNumberList[i] > secondNumberList[i])
                return false;
            if (firstNumberList[i] != 0 && secondNumberList[i] == 0)
                return false;
            
            if (firstNumberList[i] == secondNumberList[i])
            {
                oldFirstNumber = firstNumberList[i];
                oldSecondNumber = secondNumberList[i];
            }
        }
        return false;
    }
    public bool More(string firstNumber, string secondNumber)
    {
        List<int> firstNumberList = _optimization.ParsGold(firstNumber);
        List<int> secondNumberList = _optimization.ParsGold(secondNumber);

        if (firstNumberList.Count >  secondNumberList.Count)
            return true;
        
        int oldFirstNumber = 0;
        int oldSecondNumber = 0;
        for (int i = 0; i < firstNumberList.Count+1; i++)
        {
            if (i == firstNumberList.Count && oldFirstNumber == oldSecondNumber)
                return false;
            
            if (firstNumberList[i] > secondNumberList[i])
                return true;
            if (firstNumberList[i] < secondNumberList[i])
                return false;
            
            if (firstNumberList[i] == secondNumberList[i])
            {
                oldFirstNumber = firstNumberList[i];
                oldSecondNumber = secondNumberList[i];
            }
        }
        return false;
    }
    public bool Less(string firstNumber, string secondNumber)
    {
        List<int> firstNumberList = _optimization.ParsGold(firstNumber);
        List<int> secondNumberList = _optimization.ParsGold(secondNumber);

        if (firstNumberList.Count <  secondNumberList.Count)
            return true;
        
        int oldFirstNumber = 0;
        int oldSecondNumber = 0;
        for (int i = 0; i < firstNumberList.Count+1; i++)
        {
            if (i == firstNumberList.Count && oldFirstNumber == oldSecondNumber)
                return false;
            
            if (firstNumberList[i] < secondNumberList[i])
                return true;

            if (firstNumberList[i] > secondNumberList[i])
                return false;
            
            if (firstNumberList[i] == secondNumberList[i])
            {
                oldFirstNumber = firstNumberList[i];
                oldSecondNumber = secondNumberList[i];
            }
        }
        return false;
    }
    public bool Equal(string firstNumber, string secondNumber)
    {
        List<int> firstNumberList = _optimization.ParsGold(firstNumber);
        List<int> secondNumberList = _optimization.ParsGold(secondNumber);

        int oldFirstNumber = 0;
        int oldSecondNumber = 0;
        for (int i = 0; i < firstNumberList.Count+1; i++)
        {
            if (i == firstNumberList.Count && oldFirstNumber == oldSecondNumber)
                return true;
            
            if (firstNumberList[i] < secondNumberList[i])
                return false;

            if (firstNumberList[i] > secondNumberList[i])
                return false;
            
            if (firstNumberList[i] == secondNumberList[i])
            {
                oldFirstNumber = firstNumberList[i];
                oldSecondNumber = secondNumberList[i];
            }
        }
        return false;
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

        if (Less(gold.Length.ToString(), Thousands.Length.ToString()))
        {
            return gold;
        }

        if (Equal(gold.Length.ToString(),Thousands.Length.ToString())) 
        {
            return $"{gold.Substring(0,1)}K";
        }
        if (Equal(gold.Length.ToString(),ThousandsTens.Length.ToString())) 
        {
            return $"{gold.Substring(0,2)}K";
        }
        if (Equal(gold.Length.ToString(),ThousandsHundreds.Length.ToString())) 
        {
            return $"{gold.Substring(0,3)}K";
        }
        if(Equal(gold.Length.ToString(),Million.Length.ToString()))
        {
            return $"{gold.Substring(0,1)}KK";
        }
        if(Equal(gold.Length.ToString(),MillionsTens.Length.ToString()))
        {
            return $"{gold.Substring(0,2)}KK";
        }
        if(Equal(gold.Length.ToString(),MillionsHundreds.Length.ToString()))
        {
            return $"{gold.Substring(0,3)}KK";
        }
        if(Equal(gold.Length.ToString(),Billions.Length.ToString()))
        {
            return $"{gold.Substring(0,1)}B";
        }
        if(Equal(gold.Length.ToString(),BillionsTens.Length.ToString()))
        {
            return $"{gold.Substring(0,2)}B";
        }
        if(Equal(gold.Length.ToString(),BillionsHundreds.Length.ToString()))
        {
            return $"{gold.Substring(0,3)}B";
        }
        if(Equal(gold.Length.ToString(),Trillions.Length.ToString()))
        {
            return $"{gold.Substring(0,1)}KB";
        }
        if(Equal(gold.Length.ToString(),TrillionsTens.Length.ToString()))
        {
            return $"{gold.Substring(0,2)}KB";
        }
        if(Equal(gold.Length.ToString(),TrillionsHundreds.Length.ToString()))
        {
            return $"{gold.Substring(0,3)}KB";
        }
        if(Equal(gold.Length.ToString(),Quadrillions.Length.ToString()))
        {
            return $"{gold.Substring(0,1)}BB";
        }
        if(Equal(gold.Length.ToString(),QuadrillionsTens.Length.ToString()))
        {
            return $"{gold.Substring(0,2)}BB";
        }
        if(Equal(gold.Length.ToString(),QuadrillionsHundreds.Length.ToString()))
        {
            return $"{gold.Substring(0,3)}BB";
        }
        return "0";
    }
}*/