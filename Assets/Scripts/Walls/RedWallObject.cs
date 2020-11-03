using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Walls
{
    public class RedWallObject : AbstractWall
    {
        private const float DEFAULT_POSITION_X = 0f;
        private const float DEFAULT_POSITION_Y = 0f;

        [SerializeField] private Rigidbody2D topWallRigidbody;
        [SerializeField] private Rigidbody2D bottomWallRigidbody;

        [Inject]
        private Settings settings;

        private Coroutine delayCoroutine;

        /// <summary>
        /// Позиция стены в локальных координатах
        /// </summary>
        public override Vector2 LocalPosition
        {
            get
            {
                return topWallRigidbody.position - (Vector2)gameObject.transform.position;
            }
        }

        /// <summary>
        /// Позиция стены в мировых координатах
        /// </summary>
        public override Vector2 WorldPosition
        {
            get
            {
                return topWallRigidbody.position;
            }
        }

        /// <summary>
        /// Задать смещение позиции стены
        /// </summary>
        public override void AddPosition(Vector2 offset)
        {
            topWallRigidbody.MovePosition(topWallRigidbody.position + offset);
            bottomWallRigidbody.MovePosition(bottomWallRigidbody.position + offset);
        }

        /// <summary>
        /// Задать позицию трубы
        /// </summary>
        public override void MovePosition(Vector2 position)
        {
            topWallRigidbody.MovePosition(position);
            bottomWallRigidbody.MovePosition(position);
        }

        /// <summary>
        /// Задать дефолтную позицию стены
        /// </summary>
        public override void ResetToDefaultPosition()
        {
            Vector2 defaultPosition = new Vector2(DEFAULT_POSITION_X, Mathf.Abs(settings.DistanceBetweenWalls) + DEFAULT_POSITION_Y);
            defaultPosition += (Vector2)gameObject.transform.position;
            topWallRigidbody.MovePosition(defaultPosition);
            bottomWallRigidbody.MovePosition(new Vector2(defaultPosition.x, -defaultPosition.y));
        }

        private void Update()
        {
            AddPosition(settings.MoveSpeed);

            if (WorldPosition.x < settings.DespawnPositionX)
            {
                if (delayCoroutine != null)
                {
                    StopCoroutine(delayCoroutine);
                }

                delayCoroutine = StartCoroutine(Delay());
            }
        }

        private IEnumerator Delay()
        {
            ResetToDefaultPosition();
            yield return new WaitForSecondsRealtime(0.1f);
            Despawn();
        }

        /// <summary>
        /// Фабрика создания стен
        /// </summary>
        public class Factory : PlaceholderFactory<Vector2, AbstractWall> { }

        /// <summary>
        /// Настройки объекта "Стена"
        /// </summary>
        [Serializable]
        public class Settings
        {
            [Header("Префаб игрового объекта 'Стена'")]
            public GameObject WallPrefab;

            [Header("Направление перемещения стены на один тик")]
            public float DistanceBetweenWalls;

            [Header("Направление перемещения стены на один тик")]
            public Vector2 MoveSpeed;

            [Header("Позиция уничтожения объекта по оси абсцисс")]
            public float DespawnPositionX;
        }
    }
}
