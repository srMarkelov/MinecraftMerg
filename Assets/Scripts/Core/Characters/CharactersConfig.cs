using System;
using TMPro;
using UnityEngine;
using Utils;

namespace Core.Characters
{
    [CreateAssetMenu(fileName = "CharactersConfig",menuName = "Scriptables/CharactersConfig")]
    public class CharactersConfig : ScriptableObject
    {
        [SerializeField] private CharactersDictionary _charactersDictionary;

        public CharacterConfig GetConfig(CharacterType type)
        {
            return _charactersDictionary[type];
        }

        public CharacterConfig GetUpLevelConfig(CharacterType type)
        {
            var typeLevelNext = (CharacterType)((int)type + 1);
            if (_charactersDictionary.ContainsKey(typeLevelNext))
            {
                return _charactersDictionary[typeLevelNext];
            }

            return _charactersDictionary[type];
        }
    }
    
    [Serializable]
    public class CharactersDictionary : SerializableDictionary<CharacterType, CharacterConfig>{}
        
    [Serializable]
    public struct CharacterConfig
    {
        public DamageType DamageType;
        public float Health;
        public float Damage;
        public float MovementSpeed;
        public float DamageSpeed;
        public int KillReward;
        public int WeightMeleeCharacters;
        public CharacterView View;
    }
}
