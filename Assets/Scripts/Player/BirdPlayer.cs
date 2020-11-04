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
    private PlayerController playerController;

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
        if (birdRigidbody)
        {
            birdRigidbody.simulated = true;
            birdRigidbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Совершить движение
    /// </summary>
    public override void Move()
    {
        if (birdRigidbody)
        {
            birdRigidbody.velocity = settings.movement;
        }
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
        if (birdRigidbody)
        {
            birdRigidbody.position = settings.defaultPosition;
        }
    }

    private void Update()
    {
        if (birdRigidbody)
        {
            birdRigidbody.rotation = birdRigidbody.velocity.y * 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == WALL_OBJECT_TAG)
        {
            playerController.DeathHandler();
        }
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
