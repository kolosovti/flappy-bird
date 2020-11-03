using UnityEngine;
using Walls;
using Zenject;

/// <summary>
/// Внедрение зависимостей в контекст
/// </summary>
public class GameInstaller : MonoInstaller
{
    private const string WALLS_TRANSFORM_GROUP = "Walls";

    [Inject]
    private GreenWallObject.Settings wallObjectSettings;

    [Inject]
    private RedWallObject.Settings redWallObjectSettings;

    [Inject]
    private BirdPlayer.Settings birdPlayerSettings;

    /// <summary>
    /// Внедряем зависимости в контекст
    /// </summary>
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.Bind<InputControl>().AsSingle();
        Container.Bind<PlayerController>().AsSingle();
        Container.BindInterfacesAndSelfTo<SelectPlayerHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<WallSpawner>().AsSingle();

        Container.BindFactory<Vector2, AbstractWall, GreenWallObject.Factory>()
            .FromPoolableMemoryPool<Vector2, AbstractWall, WallObjectsPool>(poolBinder => poolBinder
            .WithInitialSize(10)
            .FromComponentInNewPrefab(wallObjectSettings.WallPrefab)
            .UnderTransformGroup(WALLS_TRANSFORM_GROUP));

        Container.BindFactory<Vector2, AbstractWall, RedWallObject.Factory>()
            .FromPoolableMemoryPool<Vector2, AbstractWall, WallObjectsPool>(poolBinder => poolBinder
            .WithInitialSize(10)
            .FromComponentInNewPrefab(redWallObjectSettings.WallPrefab)
            .UnderTransformGroup(WALLS_TRANSFORM_GROUP));

        Container.BindFactory<AbstractPlayer, BirdPlayer.Factory>()
            .FromComponentInNewPrefab(birdPlayerSettings.PlayerPrefab);
    }

    private class WallObjectsPool : MonoPoolableMemoryPool<Vector2, IMemoryPool, AbstractWall> { }
}
