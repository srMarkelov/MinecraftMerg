using System;
using System.Collections.Generic;
using Core.Characters;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Field
{
    public class FieldConstructor : MonoBehaviour
    {
        [SerializeField] private Cell Cell;
        [SerializeField] private Transform LeftUpPoint;
        [SerializeField] private float Width;
        [SerializeField] private float Height;
        [SerializeField] private CharacterSpawner _spawner;
        [SerializeField] private List<Cell> _cells;
        [SerializeField] private FieldConstructorDatabase _fieldConstructorDatabase;

        private Dictionary<(int, int), CharacterType> _characters = new Dictionary<(int, int), CharacterType>();
        public List<Cell> Cells => _cells;
        private readonly List<Cell> _field = new List<Cell>();
        public List<Cell> Field => _field;
        private bool restartGame; 
        
        private int[] characterTypeFOrCell = new int[15];
        public int[] CharacterTypeFOrCell => characterTypeFOrCell;
        
        
        private int[] _cellWidth = new int[15];
        public int[] CellWidth => _cellWidth;
        
        
        private int[] _cellHeight = new int[15];
        public int[] CellHeight => _cellHeight;
        

        private void Start()
        {
            /*
            SpawnCharactersOnTheField();
        */
        }

        public void ResetLevel()
        {
            foreach (var cell in _cells)
            {
                cell.ClearCharacter();
            }

            SpawnCharactersOnTheField();
        }

        public void ClearCharacter()
        {
            restartGame = true;
            /*_fieldConstructorDatabase.Characters.Clear();*/
        }

        private void Update()
        {
            /*
            FillAndSaveCharacters();
        */
        }

        public Cell GetFreeCell()
        {
            foreach (var cell in _cells)
            {
                if (cell.IsBusy() == false)
                    return cell;
            }

            return null;
        }

        public void SpawnCharactersOnTheField()
        {
            var wCount = 5;
            var hCount = 3;
            /*_characters = _fieldConstructorDatabase.Characters;
            for (var h = 0; h < hCount + 1; h++)
            {
                for (var w = 0; w < wCount + 1; w++)
                {
                    foreach (var character in _characters)
                    {
                        if (character.Key.Item1 == w && character.Key.Item2 == h)
                        {
                            foreach (var cell in Cells)
                            {
                                if (Math.Abs(cell.Width - character.Key.Item1) < 1f &&
                                    Math.Abs(cell.Height - character.Key.Item2) < 1f)
                                {
                                    cell.Character = _spawner.SpawnCharacter(_characters[(w, h)],
                                        cell.transform.position, true);
                                }
                            }
                        }
                    }

                }
            }*/

            
            int CountSaveCharacters = PlayerPrefs.GetInt("CountSaveCharacters");
            
            for (var h = 0; h < hCount + 1; h++)
            {
                for (var w = 0; w < wCount + 1; w++)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        /*int SaveCellWidth = PlayerPrefs.GetInt($"SaveCellWidth{i}");
                        int SaveCellHeight = PlayerPrefs.GetInt($"SaveCellHeight{i}");
                        int CharacterType = PlayerPrefs.GetInt($"CharacterType{i}");*/
                        
                        
                        if (/*SaveCellWidth == w && SaveCellHeight == h*/
                            _cellWidth[i] == w && _cellHeight[i] == h)
                        {
                            for (int j = 0; j < Cells.Count; j++)
                            {
                                if (Math.Abs(Cells[j].Width - /*SaveCellWidth*/ _cellWidth[i]) < 1f &&
                                    Math.Abs(Cells[j].Height - /*SaveCellHeight*/ _cellHeight[i]) < 1f)
                                {
                                    Cells[j].Character = _spawner.SpawnCharacter((CharacterType)/*CharacterType*/ characterTypeFOrCell[i],
                                        Cells[j].transform.position, true);
                                }
                            }
                            /*foreach (var cell in Cells)
                            {
                                if (Math.Abs(cell.Width - SaveCellWidth) < 1f &&
                                    Math.Abs(cell.Height - SaveCellHeight) < 1f)
                                {
                                    cell.Character = _spawner.SpawnCharacter((CharacterType)CharacterType /*characterTypeFOrCell[i]#1#,
                                        cell.transform.position, true);
                                }
                            }*/
                        }
                    }
                }
            }
        }

        
        
        
        public void FillAndSaveCharacters()
        {
            if (restartGame) return;
            
            /*_characters.Clear();
            foreach (var cell in Cells)
            {
                if (cell.Character == null) continue;
                _fieldConstructorDatabase.Characters.Add(((int)cell.Width, (int)cell.Height),
                    cell.Character.CharacterType);
            }*/
            
            var countSaveCharacters = 0;
            for (int i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].Character == null)
                {
                    /*PlayerPrefs.SetInt($"SaveCellWidth{i}",0);
                    PlayerPrefs.SetInt($"SaveCellHeight{i}",0);
                    PlayerPrefs.SetInt($"CharacterType{i}",0);*/
                    
                    _cellWidth[i] = 0;
                    _cellHeight[i] = 0;
                    characterTypeFOrCell[i] = 0;
                    continue;
                }
                countSaveCharacters++;
                /*PlayerPrefs.SetInt($"SaveCellWidth{i}",(int)Cells[i].Width);
                PlayerPrefs.SetInt($"SaveCellHeight{i}",(int)Cells[i].Height);
                PlayerPrefs.SetInt($"CharacterType{i}",(int)Cells[i].Character.CharacterType);*/
                
                _cellWidth[i] = (int)Cells[i].Width;
                _cellHeight[i] = (int)Cells[i].Height;
                characterTypeFOrCell[i] = (int)Cells[i].Character.CharacterType;

            }
            PlayerPrefs.SetInt($"CountSaveCharacters",countSaveCharacters);

        }



        public void SetCharacter(int[] character)
        {
            characterTypeFOrCell = character;
        }
        public void SetCellWidth(int[] cellWidth)
        {
            _cellWidth = cellWidth;
        }
        public void SetCellHeight(int[] cellHeight)
        {
            _cellHeight = cellHeight;
        }
    }

}
