using UnityEngine;
using TMPro;

public class IntroRandomizer : MonoBehaviour
{
    [Header("Объекты на сцене")]
    public TMP_Text[] nicknameTexts;
    [Tooltip("Сюда перетащи объекты огня/партиклы (по одному на каждого игрока)")]
    public GameObject[] fireEffects;

    [Header("Настройки игрока")]
    public string[] possibleNicknames = { "Bilda", "Killa", "entity", "trixx3d", "Sniper", "Ghost", "Viper", "Phoenix" };

    [Header("Шансы и Рандом")]
    [Range(0, 50)] public int maxWinstreak = 20;
    [Range(0f, 1f)] public float premiumChance = 0.3f;

    [Header("Оформление текста")]
    public Color levelColor = Color.yellow;
    public Color winstreakColor = Color.red;
    public string premiumTag = "<color=#FFD700>[P]</color>";

    void Start()
    {
        RandomizeAll();
    }

    private void RandomizeAll()
    {
        if (nicknameTexts == null) return;

        // Перемешиваем ники
        string[] shuffledNames = (string[])possibleNicknames.Clone();
        ShuffleArray(shuffledNames);

        for (int i = 0; i < nicknameTexts.Length; i++)
        {
            if (nicknameTexts[i] == null) continue;

            // 1. Генерируем данные
            int level = Random.Range(1, 100);
            int winstreak = Random.Range(0, maxWinstreak + 1);
            bool isPremium = Random.value < premiumChance;
            string name = (i < shuffledNames.Length) ? shuffledNames[i] : "Player";

            // 2. Формируем строку текста
            string lvlHex = ColorUtility.ToHtmlStringRGB(levelColor);
            string wsHex = ColorUtility.ToHtmlStringRGB(winstreakColor);

            // Собираем: [Ур] Ник [Прем] (Винстрик)
            string text = $"<color=#{lvlHex}>[{level}]</color> {name}";
            if (isPremium) text += $" {premiumTag}";

            if (winstreak > 0)
            {
                text += $" <color=#{wsHex}>{winstreak}</color>";
            }

            nicknameTexts[i].text = text;

            // 3. Логика огня (партиклов)
            if (i < fireEffects.Length && fireEffects[i] != null)
            {
                // Включаем огонь, только если винстрик больше 10
                fireEffects[i].SetActive(winstreak > 10);
            }
        }
    }

    private void ShuffleArray(string[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}