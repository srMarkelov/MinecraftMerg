using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AdsScriptableObject",menuName = "Scriptables/AdsScriptableObject")]

public class AdsScriptableObject : ScriptableObject
{
    [SerializeField] private List<AdsObjects> _adsObjectsList;
    public List<AdsObjects> AdsObjectsList => _adsObjectsList;
}

[Serializable]
public class AdsObjects
{
    [SerializeField] private AdsType nameAdsObject;
    [SerializeField] private GameObject gameObjectAds;
    
    public AdsType NameAdsObject => nameAdsObject;
    public GameObject GameObjectAds => gameObjectAds;
}
