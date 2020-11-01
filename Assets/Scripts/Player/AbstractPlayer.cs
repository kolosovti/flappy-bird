using UnityEngine;
using Zenject;

/// <summary>
/// Абстрактный класс 
/// </summary>
public abstract class AbstractPlayer : MonoBehaviour
{
    /// <summary>
    /// Начать игровую активность
    /// </summary>
    public abstract void StartGameMovement();

    /// <summary>
    /// Совершить движение
    /// </summary>
    public abstract void Move();

    /// <summary>
    /// Умереть
    /// </summary>
    public abstract void Death();

    /// <summary>
    /// Восстановить игрока в дефолтное значение
    /// </summary>
    public abstract void ResetToDefault();

    /// <summary>
    /// Фабрика создания игроков
    /// </summary>
    public class Factory : PlaceholderFactory<AbstractPlayer> { }
}
