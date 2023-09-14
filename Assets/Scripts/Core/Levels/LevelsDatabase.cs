using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Levels
{
    [CreateAssetMenu(fileName = "LevelsDatabase",menuName = "Scriptables/LevelsDatabase")]
    public class LevelsDatabase : ScriptableObject
    {
        public List<LevelData> LevelDatas;
    }
    
}
