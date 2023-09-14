using System;
using System.Collections;
using System.Collections.Generic;
using Core.Battler;
using UnityEngine;
using UnityEngine.UI;

public class SeasonsView : MonoBehaviour
{
    [SerializeField] private BattleHandler _battleHandler;
    [SerializeField] private int _summerLevel;
    [SerializeField] private int _autumnLevel;
    [SerializeField] private int _snowLevel;
    [SerializeField] private int _springLevel;
    
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _background2;
    [SerializeField] private Image _buyMelee;
    [SerializeField] private Image _buyRange;
    
    [SerializeField] private GameObject _autumnGameObjects;
    [SerializeField] private GameObject _springGameObjects;
    [SerializeField] private ParticleSystem _snowParticleSystem;
    [SerializeField] private List<GameObject> _snowGameObjects;

    [SerializeField] private List<Sprite> _backgroundSprites;
    [SerializeField] private List<Sprite> _buyMeleeSprites;
    [SerializeField] private List<Sprite> _buyRangeSprites;
    /*
    [SerializeField] private List<Sprite> _buyStartGameSprites;
    */
    [SerializeField] private List<AudioSource> _audioSources;

    private int _currentLevel;
    
    private bool _summer;
    private bool _autumn;
    private bool _snowL;
    private bool _spring;
    private void Update()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        SetActiveSeasonsSprites();
    }

    public void SetActiveSeasonsSprites()
    {
        if(_battleHandler.CheckWinAndLose) return;
        
        if(_currentLevel >= _summerLevel && _currentLevel < _autumnLevel || 
           _currentLevel >= _summerLevel && _currentLevel < _autumnLevel)
        {
            SetSummerSprites();
        }
        else if(_currentLevel >= _autumnLevel && _currentLevel < _snowLevel)
        {
            SetAutumnSprites();
        }
        else if (_currentLevel >= _snowLevel && _currentLevel < _springLevel)
        { 
            _snowParticleSystem.gameObject.SetActive(true);
            SetSnowSprites();
        }
        else if (_currentLevel >= _springLevel)
        {
            SetSpringSprites();
        }
    }
    
    private void SetSummerSprites()
    {
        _audioSources[0].enabled = true;
        _audioSources[1].enabled = false;
        
        _background.sprite = _backgroundSprites[0];
        _background2.sprite = _backgroundSprites[0];
        /*_buyMelee.sprite = _buyMeleeSprites[0];
        _buyRange.sprite = _buyRangeSprites[0];
        _startGame.sprite = _buyStartGameSprites[0];*/
        foreach (var gameObject in _snowGameObjects)
        {
            gameObject.SetActive(false);
        }
        _autumnGameObjects.SetActive(false);
        _springGameObjects.SetActive(false);
    }
    
    private void SetAutumnSprites()
    {
        _audioSources[0].enabled = false;
        _audioSources[1].enabled = false;
        
        _autumnGameObjects.SetActive(true);
        _background.sprite = _backgroundSprites[1];
        _background2.sprite = _backgroundSprites[1];
        /*_buyMelee.sprite = _buyMeleeSprites[1];
        _buyRange.sprite = _buyRangeSprites[1];
        _startGame.sprite = _buyStartGameSprites[1];*/
        foreach (var gameObject in _snowGameObjects)
        {
            gameObject.SetActive(false);
        }
        _springGameObjects.SetActive(false);
    }
    
    private void SetSnowSprites()
    {
        _audioSources[0].enabled = false;
        _audioSources[1].enabled = true;

        _background.sprite = _backgroundSprites[2];
        _background2.sprite = _backgroundSprites[2];
        /*_buyMelee.sprite = _buyMeleeSprites[2];
        _buyRange.sprite = _buyRangeSprites[2];
        _startGame.sprite = _buyStartGameSprites[2];*/
        foreach (var gameObject in _snowGameObjects)
        {
            gameObject.SetActive(true);
        }
        _autumnGameObjects.SetActive(false);
        _springGameObjects.SetActive(false);
    }
    
    private void SetSpringSprites()
    {
        _audioSources[0].enabled = true;
        _audioSources[1].enabled = false;


        _background.sprite = _backgroundSprites[3];
        _background2.sprite = _backgroundSprites[3];
        /*_buyMelee.sprite = _buyMeleeSprites[3];
        _buyRange.sprite = _buyRangeSprites[3];
        _startGame.sprite = _buyStartGameSprites[3];*/
        foreach (var gameObject in _snowGameObjects)
        {
            gameObject.SetActive(false);
        }
        _autumnGameObjects.SetActive(false);
        _snowParticleSystem.gameObject.SetActive(false);
        _springGameObjects.SetActive(true);
    }
}
