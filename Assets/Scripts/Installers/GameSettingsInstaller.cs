using UnityEngine;
using Zenject;

/// <summary>
/// ScriptableObject настроек игры
/// </summary>
[CreateAssetMenu(menuName = "Flappy Bird / Game Settings Installer")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    /// <summary>
    /// Поле - настройки объекта "Стандартная стена"
    /// </summary>
    public WallObject.Settings WallObjectSettings;

    /// <summary>
    /// Поле - настройки спавнера стен
    /// </summary>
    public WallSpawner.Settings wallSpawnerSettings;

    /// <summary>
    /// Настройки игрока - птица
    /// </summary>
    public BirdPlayer.Settings birdPlayerSettings;

    /// <summary>
    /// Настройки ввода
    /// </summary>
    public InputControl.Settings inputSettings;

    /// <summary>
    /// Внедряем зависимости
    /// </summary>
    public override void InstallBindings()
    {
        Container.BindInstance(wallSpawnerSettings);
        Container.BindInstance(WallObjectSettings);
        Container.BindInstance(birdPlayerSettings);
        Container.BindInstance(inputSettings);
    }
}
