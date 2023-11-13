using System;
using Core.Characters.Std;

namespace Core.Characters
{
    public static class CharacterFactory
    {
        public static ICharacter Create(CharacterType type)
        {
            ICharacter character;
            switch (type)
            {
                case CharacterType.Fist:
                    character = new FistCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Stick:
                    character = new StickCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Cudgel:
                    character = new CudgelCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Hammer:
                    character = new HammerCharacters();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Shovel:
                    character = new ShovelCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Knife:
                    character = new KnifeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.TwoKnives:
                    character = new TwoKnivesCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Dagger:
                    character = new DaggerCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.TwoDaggers:
                    character = new TwoDaggersCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Mace:
                    character = new MaceCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Saber:
                    character = new SaberCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.TwoAxes:
                    character = new TwoAxesCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Spear:
                    character = new SpearCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.Sword:
                    character = new SwordCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BigAx:
                    character = new BigAxCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BigSword:
                    character = new BigSwordCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BluePick:
                    character = new BluePickCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BlueSword:
                    character = new BlueSwordCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                
                case CharacterType.SlingshotRange:
                    character = new SlingshotRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BigSlingshotRange:
                    character = new BigSlingshotRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.AxeRange:
                    character = new AxeRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.SpearRange:
                    character = new SpearRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.LittleBowRange:
                    character = new LittleBowRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BigBowRange:
                    character = new BigBowRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.CrossbowRange:
                    character = new CrossbowRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.MagicBallRange:
                    character = new MagicBallRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BigMagicBallRange:
                    character = new BigMagicBallRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.MagicWandRange:
                    character = new MagicWandRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BadStaffRange:
                    character = new BadStaffRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.MediumStaffRange:
                    character = new MediumStaffRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.GoodStaffRange:
                    character = new GoodStaffRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.StaffAndOrbRange:
                    character = new StaffAndOrbRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.FireRange:
                    character = new FireRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BlueFireRange:
                    character = new BlueFireRangeCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BowGoldCharacter:
                    character = new BowGoldCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.BowIronCharacter:
                    character = new BowIronCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                
                
                case CharacterType.FirstCharacterBoss:
                    character = new FirstCharacterBoss();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.SecondCharacterBoss:
                    character = new SecondCharacterBoss();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.ThirdCharacterBoss:
                    character = new ThirdCharacterBoss();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.FourthCharacterBoss:
                    character = new FourthCharacterBoss();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.FifthBossCharacter:
                    character = new FifthBossCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.SixthBossCharacter:
                    character = new SixthBossCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                case CharacterType.SeventhBossCharacter:
                    character = new SeventhBossCharacter();
                    character.Init(GameConfigSingleton.Instance.CharactersConfig.GetConfig(type));
                    return character;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
