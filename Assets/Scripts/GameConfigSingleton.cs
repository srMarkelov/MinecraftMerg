using Core.Characters;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "GameConfig",menuName = "Scriptables/GameConfig")]
public class GameConfigSingleton : SingletonScriptableObject<GameConfigSingleton>
{
    [SerializeField] public CharactersConfig CharactersConfig;
}
