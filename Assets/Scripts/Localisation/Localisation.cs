using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


public class Localisation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bacground;
    [SerializeField] private List<GameObject> _langButtens;
    
    private bool active;
    private bool _onLangPanel;
    private bool starsGame;
    private void Start()
    {
        var lang = PlayerPrefs.GetInt("Localization", 0);
        Delay(lang);
    }

    public void CheckScaleButtons(int lang)
    {
        if (starsGame == false)
        {
            starsGame = true;
            return;
        }
        for (int i = 0; i < _langButtens.Count; i++)
        {
            if (lang == i)
            {
                _langButtens[i].transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f);
            }
            else
            {
                _langButtens[i].transform.DOScale(new Vector3(1.47f, 1.47f, 1.47f), 0.5f);
            }
        }
    }

    public void Delay(int lang)
    {
        if (active)
            return;
        bacground.DOColor(new Color(bacground.color.r, bacground.color.g, bacground.color.b, 1f),0.5f).OnComplete(() =>
        {
            StartCoroutine( SetLocalisation(lang));
        });
        CheckScaleButtons(lang);
    }

    public IEnumerator SetLocalisation(int lang)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lang];
        PlayerPrefs.SetInt("Localization", lang);
        yield return LocalizationSettings.StartupLocaleSelectors;
        yield return new WaitForSeconds(0.3f);

        active = false;
        bacground.DOColor(new Color(bacground.color.r, bacground.color.g, bacground.color.b, 0f),1f).OnComplete(() =>
        {
            
        });
    }
    
    public void OnLibraryPanel()
    {
        if (_onLangPanel == false)
        {
            _onLangPanel = true;

            foreach (var item in _langButtens)
            {
                item.SetActive(true);

                item.transform.DOScale(new Vector3(1f, 1f, 1f), 0.6f).
                    SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        CheckScaleButtons(PlayerPrefs.GetInt("Localization"));
                    }).SetLink(gameObject);

            }

        }
        else
        {
            foreach (var item in _langButtens)
            {
                item.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.8f).
                    SetEase(Ease.InBack).OnComplete(() =>
                    {
                        _onLangPanel = false;

                        item.SetActive(false);
                    }).SetLink(gameObject);
            }
        }
    }
    
}
