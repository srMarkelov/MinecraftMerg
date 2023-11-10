using System;
using Core.Battler;
using Core.Characters;
using Core.Field;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class MouseTracker : MonoBehaviour
    {
        [SerializeField] private FieldConstructor _fieldConstructor;
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private LibraryController _libraryController;
        [SerializeField] private AudioSource _audioSpawn;
        [SerializeField] private AudioSource _audioClick;
        [SerializeField] private BattleHandler _battleHandler;
        [SerializeField] private UiLevelManager _uiLevelManager;
        [SerializeField] private FinishLevelView _finishLevelView;

        private int oldCharacterTypeMelee = 0;
        private int oldCharacterTypeRange = 0;
        private bool MousDown;


        private ICharacter MovingCharacter;
        private Cell OldCell;

        public void ResetLevel()
        {
            MovingCharacter = null;
            OldCell = null;
        }

        public void StartGame()
        {
            oldCharacterTypeMelee = PlayerPrefs.GetInt("OldCharacterTypeMelee");
            oldCharacterTypeRange = PlayerPrefs.GetInt("OldCharacterTypeRange");
        }

        private void Update()
        {
            if (_finishLevelView.OnFinishPanel)
                return; 
            
            if (MovingCharacter != null)
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MovingCharacter.View._Character = MovingCharacter;
                MovingCharacter.View.SetPosition(pos);
            }

            if (_battleHandler.IsBattle || 
                _uiLevelManager.OnSetting||
                _uiLevelManager.OnLibrary||
                _libraryController.ShowNewCharacter)
            {
                return;
            }
            
            if (MousDown)
            {
                foreach (var cell in _fieldConstructor.Cells)
                {
                    cell.OffAndOnColorFinishButton();
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                
                ChangeCell();
            }

            if (Input.GetMouseButtonUp(0))
            {
                MousDown = false;
                firstCell = null;

                ExchangeCharacter();
            }

            if (firstCell != null)
            {
                firstCell.SetColor();
            }
        }

        private Cell firstCell;
        private bool Test;
        private void ChangeCell()
        {
            foreach (var cell in _fieldConstructor.Cells)
            {
                if (cell.InCell(Camera.main.ScreenToWorldPoint(Input.mousePosition))
                    && cell.IsBusy())
                {
                    if (MousDown == false)
                    {
                        firstCell = cell;
                    }
                    MousDown = true;

                    _audioClick.Play();
                    MovingCharacter = cell.Character;
                    OldCell = cell;
                }
            }
        }

        private void ExchangeCharacter()
        {
            if (MovingCharacter == null)
                return;
            
            foreach (var cell in _fieldConstructor.Cells)
            {
                cell.OffColor();

                if(cell == OldCell)
                    continue;
                
                if (cell.InCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    if (cell.IsBusy() == false)
                    {
                        cell.Character = MovingCharacter;
                        cell.Character.View.SetPosition(cell.transform.position);
                        cell.Character.Position = cell.transform.position;

                        OldCell.Character = null;
                        OldCell = null;
                        MovingCharacter = null;
                        SaveCloudController.singleton.ICloud.Save();
                        return;
                    }

                    if (cell.Character.CharacterType == MovingCharacter.CharacterType 
                        && MovingCharacter.DamageType == DamageType.Melee
                        )
                    {
                        var type = MovingCharacter.CharacterType;
                        if (type == CharacterType.BlueSword)
                        {
                            continue;  
                        }
                        var character = MovingCharacter;
                        OldCell.Character = null;
                        OldCell = null;
                        MovingCharacter = null;
                        
                        _characterSpawner.DeleteCharacter(cell.Character);
                        _characterSpawner.DeleteCharacter(character);
                        cell.Character = _characterSpawner.SpawnCharacter((CharacterType)((int)type + 1),
                            cell.transform.position, false);
                        CheckNewCharacterType((CharacterType)((int)type + 1));

                        cell.Character.Position = cell.transform.position;
                        _audioSpawn.Play();
                    }
                    
                    else if (cell.Character.CharacterType == MovingCharacter.CharacterType 
                        && MovingCharacter.DamageType == DamageType.Range
                       )
                    {
                        var type = MovingCharacter.CharacterType;
                        if (type == CharacterType.BowIronCharacter)
                        {
                            continue;  
                        }
                        var character = MovingCharacter;
                        OldCell.Character = null;
                        OldCell = null;
                        MovingCharacter = null;
                        _characterSpawner.DeleteCharacter(cell.Character);
                        _characterSpawner.DeleteCharacter(character);
                        cell.Character = _characterSpawner.SpawnCharacter((CharacterType)((int)type + 1),
                            cell.transform.position, false);
                        CheckNewCharacterType((CharacterType)((int)type + 1));

                        cell.Character.Position = cell.transform.position;
                        _audioSpawn.Play();

                    }
                    else
                    {
                        var a = MovingCharacter;
                        var b = cell.Character;
                        _characterSpawner.DeleteCharacter(a);
                        _characterSpawner.DeleteCharacter(b);

                        OldCell.Character = _characterSpawner.SpawnCharacter(b.CharacterType, OldCell.transform.position,
                            false);
                        cell.Character = _characterSpawner.SpawnCharacter(a.CharacterType, cell.transform.position,
                            false);
                        /*MovingCharacter.View.SetPosition(b.Position);
                        cell.Character.View.SetPosition(a.Position);*/
                    }
                    SaveCloudController.singleton.ICloud.Save();

                    break;
                }
            }

            if (MovingCharacter != null)
            {
                MovingCharacter.View.SetPosition(OldCell.transform.position);
                OldCell = null;
            }
            SaveCloudController.singleton.ICloud.Save();


            MovingCharacter = null;
        }
        
        


        private void CheckNewCharacterType(CharacterType characterType)
        {
            if ((int)characterType < 18)
            {
                if ((int)characterType > oldCharacterTypeMelee)
                {
                    oldCharacterTypeMelee++;
                    PlayerPrefs.SetInt("OldCharacterTypeMelee", oldCharacterTypeMelee);
                    _libraryController.SetSpriteCharactersLibrary(oldCharacterTypeMelee, characterType);
                }
            }

            if ((int)characterType < 36 && (int)characterType > 17)
            {
                if ((int)characterType > oldCharacterTypeRange+18)
                {
                    oldCharacterTypeRange++;
                    PlayerPrefs.SetInt("OldCharacterTypeRange", oldCharacterTypeRange);
                    _libraryController.SetSpriteCharactersLibraryRange(oldCharacterTypeRange, characterType);
                }
            }
        }
    }
}
