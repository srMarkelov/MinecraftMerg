using System.Collections.Generic;
using Core.Characters;
using Core.Levels;
using UnityEngine;

namespace Core.Field
{
    public class FieldPreset : MonoBehaviour
    {
        [SerializeField] private LevelsDatabase _levelsDatabase;
        [SerializeField] private Cell _cell;
        [SerializeField] private Transform _leftUpPoint;
        [SerializeField] private CharacterSpawner _spawner;
        [SerializeField] private GuideGame.GuideGame _guideGame;
        [SerializeField] private List<Cell> _cells;

        public List<Cell> Cells => _cells;
        private readonly List<Cell> _field = new List<Cell>();
        private LevelData _levelData;
        private Dictionary<(int, int), CharacterType> _characters = new Dictionary<(int, int), CharacterType>();
        /*public List<Cell> Field => _field;*/
        private int _currentLevel;

        public void ResetLevel()
        {
            foreach (var cell in Cells)
            {
                cell.Character = null;
            }
            
            FillCharacters();
        }

        public void StartGame()
        {
            /*_currentLevel = PlayerPrefs.GetInt("CurrentLevel");*/

            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            _levelData = _levelsDatabase.LevelDatas[_currentLevel];
            if (_currentLevel == 0)
            {
                _guideGame.StartGuide();
            }
            FillCharacters();
        }

        public void SetCurrentLevel(int currentLevel)
        {
            _currentLevel = currentLevel;
            _levelData = _levelsDatabase.LevelDatas[_currentLevel];
        }

        /*private void GenerateField()
        {
            _field?.Clear();
            var wCount = _levelData.FieldWidth;
            var hCount = _levelData.FieldHeight;

            for (var h = 0; h < hCount; h++)
            {
                for (var w = 0; w < wCount; w++)
                {
                    var position = new Vector3(_leftUpPoint.position.x + w * _cell.Width,
                        _leftUpPoint.position.y - h * _cell.Height, 1);
                    var newCell = Instantiate(_cell.gameObject, position,
                        Quaternion.identity, transform);
                    newCell.gameObject.SetActive(true);
                    _field.Add(newCell.GetComponent<Cell>());
                    if (_characters.ContainsKey((w, h)))
                    {
                        newCell.GetComponent<Cell>().Character = _spawner.SpawnCharacter(_characters[(w, h)],
                            position, true);
                    }
                }
            }
            _cell.gameObject.SetActive(false);
        }*/

        private void SpawnCharactersOnTheField()
        {
            var wCount = _levelData.FieldWidth;
            var hCount = _levelData.FieldHeight;

            for (var h = 0; h < hCount+1; h++)
            {
                for (var w = 0; w < wCount+1; w++)
                {
                    foreach (var character in _levelsDatabase.LevelDatas[_currentLevel].Characters)
                    {
                        if (character.PositionX == w && character.PositionY == h)
                        {
                            foreach (var cell in Cells)
                            {
                                if (cell.Width == character.PositionX && cell.Height == character.PositionY)
                                {
                                    cell.Character = _spawner.SpawnCharacter(_characters[(w, h)],
                                        cell.transform.position, true);
                                }
                            }
                        }
                    }
                    
                }
            }
        }
        
        private void FillCharacters()
        {
            _characters.Clear();
            foreach (var character in _levelData.Characters)
            {
                _characters.Add((character.PositionX, character.PositionY), character.Type);
            }

            SpawnCharactersOnTheField();
        }
    }
}
