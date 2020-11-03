using System;
using UnityEngine;
using Zenject;

namespace Walls
{
    /// <summary>
    /// Контроллер спавна стен
    /// </summary>
    public class WallSpawner : ITickable
    {
        /// <summary>
        /// Перечисляемый тип - цвет стен
        /// </summary>
        public enum WallColor
        {
            Red,
            Green
        }

        private const float DEFAULT_OFFSET_X = 0f;

        [Inject]
        private GreenWallObject.Factory greenWallObjectsFactory;

        [Inject]
        private RedWallObject.Factory redWallObjectsFactory;

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

            switch (spawnerSettings.wallColor)
            {
                case WallColor.Green:
                    lastSpawnedWall = greenWallObjectsFactory.Create(offset);
                    break;
                case WallColor.Red:
                    lastSpawnedWall = redWallObjectsFactory.Create(offset);
                    break;
            }
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

            [Header("Цвет стен")]
            public WallColor wallColor;
        }
    }
}
