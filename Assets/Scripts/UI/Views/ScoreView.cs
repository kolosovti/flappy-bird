using UnityEngine;
using Zenject;

/// <summary>
/// Вьюшка для отображения счёта игрока
/// </summary>
public class ScoreView : AbstractText
{
    /// <summary>
    /// Перечисляемый тип - вид счётчика (игровой или лучший счёт)
    /// </summary>
    public enum ScoreType
    {
        CurrentScore,
        BestScore
    }

    [SerializeField] private ScoreType scoreType;

    private SignalBus signalBus;

    [Inject]
    private ScoreController scoreController;

    [Inject]
    public void Construct(SignalBus _signalBus)
    {
        signalBus = _signalBus;
        signalBus.Subscribe<ScoreController.ScoreUpdatedSignal>(UpdateScore);
        signalBus.Subscribe<ScoreController.BestScoreUpdatedSignal>(UpdateScore);
    }

    private void Start()
    {
        UpdateScore();
    }

    /// <summary>
    /// Обновить счёт
    /// </summary>
    public void UpdateScore()
    {
        if (scoreType == ScoreType.CurrentScore)
        {
            text.text = scoreController.CurrentScore.ToString();
        }
        else
        {
            text.text = scoreController.BestScore.ToString();
        }
    }
    private void OnDestroy()
    {
        if (signalBus != null)
        {
            signalBus.Unsubscribe<ScoreController.ScoreUpdatedSignal>(UpdateScore);
            signalBus.Unsubscribe<ScoreController.BestScoreUpdatedSignal>(UpdateScore);
        }
    }
}
