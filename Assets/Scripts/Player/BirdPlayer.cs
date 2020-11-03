using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс игрока - птицы
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BirdPlayer : AbstractPlayer
{
    [Inject]
    private Settings settings;

    private Rigidbody2D birdRigidbody;

    private void Start()
    {
        birdRigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Начать игровую активность
    /// </summary>
    public override void StartGameMovement()
    {
        birdRigidbody.simulated = true;
    }

    /// <summary>
    /// Совершить движение
    /// </summary>
    public override void Move()
    {
        birdRigidbody.velocity = settings.movement;
    }

    /// <summary>
    /// Умереть
    /// </summary>
    public override void Death()
    {
        birdRigidbody.simulated = false;
    }

    /// <summary>
    /// Восстановить игрока в дефолтное значение
    /// </summary>
    public override void ResetToDefault()
    {
        birdRigidbody.position = settings.defaultPosition;
    }

    private void Update()
    {
        birdRigidbody.rotation = Vector2.Angle(Vector2.zero, birdRigidbody.velocity);
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

        [Header("Дефолтная позиция игрока")]
        public Vector2 defaultPosition = Vector2.zero;
    }

    /// <summary>
    /// Фабрика создания игроков
    /// </summary>
    public class Factory : PlaceholderFactory<AbstractPlayer> { }
}
