using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortunaTrigger : MonoBehaviour
{
    public float GoldMultiplier;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<TridderX2>())
        {
            GoldMultiplier = 2;
            /*Debug.Log("2");*/
        }
        if (col.GetComponent<TridderX3>())
        {
            GoldMultiplier = 3;
            /*Debug.Log("3");*/
        }
        if (col.GetComponent<TridderX4>())
        {
            GoldMultiplier = 4;
            /*Debug.Log("4");*/
        }
        if (col.GetComponent<TridderX5>())
        {
            GoldMultiplier = 5;
            /*Debug.Log("5");*/
        }
    }
    
}
