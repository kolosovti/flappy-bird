using Zenject;

/// <summary>
/// Класс, обрабатывающий выбор игрока
/// </summary>
public class SelectPlayerHandler : IInitializable
{
    [Inject]
    private AbstractPlayer.Factory playerFactory;

    [Inject]
    private PlayerController playerController;

    /// <summary>
    /// Реализация интерфейса IInitializable
    /// </summary>
    public void Initialize()
    {
        playerController.player = playerFactory.Create();
    }
}
