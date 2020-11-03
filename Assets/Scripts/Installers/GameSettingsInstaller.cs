using UnityEngine;
using Walls;
using Zenject;

/// <summary>
/// ScriptableObject настроек игры
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects / Game Settings Installer")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    /// <summary>
    /// Поле - настройки объекта "Стандартная стена"
    /// </summary>
    public GreenWallObject.Settings GreenWallObjectSettings;

    /// <summary>
    /// Поле - настройки объекта "Красная стена"
    /// </summary>
    public RedWallObject.Settings RedWallObjectSettings;

    /// <summary>
    /// Поле - настройки спавнера стен
    /// </summary>
    public WallSpawner.Settings WallSpawnerSettings;

    /// <summary>
    /// Настройки игрока - птица
    /// </summary>
    public BirdPlayer.Settings BirdPlayerSettings;

    /// <summary>
    /// Настройки ввода
    /// </summary>
    public InputControl.Settings InputSettings;

    /// <summary>
    /// Внедряем зависимости
    /// </summary>
    public override void InstallBindings()
    {
        Container.BindInstance(GreenWallObjectSettings);
        Container.BindInstance(RedWallObjectSettings);

        Container.BindInstance(WallSpawnerSettings);
        Container.BindInstance(BirdPlayerSettings);
        Container.BindInstance(InputSettings);
    }
}
