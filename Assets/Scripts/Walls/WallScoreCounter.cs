﻿using UnityEngine;
using Zenject;

/// <summary>
/// Объект, столкновение игрока с которым должно увеличить игровой счёт.
/// </summary>
public class WallScoreCounter : MonoBehaviour
{
    private const string PLAYER = "Player";

    [Inject]
    private ScoreController score;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER)
        {
            score.CountScore();
        }
    }
}
