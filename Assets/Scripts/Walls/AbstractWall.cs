using System.Collections;
using UnityEngine;
using Zenject;

namespace Walls
{
    /// <summary>
    /// Абстрактный класс - Стена
    /// </summary>
    public abstract class AbstractWall : MonoBehaviour, IPoolable<Vector2, IMemoryPool>
    {
        private IMemoryPool pool;
        private Coroutine delayCoroutine;

        /// <summary>
        /// Позиция стены в глобальных координатах
        /// </summary>
        public abstract Vector2 WorldPosition { get; }

        /// <summary>
        /// Позиция стены в локальных координатах
        /// </summary>
        public abstract Vector2 LocalPosition { get; }

        /// <summary>
        /// Сместить позицию объекта на <paramref name="offset"/>
        /// </summary>
        /// <param name="offset"></param>
        public abstract void AddPosition(Vector2 offset);

        /// <summary>
        /// Задать позицию объекта, равную <paramref name="offset"/>
        /// </summary>
        /// <param name="offset"></param>
        public abstract void MovePosition(Vector2 offset);

        /// <summary>
        /// Задать дефолтную позицию стены
        /// </summary>
        public abstract void ResetToDefaultPosition();

        /// <summary>
        /// Реализация интерфейса IPoolable - вызывается при спавне или создании объекта.
        /// </summary>
        public virtual void OnSpawned(Vector2 offset, IMemoryPool memoryPool)
        {
            pool = memoryPool;

            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
            }
            delayCoroutine = StartCoroutine(Delay(offset));
        }

        private IEnumerator Delay(Vector2 position)
        {
            yield return new WaitForSeconds(0.1f);
            ResetToDefaultPosition();
            AddPosition(position);
        }

        /// <summary>
        /// Реализация интерфейса IPoolable - вызывается при деспавне объекта
        /// </summary>
        public virtual void OnDespawned()
        {
            pool = null;
        }

        /// <summary>
        /// Виртуальный метод - вызвать деспавн объекта. 
        /// При необходимости произвести какие-то манипуляции при деспавне, можно переопределить метод, не забыв вызвать base.OnDespawn()
        /// </summary>
        public virtual void Despawn()
        {
            pool.Despawn(this);
        }
    }
}
