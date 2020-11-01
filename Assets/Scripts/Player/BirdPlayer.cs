using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс игрока - птицы
/// </summary>
public class BirdPlayer : AbstractPlayer
{
    [Inject]
    private Settings settings;

    /// <summary>
    /// Начать игровую активность
    /// </summary>
    public override void StartGameMovement()
    {
        settings.rigidbody.simulated = true;
    }

    /// <summary>
    /// Совершить движение
    /// </summary>
    public override void Move()
    {
        settings.rigidbody.velocity = settings.movement;
    }

    /// <summary>
    /// Умереть
    /// </summary>
    public override void Death()
    {
        settings.rigidbody.simulated = false;
    }

    /// <summary>
    /// Восстановить игрока в дефолтное значение
    /// </summary>
    public override void ResetToDefault()
    {
        settings.rigidbody.position = settings.defaultPosition;
    }

    private void Update()
    {
        settings.rigidbody.rotation = Vector2.Angle(Vector2.zero, settings.rigidbody.velocity);
    }

    /// <summary>
    /// Настройки игрока
    /// </summary>
    [Serializable]
    public class Settings
    {
        [Header("Префаб игрока")]
        public GameObject PlayerPrefab;

        [Header("Направление движения при совершении активности")]
        public Vector2 movement = Vector2.up;

        [Header("Компонент Rigidbody")]
        public Rigidbody2D rigidbody;

        [Header("Дефолтная позиция игрока")]
        public Vector2 defaultPosition = Vector2.zero;
    }
}
