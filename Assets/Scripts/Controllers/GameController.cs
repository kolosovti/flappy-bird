using UnityEngine.SceneManagement;
using Zenject;

public class GameController
{
    private SignalBus signalBus;

    [Inject]
    private TimeController timeController;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_signalBus">Шина сигналов (событий)</param>
    public GameController(SignalBus _signalBus)
    {
        signalBus = _signalBus;
    }

    /// <summary>
    /// Запустить игру
    /// </summary>
    public void StartGame()
    {
        signalBus.Fire(new StartGameSignal() { });
    }

    /// <summary>
    /// Поставить игру на паузу
    /// </summary>
    public void PauseGame()
    {
        signalBus.Fire(new PauseSignal() { });
    }

    /// <summary>
    /// Восстановить в дефолтное состояние
    /// </summary>
    public void ResetToDefault()
    {
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
