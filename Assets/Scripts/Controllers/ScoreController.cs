using UnityEngine;

/// <summary>
/// Контроллер подсчёта набранных очков
/// </summary>
public class ScoreController : MonoBehaviour
{
    private const string BEST_SCORE_PREFS_KEY = "BestScore_";

    private int currentScore;

    public void CountScore()
    {
        if (++currentScore > BestScore)
        {
            BestScore = currentScore;
        }
    }

    /// <summary>
    /// Свойство - лучший набранный результат
    /// </summary>
    public int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(BEST_SCORE_PREFS_KEY, 0);
        }
        private set
        {
            PlayerPrefs.SetInt(BEST_SCORE_PREFS_KEY, value);
        }
    }
}
