using Zenject;

/// <summary>
/// Класс, обрабатывающий выбор игрока
/// Заглушка с инициализацией, из которой можно реализовать выбор игрока
/// </summary>
public class SelectPlayerHandler : IInitializable
{
    [Inject]
    private BirdPlayer.Factory playerFactory;

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
