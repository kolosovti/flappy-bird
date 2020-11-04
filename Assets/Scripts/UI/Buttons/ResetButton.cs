using Zenject;

public class ResetButton : AbstractButton
{
    [Inject]
    private GameController gameController;

    protected override void OnButtonClicked()
    {
        gameController.ResetToDefault();
    }
}
