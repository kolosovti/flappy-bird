using UnityEngine;
using Zenject;

/// <summary>
/// Контроллер подсчёта набранных очков
/// </summary>
public class ScoreController
{
    private const string BEST_SCORE_PREFS_KEY = "BestScore_";

    private int currentScore;
    private SignalBus signalBus;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_signalBus">Шина сигналов (событий)</param>
    public ScoreController(SignalBus _signalBus)
    {
        signalBus = _signalBus;
    }

    /// <summary>
    /// Добавить счёт
    /// </summary>
    public void CountScore()
    {
        if (++currentScore > BestScore)
        {
            BestScore = currentScore;
        }

        signalBus.Fire(new ScoreUpdatedSignal() { });
    }

    /// <summary>
    /// Свойство - текущий результат
    /// </summary>
    public int CurrentScore
    {
        get
        {
            return currentScore;
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
            signalBus.Fire(new BestScoreUpdatedSignal() { });
        }
    }

    /// <summary>
    /// Класс для отправки сигнала об увеличении счёта
    /// </summary>
    public class ScoreUpdatedSignal { }

    /// <summary>
    /// Класс для отправки сигнала об увеличении лучшего счёта
    /// </summary>
    public class BestScoreUpdatedSignal { }
}
