﻿using Zenject;

/// <summary>
/// Кнопка начала игры
/// </summary>
public class PlayButton : AbstractButton
{
    [Inject]
    private GameController gameController;

    protected override void OnButtonClicked()
    {
        gameController.StartGame();
    }
}
