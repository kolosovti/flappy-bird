using UnityEngine;

/// <summary>
/// Управление персонажем
/// </summary>
public class PlayerController
{
    private const string PLAYER_MOVE_HANDLE_OBJECT_TAG = "PlayerJumpControlObject";

    /// <summary>
    /// Абстрактный игрок
    /// </summary>
    public AbstractPlayer player;

    /// <summary>
    /// Обработчик сигнала нажатия на экран
    /// </summary>
    /// <param name="inputSignal"></param>
    public void InputSignalHandler(InputControl.TouchInputDetectSignal inputSignal)
    {
        if (inputSignal.objectTag == PLAYER_MOVE_HANDLE_OBJECT_TAG)
        {
            player.Move();
        }
    }

    public void InputSignalHandler(InputControl.MouseInputDetectSignal inputSignal)
    {
        Debug.Log("MOUSE CLICK SIGNAL");
        if (inputSignal.PressedMouseButtonKey == InputControl.MOUSE_LEFT_BUTTON_KEY)
        {
            player.Move();
        }
    }

    private void Stash()
    {
        //TODO: реализовать удаление игрока
    }
}
