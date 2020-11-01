using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Контроллер спавна объектов - 
/// </summary>
public class WallSpawner : ITickable
{
    private const float DEFAULT_OFFSET_X = 0f;

    [Inject]
    private AbstractWall.Factory wallObjectsFactory;

    [Inject]
    private WallObject.Settings wallSettings;

    [Inject]
    private Settings spawnerSettings;

    private AbstractWall lastSpawnedWall;

    /// <summary>
    /// Реализация интерфейса ITickable
    /// </summary>
    public void Tick()
    {
        if (ShouldSpawnNewWall())
        {
            SpawnWall();
        }
    }

    private bool ShouldSpawnNewWall()
    {
        if (lastSpawnedWall == null)
        {
            return true;
        }

        return Mathf.Abs(lastSpawnedWall.LocalPosition.x) > spawnerSettings.MaxDistance;
    }

    private void SpawnWall()
    {
        float maxOffsetY = Mathf.Abs(spawnerSettings.MaxOffsetY);
        Vector2 offset = new Vector2(DEFAULT_OFFSET_X, UnityEngine.Random.Range(-maxOffsetY, maxOffsetY));

        lastSpawnedWall = wallObjectsFactory.Create(offset);
    }

    /// <summary>
    /// Настройки спавнера стен
    /// </summary>
    [Serializable]
    public class Settings
    {
        [Header("Максимальное смещение объекта по оси ординат")]
        public float MaxOffsetY;

        [Header("Расстояние между спавном объектов")]
        public float MaxDistance;
    }
}
