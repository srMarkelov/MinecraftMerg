/*using System;
using System.Collections;
using System.Collections.Generic;
using Ads;
using Unity.VisualScripting;*/

using Ads;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AdsController : MonoBehaviour
{
    [SerializeField] private AdsScriptableObject adsScriptableObject;
    public static AdsController singleton { get; private set; }
    
    public IAds iAds;
    
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject );
        }
        singleton = this;
        
#if UNITY_WEBGL
        iAds = new AdsYandex();
#endif
    }
    private void Start()
    {
        iAds.Init(adsScriptableObject);
    }
}



