using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Core.Levels
{
    [Serializable]
    public class LevelData
    {
        public int FieldWidth;
        public int FieldHeight;
        /*public long VictoryReward;
        public long DefeatReward;*/
        /*public string VictoryReward;
        public string DefeatReward;*/
        public float VictoryReward;
        public float DefeatReward;
        public List<LevelDataCharacter> Characters;
    }
}
