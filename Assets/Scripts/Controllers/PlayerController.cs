using Zenject;

/// <summary>
/// Управление персонажем
/// </summary>
public class PlayerController
{
    private const string PLAYER_MOVE_HANDLE_OBJECT_TAG = "PlayerJumpControlObject";

    private SignalBus signalBus;

    [Inject]
    private TimeController timeController;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_signalBus">Шина сигналов (событий)</param>
    public PlayerController(SignalBus _signalBus)
    {
        signalBus = _signalBus;
        signalBus.Subscribe<GameController.StartGameSignal>(StartGame);
    }

    /// <summary>
    /// Абстрактный игрок
    /// </summary>
    public AbstractPlayer player;

    /// <summary>
    /// Начать игру
    /// </summary>
    public void StartGame()
    {
        player.StartGameMovement();
    }

    /// <summary>
    /// Обработчик сигнала нажатия на экран
    /// </summary>
    /// <param name="inputSignal"></param>
    public void InputSignalHandler(InputControl.TouchInputDetectSignal inputSignal)
    {
        if (player)
        {
            player.Move();
        }
    }

    /// <summary>
    /// Обработчик сигнала клика мыши
    /// </summary>
    /// <param name="inputSignal"></param>
    public void InputSignalHandler(InputControl.MouseInputDetectSignal inputSignal)
    {
        if (inputSignal.PressedMouseButtonKey == InputControl.MOUSE_LEFT_BUTTON_KEY)
        {
            if (player)
            {
                player.Move();
            }
        }
    }

    /// <summary>
    /// Восстановить игрока в дефолтное значение
    /// </summary>
    public void ResetGameSignalHandler()
    {
        player.ResetToDefault();
    }

    /// <summary>
    /// Обработчик события о смерти игрока
    /// </summary>
    public void DeathHandler()
    {
        player.Death();
        signalBus.Fire(new PlayerDeathSignal { });
    }

    /// <summary>
    /// Класс для отправки события о смерти персонажа
    /// </summary>
    public class PlayerDeathSignal { }
}
