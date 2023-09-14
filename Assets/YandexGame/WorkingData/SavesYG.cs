
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public float MoneyInTheStorage = 400;
        public int MoneyLetter = 0;
        public float PriceMelee = 200;
        public float PriceRange = 200;
        public int PriceMeleeLetter = 0;
        public int PriceRangeLette = 0;
        public int currentLevel = 0;
        public int currentOpenCharacter = 0;
        public int OldCharacterTypeMelee = 0;
        public int OldCharacterTypeRange = 0;
        public int CurrentMaxCharacterMelee = 0;
        public int CurrentMaxCharacterRange = 0;
        
        public int[] CharacterTypeFOrCell = new int[15];
        public int[] CellWidth = new int[15];
        public int[] CellHeight = new int[15];



        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
    }
}
