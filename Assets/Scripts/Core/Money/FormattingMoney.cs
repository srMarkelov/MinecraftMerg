using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormattingMoney : MonoBehaviour
{
    private int money;
    public string FormattingGold(long money)
    {
        var k = " ";
        while (money > 1000.0f)
        {
            k += "K";
            money /= 1000;
        }
        return money + k;
    }
    /*public string FormattingGold(List<long> money)
    {
        var k = " ";
        foreach (var gold in money)
        {
            if (money)
            {
                
            }
        }
        while (money>1000.0f)
        {
            k += "K";
            money /= 1000;
        }
        return money + k;
    }*/
}
