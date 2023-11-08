using System;
using System.Collections;
using System.Collections.Generic;
using Core.Characters;
using Core.Field;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private FieldConstructor _constructor;
    [SerializeField] private CharactersConfig _characterConfig;
    
    List<ICharacter> _meleeCharactersInField = new List<ICharacter>();
    List<ICharacter> _rangeCharactersInField = new List<ICharacter>();
    
    private Dictionary<(int, int), CharacterType> _characters = new Dictionary<(int, int), CharacterType>();


    private int _weightMeleeCharacters;
    private int _weightRangeCharacters;

    private void Start()
    {
        Invoke("WeightCharacterInField",1f);
    }
    

    private void WeightCharacterInField()
    {
        foreach (var cells in _constructor.Cells)
        {
            if (cells.IsBusy())
            {
                if (cells.Character.DamageType == DamageType.Melee )
                {
                    _meleeCharactersInField.Add(cells.Character);
                    _weightMeleeCharacters += cells.Character.WeightCharacters;
                }
                else
                {
                    _rangeCharactersInField.Add(cells.Character);
                    _weightRangeCharacters += cells.Character.WeightCharacters;
                }
            }
        }
        Debug.Log(_weightMeleeCharacters);
        Debug.Log(_weightRangeCharacters);
        
    }

}
