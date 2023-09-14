using System;
using Core.Characters;

namespace Core.Levels
{
    [Serializable]
    public struct LevelDataCharacter
    {
        public CharacterType Type;
        public int PositionX;
        public int PositionY;
    }
}
