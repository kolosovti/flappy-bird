using UnityEngine.SceneManagement;
using Zenject;

public class GameController
{
    private SignalBus signalBus;

    [Inject]
    private TimeController timeController;

    private bool isGamePlaying;

    /// <summary>
    /// Свойство - активна ли сейчас игровая сессия
    /// </summary>
    public bool IsGamePlaying { get => isGamePlaying; private set => isGamePlaying = value; }

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_signalBus">Шина сигналов (событий)</param>
    public GameController(SignalBus _signalBus)
    {
        signalBus = _signalBus;
        signalBus.Subscribe<PlayerController.PlayerDeathSignal>(PauseGame);
    }

    /// <summary>
    /// Запустить игру
    /// </summary>
    public void StartGame()
    {
        IsGamePlaying = true;
        signalBus.Fire(new StartGameSignal() { });
    }

    /// <summary>
    /// Поставить игру на паузу
    /// </summary>
    public void PauseGame()
    {
        IsGamePlaying = false;
        signalBus.Fire(new PauseSignal() { });
    }

    /// <summary>
    /// Восстановить в дефолтное состояние
    /// </summary>
    public void ResetToDefault()
    {
        IsGamePlaying = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Класс для отправки сигнала о начале игры
    /// </summary>
    public class StartGameSignal { }

    /// <summary>
    /// Класс для отправки сигнала о паузе игры
    /// </summary>
    public class PauseSignal { }
}
