using Zenject;

/// <summary>
/// Кнопка паузы
/// </summary>
public class PauseButton : AbstractButton
{
    [Inject]
    private GameController gameController;

    protected override void OnButtonClicked()
    {
        gameController.PauseGame();
    }
}
