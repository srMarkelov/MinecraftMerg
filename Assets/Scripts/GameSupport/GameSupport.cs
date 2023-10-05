using System;
using System.Collections;
using System.Collections.Generic;
using Core.Battler;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class GameSupport : MonoBehaviour
{
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private FinishLevelController _finishLevelController;
    [SerializeField] private GameObject _finger;

    private int currentLevel;
    private float timer = 5;
    private bool timerOn;
    private bool restart;
    

    private void OnEnable()
    {
        _finishLevelController.restartLevel += RestartLevel;
    }

    private void OnDisable()
    {
        _finishLevelController.restartLevel -= RestartLevel;
    }

    private void RestartLevel()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (currentLevel == 1)
        {
            timer = 1;
            restart = false;
            return;
        }
        timer = 15;
        restart = false;
    }
    private void Update()
    {
        if (_battleHandler.IsBattle)
        {
            restart = true;
            return;
        }
        if (restart)
            return;
        if (currentLevel == 1)
        {
            FingerSupportGuide();
            return;
        }
        FingerSupport();
    }

    public void FingerSupport()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && timerOn == false)
        {
            for (int i = 0; i < _battleHandler.PlayerCharacters.Count; i++)
            {
                _battleHandler.OverwritePlayerCharacters();
                for (int j = i + 1; j < _battleHandler.PlayerCharacters.Count; j++)
                {
                    if (_battleHandler.PlayerCharacters[i].CharacterType == _battleHandler.PlayerCharacters[j].CharacterType)
                    {
                        timerOn = true;

                        timer = 14;
                        timerOn = false;
                        
                        var finger = Instantiate(_finger, _battleHandler.PlayerCharacters[i].Position,quaternion.identity);
                        finger.transform.DOMove(_battleHandler.PlayerCharacters[j].Position, 2.5f).OnComplete(() =>
                        {
                            Destroy(finger);
                        });
                        return;
                    }
                }
            }
        }
    }
    
    public void FingerSupportGuide()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && timerOn == false)
        {
            for (int i = 0; i < _battleHandler.PlayerCharacters.Count; i++)
            {
                _battleHandler.OverwritePlayerCharacters();
                for (int j = i + 1; j < _battleHandler.PlayerCharacters.Count; j++)
                {
                    if (_battleHandler.PlayerCharacters[i].CharacterType == _battleHandler.PlayerCharacters[j].CharacterType)
                    {
                        timerOn = true;

                        timer = 3;
                        timerOn = false;
                        
                        var finger = Instantiate(_finger, _battleHandler.PlayerCharacters[i].Position,quaternion.identity);
                        finger.transform.DOMove(_battleHandler.PlayerCharacters[j].Position, 2.5f).OnComplete(() =>
                        {
                            Destroy(finger);
                        });
                        return;
                    }
                }
            }
        }
    }
}
