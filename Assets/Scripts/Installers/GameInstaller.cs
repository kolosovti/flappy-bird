using UnityEngine;
using Walls;
using Zenject;

/// <summary>
/// Внедрение зависимостей в контекст
/// </summary>
public class GameInstaller : MonoInstaller
{
    private const string WALLS_TRANSFORM_GROUP = "Walls";
    private const int WALLS_INITIALIZE_POOL_SIZE = 10;

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
        Container.Bind<GameController>().AsSingle();
        Container.Bind<ScoreController>().AsSingle();
        Container.Bind<PlayerController>().AsSingle();
        Container.BindInterfacesAndSelfTo<WallSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputControl>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<SelectPlayerHandler>().AsSingle();

        Container.BindFactory<Vector2, AbstractWall, GreenWallObject.Factory>()
            .FromPoolableMemoryPool<Vector2, AbstractWall, WallObjectsPool>(poolBinder => poolBinder
            .WithInitialSize(10)
            .FromComponentInNewPrefab(wallObjectSettings.WallPrefab)
            .UnderTransformGroup(WALLS_TRANSFORM_GROUP));

        Container.BindFactory<Vector2, AbstractWall, RedWallObject.Factory>()
            .FromPoolableMemoryPool<Vector2, AbstractWall, WallObjectsPool>(poolBinder => poolBinder
            .WithInitialSize(WALLS_INITIALIZE_POOL_SIZE)
            .FromComponentInNewPrefab(redWallObjectSettings.WallPrefab)
            .UnderTransformGroup(WALLS_TRANSFORM_GROUP));

        Container.BindFactory<AbstractPlayer, BirdPlayer.Factory>()
            .FromComponentInNewPrefab(birdPlayerSettings.PlayerPrefab);

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<InputControl.TouchInputDetectSignal>();
        Container.DeclareSignal<InputControl.MouseInputDetectSignal>();

        Container.BindSignal<InputControl.MouseInputDetectSignal>().
            ToMethod<PlayerController>((x, s) => x.InputSignalHandler(s)).FromResolve();
        Container.BindSignal<InputControl.TouchInputDetectSignal>().
            ToMethod<PlayerController>((x, s) => x.InputSignalHandler(s)).FromResolve();

        Container.DeclareSignal<GameController.StartGameSignal>();
        Container.DeclareSignal<GameController.PauseSignal>();

        Container.DeclareSignal<ScoreController.ScoreUpdatedSignal>();
        Container.DeclareSignal<ScoreController.BestScoreUpdatedSignal>();

        Container.DeclareSignal<PlayerController.PlayerDeathSignal>();
    }

    private class WallObjectsPool : MonoPoolableMemoryPool<Vector2, IMemoryPool, AbstractWall> { }
}
