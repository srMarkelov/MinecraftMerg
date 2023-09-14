using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAds
{
    public bool IsAvailable();
    public void Init(AdsScriptableObject adsScriptableObject);
    public void RewardedShow(int id,Action<int> onComplete);
    public void FullscreenShow();
}
