using System;
using System.Collections.Generic;
using Core.Field;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace GuideGame
{
    public class GuideGame : MonoBehaviour
    {
        [SerializeField] private GameSupport _gameSupport;
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private Button _buyRange;
        [SerializeField] private Button _buyMelee;
        [SerializeField] private Button _startGame;
        [SerializeField] private GameObject _fingerGuid;
        private GameObject fingerGuidInst;

        private int currentLevel;
        
        private bool StepOnePassed;
        private bool StepTwoPassed;
        private bool StepThreePassed;
        


        public void StartGuide()
        {
            _buyRange.onClick.AddListener(GuideStepTwo);
            _buyMelee.onClick.AddListener(GuideStepThree);
            _startGame.onClick.AddListener(FinishGuide);
            GuideStepOne();
        }

        private void GuideStepOne()
        {
            if (currentLevel > 0)
                return;
            foreach (var button in _buttons)
            {
                button.interactable = false;
            }
            _buttons[0].interactable = true;
            fingerGuidInst = Instantiate(_fingerGuid,_buyRange.transform);
            
        }
        private void GuideStepTwo()
        {
            if (currentLevel > 0)
                return;
            foreach (var button in _buttons)
            {
                button.interactable = false;
            }
            _buttons[1].interactable = true;
            fingerGuidInst.transform.parent = _buyMelee.transform;
        }
        private void GuideStepThree()
        {
            if (currentLevel > 0)
                return;
            foreach (var button in _buttons)
            {
                button.interactable = false;
            }
            _buttons[2].interactable = true;
            fingerGuidInst.transform.parent = _startGame.transform;
        }

        public void FinishGuide()
        {
            if (currentLevel > 0)
                return;
            _buyRange.onClick.RemoveListener(GuideStepTwo);
            _buyMelee.onClick.RemoveListener(GuideStepThree);
            _startGame.onClick.RemoveListener(FinishGuide);
            foreach (var button in _buttons)
            {
                button.interactable = true;
            }
            Destroy(fingerGuidInst);
        }
    }
}
