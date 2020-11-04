using UnityEngine;
using Zenject;

/// <summary>
/// Управление отсчётом времени в приложении
/// </summary>
public class TimeController : IInitializable
{
    private float PLAY_TIME_SCALE = 1f;
    private float PAUSE_TIME_SCALE = 0f;
    private SignalBus signalBus;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_signalBus">Шина сигналов (событий)</param>
    public TimeController(SignalBus _signalBus)
    {
        signalBus = _signalBus;
        signalBus.Subscribe<GameController.StartGameSignal>(Play);
        signalBus.Subscribe<GameController.PauseSignal>(Pause);
        signalBus.Subscribe<PlayerController.PlayerDeathSignal>(Pause);
    }

    /// <summary>
    /// Реализация интерфейса IInitializable
    /// </summary>
    public void Initialize()
    {
        Pause();
    }

    /// <summary>
    /// Запустить игру
    /// </summary>
    public void Play()
    {
        Time.timeScale = PLAY_TIME_SCALE;
    }

    /// <summary>
    /// Поставить игру на паузу
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }
}
