using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptimizationGoldConversion : MonoBehaviour
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
    
    
    
    /*public GoldConversion _goldConversion;*/
    public List<int> ParsGold(string gold)                                     //ParsGold
    {
        List<int> returnParsNumber = new List<int>(24);
        
        for (int i = 0; i < SextillionsMaxValue.Length-gold.Length; i++)
        {
            returnParsNumber.Add(0);
        }
        
        for (int i = 0; i < gold.Length; i++)
        {
            returnParsNumber.Add(int.Parse(gold[i].ToString()));
        }
        return returnParsNumber;
    }

    public List<int> ParsGold(string gold, bool f) //ParsGold
    {
        List<int> returnParsNumber = new List<int>();


        string maxValueNumbers = SextillionsMaxValue;

        for (int i = 0; i < SextillionsMaxValue.Length; i++)
        {
            if (gold.Length == maxValueNumbers.Length)
            {
                returnParsNumber.Add(int.Parse(gold.Substring(0, 1)));
                gold = gold.Remove(0, 1);
            }
            else returnParsNumber.Add(0);

            var remove = maxValueNumbers.Remove(0, 1);
            maxValueNumbers = remove;
        }

        return returnParsNumber;
    }

}
