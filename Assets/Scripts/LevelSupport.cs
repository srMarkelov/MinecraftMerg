using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelSupport : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;

    public void SwapBackground()
    {
        if (InputBlocker.IsLock())
        {
            return;
        }
        _background.DOColor(new Color(1, 1, 1, 0f), 1.5f);
    }
    public void SwapBackBackground()
    {
        
        _background.DOColor(new Color(1, 1, 1, 1f), 1.1f);
    }
}
