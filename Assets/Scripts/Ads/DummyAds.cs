using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAds : IAds
{
    public bool IsAvailable()
    {
        return true;
    }

    public void Init(AdsScriptableObject adsScriptableObject)
    {
    }

    public void RewardedShow(int idReward, Action<int> onComplete)
    {
    }

    public void FullscreenShow()
    {
    }
}
