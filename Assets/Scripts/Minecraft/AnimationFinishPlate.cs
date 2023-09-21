using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationFinishPlate : MonoBehaviour
{
    void Start()
    {
        MoreAnimation();
    }

    private void MoreAnimation()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            LessAnimation();
        });
    }

    private void LessAnimation()
    {
        transform.DOScale(Vector3.one,2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            MoreAnimation();
        });
    }
}
