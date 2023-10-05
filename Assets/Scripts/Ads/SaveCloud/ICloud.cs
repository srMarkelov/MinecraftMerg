using System;
using System.Collections;
using System.Collections.Generic;
using Core.Field;
using UnityEngine;

public interface ICloud
{
    public void OnEnable();
    public void OnDisable();
    public void Awake();
    public void SetGuideGame(GuideGame.GuideGame guideGame);
    public void SetFieldConstructor(FieldConstructor fieldConstructor);
    public void Save();
    public void GetDate();
    public void ResetSaveProgress();
    public void SetSaveCloudController(SaveCloudController saveCloudController);

    /*public bool IsAvailable();
    public void Init(AdsScriptableObject adsScriptableObject);
    public void RewardedShow(int id,Action<int> onComplete);
    public void FullscreenShow();*/

}
