using System;
using System.Collections;
using System.Collections.Generic;
using Core.Battler;
using UnityEngine;
using YG;

public class GameRating : MonoBehaviour
{
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private float time;
    private void Update()
    {
        /*if (_battleHandler.IsBattle)
        {
            return;
        }
        
        time -= Time.deltaTime;
        if (time <= 0)
        {
            YandexGame.ReviewShow(true);
            time = 15f;
        }*/
    }
}

