using System;
using System.Collections.Generic;
using Ads;
using Core.Field;
using DG.Tweening;
using Spine.Unity.Examples;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Core.Characters
{
    public class CharacterSeller : MonoBehaviour
    {
        private static string MoneyInTheStorage = "MoneyInTheStorage";

        private static string PriceMeleeS = "PriceMelee"; // Remove name
        private static string PriceRangeS = "PriceRange"; // Remove name
        [SerializeField] private AdsYandex _adsYandex;
        [SerializeField] private Button _btnBuy;
        [SerializeField] private Button _btnBuyRange;
        [SerializeField] private CharacterSpawner _spawner;
        [SerializeField] private FieldConstructor _fieldConstructor;
        [SerializeField] private StorageVariable _storageVariable;
        [SerializeField] private TextMeshProUGUI _countBuyMeleeText;
        [SerializeField] private TextMeshProUGUI _countBuyRangeText;
        [SerializeField] private TextMeshProUGUI _priceMeleeCharacterText;
        [SerializeField] private TextMeshProUGUI _priceMeleeCharacterTextBackground;
        [SerializeField] private TextMeshProUGUI _priceRangeCharacterText;
        [SerializeField] private TextMeshProUGUI _priceRangeCharacterTextBackground;
        [SerializeField] private MoneyСonverting _moneyСonverting;
        [SerializeField] private UiLevelManager _uiLevelManager;
        [SerializeField] private SpriteRenderer _fullCells;


        [SerializeField] private float PRm;
        [SerializeField] private float PRr;
        [SerializeField] private int _oldLenelCorrect;

        private YandexGame _yandexGame;
        
        private int _currentLevel;
        private int _oldLevel;
        
        private int _countBuyMelee;
        private int _countBuyRange;

        private float _priceMelee;
        private float _priceRange;
        public float  PriceMelee =>_priceMelee;
        public float  PriceRange =>_priceRange;
        
        private string _priceMeleeStr;
        private string _priceRangeStr;
        public string  PriceMeleeStr =>_priceMeleeStr;
        public string  PriceRangeStr =>_priceRangeStr;
        
        private int _idAddRangeCharacter = 0;
        private int _idAddMeleeCharacter = 1;

        private bool dontClickRewardBuyButton;
        private bool takeCellsFilled;
        public bool TakeCellsFilled => takeCellsFilled;

        

        private void OnEnable()
        {
            _btnBuy.onClick.AddListener(BuyMelee);
            _btnBuyRange.onClick.AddListener(BuyRange);
        }

        private void OnDisable()
        {
            _btnBuy.onClick.RemoveListener(BuyMelee);
            _btnBuyRange.onClick.RemoveListener(BuyRange);
        }
        
        private void Start()
        {
            _countBuyMelee = PlayerPrefs.GetInt("CountMelee");
            _countBuyRange = PlayerPrefs.GetInt("CountRange");
            
            _oldLevel  = PlayerPrefs.GetInt("OldLevel");
        }

        public void StartGame()
        {
            _priceMelee = PlayerPrefs.GetFloat("PriceMelee");
            _priceRange = PlayerPrefs.GetFloat("PriceRange");

            _moneyСonverting.GoldConverting(PriceMeleeS,_priceMelee);
            _moneyСonverting.GoldConverting(PriceRangeS,_priceRange);
            
            _priceRangeCharacterText.text = _moneyСonverting.GetMoney(PriceRangeS);
            _priceRangeCharacterTextBackground.text = _moneyСonverting.GetMoney(PriceRangeS);
            
            _priceMeleeCharacterText.text = _moneyСonverting.GetMoney(PriceMeleeS);
            _priceMeleeCharacterTextBackground.text = _moneyСonverting.GetMoney(PriceMeleeS);
        }
        private void Update()
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        }

        public void Reward(int idReward)
        {
            if (InputBlocker.IsLock())
            {
                return;
            }
            InputBlocker.InputLock(this,0.5f);
            
            if (idReward == (int)AdsRewardType.IdAddRangeCharacter)
            {
                Invoke("BuyRewardRange",0.3f);
            }
            else if (idReward == (int)AdsRewardType.IdAddMeleeCharacter)
            {
                Invoke("BuyRewardMelee",0.3f);
            }
            SaveCloudController.singleton.ICloud.Save();
        }
        public void CorrectOldLevel()
        {
            PlayerPrefs.SetInt("OldLevel", _oldLenelCorrect);
        }
        public void SetPriceInInspector()
        {
            PlayerPrefs.SetFloat("PriceMelee",PRm);
            PlayerPrefs.SetFloat("PriceRange", PRr);
        }


        private void CorrectedPriceCharacters()
        {
            _priceMelee = _moneyСonverting.Multiplication(PriceMeleeS,1.12f);
            
            _priceMelee = _moneyСonverting.GoldConverting(PriceMeleeS, _priceMelee);
            _priceMeleeCharacterText.text = _moneyСonverting.GetMoney(PriceMeleeS);
            _priceMeleeCharacterTextBackground.text = _moneyСonverting.GetMoney(PriceMeleeS);
            
            PlayerPrefs.SetString("PriceMelee",_priceMeleeStr);
            PlayerPrefs.SetFloat(PriceMeleeS,_priceMelee);
        }
        
        private void CorrectedPriceRangeCharacters()
        {
            _priceRange = _moneyСonverting.Multiplication(PriceRangeS,1.12f);
            _priceRange = _moneyСonverting.GoldConverting(PriceRangeS, _priceRange);
            _priceRangeCharacterText.text = _moneyСonverting.GetMoney(PriceRangeS);
            _priceRangeCharacterTextBackground.text = _moneyСonverting.GetMoney(PriceRangeS);

            PlayerPrefs.SetString("PriceRange",_priceRangeStr);
            PlayerPrefs.SetFloat("PriceRange",_priceRange);
        }

        private int CorrectingFigureRange;
        private int CorrectingFigureMelee;


        private float _totalPercentage;
        private float _higherPercentage;
        private float _smallerPercentages;
        
        public void CorrectedPriceAllCharacters()
        {
            if (_currentLevel < 3)
            {
                _totalPercentage = 1f;
                _higherPercentage = 1f;
                _smallerPercentages = 1f;
            }
            else if (_currentLevel < 19)
            {
                _totalPercentage = 1.2f;
                _higherPercentage = 1.24f;
                _smallerPercentages = 1.16f;
            }
            else if (_currentLevel < 29)
            {
                _totalPercentage = 1.25f;
                _higherPercentage = 1.3f;
                _smallerPercentages = 1.2f;
            }
            else
            {
                _totalPercentage = 1.3f;
                _higherPercentage = 1.36f;
                _smallerPercentages = 1.24f;
            }
            
            if (((_countBuyRange - _countBuyMelee) < 2) && ((_countBuyRange - _countBuyMelee) > -2) )
            {
                _priceRange = _moneyСonverting.Multiplication(PriceRangeS, _totalPercentage); 
                _priceMelee = _moneyСonverting.Multiplication(PriceMeleeS, _totalPercentage); 
            }
            else
            {
                if (_priceRange < _priceMelee)
                {
                    _priceRange = _moneyСonverting.Multiplication(PriceRangeS, _higherPercentage);   //60%
                    _priceMelee = _moneyСonverting.Multiplication(PriceMeleeS, _smallerPercentages); //40%
                        
                }
                else
                {
                    _priceRange = _moneyСonverting.Multiplication(PriceRangeS, _smallerPercentages);
                    _priceMelee = _moneyСonverting.Multiplication(PriceMeleeS, _higherPercentage);
                }
            }
            _oldLevel += 1;
            PlayerPrefs.SetInt("OldLevel", _oldLevel);
            _priceMeleeCharacterText.text = _moneyСonverting.GetMoney(PriceMeleeS);
            _priceMeleeCharacterTextBackground.text = _moneyСonverting.GetMoney(PriceMeleeS);
            
            _priceRangeCharacterText.text = _moneyСonverting.GetMoney(PriceRangeS);
            _priceRangeCharacterTextBackground.text = _moneyСonverting.GetMoney(PriceRangeS);

            PlayerPrefs.SetFloat(PriceMeleeS,_priceMelee);
            PlayerPrefs.SetFloat(PriceRangeS,_priceRange);
            /*if (_currentLevel > _oldLevel) //--
            {
                
            }*/
            PlayerPrefs.SetString("PriceMelee",_priceMeleeStr);
            PlayerPrefs.SetString("PriceRange",_priceRangeStr);
            
            PlayerPrefs.SetFloat("PriceMelee",_priceMelee);
            PlayerPrefs.SetFloat("PriceRange",_priceRange);
        }
        
        public void TestSeller()
        {
            /*var freeCell = _fieldConstructor.GetFreeCell();
            freeCell.Character = _spawner.SpawnCharacter(CharacterType.TwoDaggers,
                freeCell.transform.position, false);*/
        }

    
        private void BuyMelee()
        {
            if (dontClickRewardBuyButton)
                return;
            
            if (takeCellsFilled)
                return;
            
            var freeCell = _fieldConstructor.GetFreeCell();
            if (freeCell == null)
            {
                IncludeCellsFilled();
                return;
            }

            if (_uiLevelManager.NoMoneyStateMelee)
            {
                AdsController.singleton.iAds.RewardedShow((int)AdsRewardType.IdAddMeleeCharacter,Reward);
                dontClickRewardBuyButton = true;
                return;
            }

            var moneyInStorage = _storageVariable.GetMoneyInTheStorage();
            if (_moneyСonverting.MoreOrEqual(MoneyInTheStorage, PriceMeleeS))
            {
                _countBuyMelee++;
                PlayerPrefs.SetInt("CountMelee", _countBuyMelee);
                _storageVariable.RemoveMoney(PriceMeleeS);
                CorrectedPriceCharacters();
                
                _countBuyMeleeText.text = _countBuyMelee.ToString();
                
                if (_currentLevel < 9)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Fist,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 19)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Stick,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 29)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Cudgel,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 39)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Hammer,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 49)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Shovel,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 59)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Knife,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 69)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.TwoKnives,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 79)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Dagger,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 89)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.TwoDaggers,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 99)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Mace,
                        freeCell.transform.position, false);
                }
                else 
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.Saber,
                        freeCell.transform.position, false);
                }
                
                StartCoroutine(_uiLevelManager.CheckStorageAndPriceMelee());
                StartCoroutine(_uiLevelManager.CheckStorageAndPriceRange());
                
                SaveCloudController.singleton.ICloud.Save();
            }
        }
        private void BuyRange()
        {
            if (dontClickRewardBuyButton)
                return;
            
            if (takeCellsFilled)
                return;
            
            var freeCell = _fieldConstructor.GetFreeCell();
            
            if (freeCell == null)
            {
                IncludeCellsFilled();
                return;
            }

            if (_uiLevelManager.NoMoneyStateRange)
            {
                AdsController.singleton.iAds.RewardedShow((int)AdsRewardType.IdAddRangeCharacter,Reward);
                dontClickRewardBuyButton = true;
                return;
            }
            
            var moneyInStorage = _storageVariable.GetMoneyInTheStorage();
            if (_moneyСonverting.MoreOrEqual(MoneyInTheStorage, PriceRangeS))
            {
                
                _countBuyRange++;
                PlayerPrefs.SetInt("CountRange", _countBuyRange);
                _storageVariable.RemoveMoney(PriceRangeS);
                CorrectedPriceRangeCharacters();
                
                _countBuyRangeText.text = _countBuyRange.ToString();
                
                if (_currentLevel < 9)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.SlingshotRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 19)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.BigSlingshotRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 29)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.AxeRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 39)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.SpearRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 49)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.LittleBowRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 59)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.BigBowRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 69)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.CrossbowRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 79)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.MagicBallRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 89)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.BigMagicBallRange,
                        freeCell.transform.position, false);
                }
                else if (_currentLevel < 99)
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.MagicWandRange,
                        freeCell.transform.position, false);
                }
                else
                {
                    freeCell.Character = _spawner.SpawnCharacter(CharacterType.BadStaffRange,
                        freeCell.transform.position, false);
                }
                
                StartCoroutine(_uiLevelManager.CheckStorageAndPriceRange());
                StartCoroutine(_uiLevelManager.CheckStorageAndPriceMelee());

                SaveCloudController.singleton.ICloud.Save();
            }
        }
        
        public void BuyRewardMelee()
        {
            if (takeCellsFilled)
                return;
            
            var freeCell = _fieldConstructor.GetFreeCell();
            if (freeCell == null)
            {
                IncludeCellsFilled();
                return;
            }
            
            _countBuyMelee++;
            PlayerPrefs.SetInt("CountMelee", _countBuyMelee);
            _countBuyMeleeText.text = _countBuyMelee.ToString();
            
            if (_currentLevel < 9)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Fist,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 19)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Stick,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 29)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Cudgel,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 39)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Hammer,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 49)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Shovel,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 59)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Knife,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 69)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.TwoKnives,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 79)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Dagger,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 89)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.TwoDaggers,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 99)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Mace,
                    freeCell.transform.position, false);
            }
            else
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.Saber,
                    freeCell.transform.position, false);
            }
            
            StartCoroutine(_uiLevelManager.CheckStorageAndPriceMelee());
            StartCoroutine(_uiLevelManager.CheckStorageAndPriceRange());
            dontClickRewardBuyButton = false;

        }

        public void BuyRewardRange()
        {
            // Insert call reward advertisement
            if (takeCellsFilled)
                return;
            
            var freeCell = _fieldConstructor.GetFreeCell();
            if (freeCell == null)
            {
                IncludeCellsFilled();
                return;
            }

            _countBuyRange++;
            PlayerPrefs.SetInt("CountRange", _countBuyRange);
            _countBuyRangeText.text = _countBuyRange.ToString();
            
            if (_currentLevel < 9)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.SlingshotRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 19)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.BigSlingshotRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 29)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.AxeRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 39)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.SpearRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 49)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.LittleBowRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 59)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.BigBowRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 69)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.CrossbowRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 79)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.MagicBallRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 89)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.BigMagicBallRange,
                    freeCell.transform.position, false);
            }
            else if (_currentLevel < 99)
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.MagicWandRange,
                    freeCell.transform.position, false);
            }
            else
            {
                freeCell.Character = _spawner.SpawnCharacter(CharacterType.BadStaffRange,
                    freeCell.transform.position, false);
            }
            
            StartCoroutine(_uiLevelManager.CheckStorageAndPriceRange());
            StartCoroutine(_uiLevelManager.CheckStorageAndPriceMelee());
            dontClickRewardBuyButton = false;
        }
        public void SpawnCharacter(CharacterType characterType)
        {
            // Insert call reward advertisement
            
            var freeCell = _fieldConstructor.GetFreeCell();
            if(freeCell == null) return;
            
            _countBuyMelee++;
            PlayerPrefs.SetInt("CountMelee", _countBuyMelee);
            _countBuyMeleeText.text = _countBuyMelee.ToString();
            
            freeCell.Character = _spawner.SpawnCharacterTest(characterType,
                freeCell.transform.position, false);
        }
        public void IncludeCellsFilled()
        {
            takeCellsFilled = true;
            _fullCells.gameObject.SetActive(true);
            _fullCells.DOColor(new Color(1, 1, 1, 1), 0.45f);
            DOVirtual.DelayedCall(1.5f, () =>
            {
                _fullCells.DOColor(new Color(1, 1, 1, 0f), 0.45f).OnComplete(() =>
                {
                    takeCellsFilled = false;
                    _fullCells.gameObject.SetActive(false);
                });
            });
        }
    }
}
