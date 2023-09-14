using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
    public class NewResultLeaderboard : MonoBehaviour
    {
        [SerializeField] LeaderboardYG leaderboardYG;
        [SerializeField] InputField nameLbInputField;
        [SerializeField] InputField scoreLbInputField;

        // Код для примера! Смена технического названия таблицы и её обновление в компоненте LeaderboardYG
        public void NewName()
        {
            /*leaderboardYG.nameLB = nameLbInputField.text;*/
            leaderboardYG.UpdateLB();
        }

        public void NewScore()
        {
            var a = PlayerPrefs.GetInt("CurrentLevel");
            // Статический метод добавление нового рекорда
            YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, PlayerPrefs.GetInt("CurrentLevel")+1);

            // Метод добавление нового рекорда обращением к компоненту LeaderboardYG
            /*leaderboardYG.NewScore(a);*/
        }

        public void NewScoreTimeConvert()
        {
            // Статический метод добавление нового рекорда конвертированного в time тип
            /*
            YandexGame.NewLBScoreTimeConvert(leaderboardYG.nameLB, float.Parse(scoreLbInputField.text));
            */

            // Метод добавление нового рекорда обращением к компоненту LeaderboardYG
            // leaderboardYG.NewScoreTimeConvert(float.Parse(scoreLbInputField.text));
        }
    }
}