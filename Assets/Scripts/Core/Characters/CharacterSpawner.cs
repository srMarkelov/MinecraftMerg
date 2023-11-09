using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Characters
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private LibraryController _libraryController;
        [SerializeField] private Transform _containerEnemy;
        [SerializeField] private Transform _containerPlayers;
        private readonly Dictionary<CharacterType, List<ICharacter>> _characters = new ();
        private int _currentLevel;


        private void Start()
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }

        private void Update()
        {
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }

        public void ResetLevel()
        {
            foreach (var characterList in _characters)
            {
                foreach (var character in characterList.Value)
                {
                    Destroy(character.View.gameObject);
                }
            }
            _characters.Clear();
        }

        public ICharacter SpawnCharacter(CharacterType type, Vector3 position, bool isEnemy)
        {
            if (_currentLevel < 9)
            {
                
            }
            else if (_currentLevel < 19)
            {
                if (type == CharacterType.Fist || type == CharacterType.SlingshotRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 29)
            {
                if (type == CharacterType.Stick || type == CharacterType.BigSlingshotRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 39)
            {
                if (type == CharacterType.Cudgel || type == CharacterType.AxeRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 49)
            {
                if (type == CharacterType.Hammer || type == CharacterType.SpearRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 59)
            {
                if (type == CharacterType.Shovel || type == CharacterType.LittleBowRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 69)
            {
                if (type == CharacterType.Knife || type == CharacterType.BigBowRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 79)
            {
                if (type == CharacterType.TwoKnives || type == CharacterType.CrossbowRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 89)
            {
                if (type == CharacterType.Dagger || type == CharacterType.MagicBallRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 99)
            {
                if (type == CharacterType.TwoDaggers || type == CharacterType.BigMagicBallRange)
                {
                    type = type+ 1;
                }
            }
            else
            {
                if (type == CharacterType.Mace || type == CharacterType.MagicWandRange)
                {
                    type = type+ 1;
                }
            }
            var config = GameConfigSingleton.Instance.CharactersConfig.GetConfig(type);
            var newCharacter = CharacterFactory.Create(type);
            position *= Vector2.one;
            
            if (config.View != null)
            {
                var view = Instantiate(config.View, position, Quaternion.identity, isEnemy ? _containerEnemy : _containerPlayers);
                newCharacter.View = view;
                AddNewCharacter(newCharacter);
            }
            
            return newCharacter;
            
        }public ICharacter SpawnCharacterTest(CharacterType type, Vector3 position, bool isEnemy)
        {
            if (_currentLevel < 9)
            {
                
            }
            else if (_currentLevel < 19)
            {
                if (type == CharacterType.Fist || type == CharacterType.SlingshotRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 29)
            {
                if (type == CharacterType.Stick || type == CharacterType.BigSlingshotRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 39)
            {
                if (type == CharacterType.Cudgel || type == CharacterType.AxeRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 49)
            {
                if (type == CharacterType.Hammer || type == CharacterType.SpearRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 59)
            {
                if (type == CharacterType.Shovel || type == CharacterType.LittleBowRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 69)
            {
                if (type == CharacterType.Knife || type == CharacterType.BigBowRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 79)
            {
                if (type == CharacterType.TwoKnives || type == CharacterType.CrossbowRange)
                {
                    type = type+ 1;
                }
            }
            else if (_currentLevel < 99)
            {
                if (type == CharacterType.Dagger || type == CharacterType.MagicBallRange)
                {
                    type = type+ 1;
                }
            }
            else
            {
                if (type == CharacterType.TwoDaggers || type == CharacterType.BigMagicBallRange)
                {
                    type = type+ 1;
                }
            }
            var config = GameConfigSingleton.Instance.CharactersConfig.GetConfig(type);
            var newCharacter = CharacterFactory.Create(type);
            position *= Vector2.one;
            var view = Instantiate(config.View, position, Quaternion.identity, isEnemy ? _containerEnemy : _containerPlayers);
            newCharacter.View = view;
            AddNewCharacter(newCharacter);
            view.gameObject.SetActive(false);
            return newCharacter;
        }

        private void AddNewCharacter(ICharacter character)
        {
            if (_characters.ContainsKey(character.CharacterType) == false)
            {
                _characters.Add(character.CharacterType, new List<ICharacter>());
            }
        
            _characters[character.CharacterType].Add(character);
        }

        public void DeleteCharacter(ICharacter character)
        {
            _characters[character.CharacterType].Remove(character);
            Destroy(character.View.gameObject);
        }
        
    }
}
