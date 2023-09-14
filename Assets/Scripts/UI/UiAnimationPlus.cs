 using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class UiAnimationPlus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private AudioSource _audioClick;
    private Button _thisButton;
    private List<TweenerCore<Vector3, Vector3, VectorOptions>> _tweener = new List<TweenerCore<Vector3, Vector3, VectorOptions>>();
    private bool _onPointerEnter;
    public bool _OnPointerEnters => _onPointerEnter;
    
    private void Start()
    {
        _thisButton = gameObject.GetComponent<Button>();
        if (Application.isMobilePlatform)    ////////   4
        {
            if (_thisButton.interactable)
            {
                _onPointerEnter = false;
                _tweener.Add(transform.DOScaleX(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
                _tweener.Add(transform.DOScaleY(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
            }
        }
        else
        {
            if (_thisButton.interactable)
            {
                _onPointerEnter = false;
                _tweener.Add(transform.DOScaleX(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
                _tweener.Add(transform.DOScaleY(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Application.isMobilePlatform) ////////
        {
            if (_thisButton.interactable)
            {
                _onPointerEnter = true;
                _tweener.Add(transform.DOScaleX(1.62f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
                _tweener.Add(transform.DOScaleY(1.62f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
            }
        }
        else
        {
            if (_thisButton.interactable)
            {
                _onPointerEnter = true;
                _tweener.Add(transform.DOScaleX(1.62f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
                _tweener.Add(transform.DOScaleY(1.62f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Application.isMobilePlatform)    ///////////
        {
            if (_thisButton.interactable)
            {
                _onPointerEnter = false;
                _tweener.Add(transform.DOScaleX(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
                _tweener.Add(transform.DOScaleY(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
            }
        }
        else
        {
            if (_thisButton.interactable)
            {
                _onPointerEnter = false;
                _tweener.Add(transform.DOScaleX(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
                _tweener.Add(transform.DOScaleY(1.47f, 0.2f).SetUpdate(UpdateType.Normal).SetLink(gameObject));
            }
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Application.isMobilePlatform) /////////
        {
            if (_thisButton.interactable)
            {
                _audioClick.Play();
                _tweener.Add(transform.DOScaleX(1.85f, 0.1f).SetUpdate(UpdateType.Normal));
                _tweener.Add(transform.DOScaleX(1.62f, 0.1f).SetDelay(0.1f).SetUpdate(UpdateType.Normal));

                _tweener.Add(transform.DOScaleY(1.25f, 0.1f).SetUpdate(UpdateType.Normal));
                _tweener.Add(transform.DOScaleY(1.62f,  0.1f).SetDelay(0.1f).SetUpdate(UpdateType.Normal));
            }
        }
        else
        {
            if (_thisButton.interactable)
            {
                _audioClick.Play();
                _tweener.Add(transform.DOScaleX(1.85f, 0.1f).SetUpdate(UpdateType.Normal));
                _tweener.Add(transform.DOScaleX(1.62f, 0.1f).SetDelay(0.1f).SetUpdate(UpdateType.Normal));

                _tweener.Add(transform.DOScaleY(1.25f, 0.1f).SetUpdate(UpdateType.Normal));
                _tweener.Add(transform.DOScaleY(1.62f,  0.1f).SetDelay(0.1f).SetUpdate(UpdateType.Normal));
            }
        }
        
    }

    private void OnDisable()
    {
        foreach (var tween in _tweener)
        {
            tween?.Kill();
        }
        
        _tweener.Clear();
    }
}

