using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    public Image LoadBar;
    public int SceneID;

    private void Start()
    {
        StartCoroutine("LoadSceneCor");
    }

    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(10f);
        _asyncOperation = SceneManager.LoadSceneAsync(SceneID);
        
        while (_asyncOperation.isDone == false 
               && YandexGame.SDKEnabled == false 
               && YandexGame.initializedLB 
               && YandexGame.AccessToStartTheGame == false)
        {
            float progresss = _asyncOperation.progress / 0.9f;
            LoadBar.fillAmount = progresss;
            Debug.Log("232");
            YandexGame.GameReadyAPI();
            yield return 0;
        }
        
    }
    
}
