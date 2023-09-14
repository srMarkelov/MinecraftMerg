using System;
using UnityEngine;
using YG;

namespace Ads
{
    public class AdsYandex : IAds
    {
        private YandexGame _yandexGame;
        private int goldInTheStorege;
        private int priseMele;
        private int priseRage;
        
        
        public bool IsAvailable()
        {
            return true; 
        }

        public void Init(AdsScriptableObject adsScriptableObject)
        {
             _yandexGame = GameObject.Instantiate(adsScriptableObject.AdsObjectsList[(int)AdsType.AdsYandexGame].GameObjectAds.GetComponent<YandexGame>());
        }

        public void RewardedShow(int id, Action<int> onComplete)
        {
            YandexGame.RewardVideoEvent += (id)=>
            {
                onComplete?.Invoke(id);
                YandexGame.RewardVideoEvent = null;
            };
            _yandexGame._RewardedShow(id);
        }

        public void FullscreenShow()
        {
            _yandexGame._FullscreenShow();
        }
    }
}
