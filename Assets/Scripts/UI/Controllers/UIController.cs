using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Управление UI
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField] private AbstractRendererView mainMenuPanel;
    [SerializeField] private AbstractRendererView gamePanel;
    [SerializeField] private AbstractRendererView pausePanel;
    [SerializeField] private AbstractRendererView bestScorePanel;
    [SerializeField] private AbstractRendererView gameOverPanel;

    /// <summary>
    /// Панель главного меню
    /// </summary>
    public AbstractRendererView MainMenuPanel { get => mainMenuPanel; private set => mainMenuPanel = value; }

    /// <summary>
    /// Панель игрового меню
    /// </summary>
    public AbstractRendererView GamePanel { get => gamePanel; private set => gamePanel = value; }

    /// <summary>
    /// Панель паузы
    /// </summary>
    public AbstractRendererView PausePanel { get => pausePanel; private set => pausePanel = value; }

    /// <summary>
    /// Панель наибольшего набранного счёта
    /// </summary>
    public AbstractRendererView BestScorePanel { get => bestScorePanel; private set => bestScorePanel = value; }

    /// <summary>
    /// Панель завершения игры \ проигрыша
    /// </summary>
    public AbstractRendererView GameOverPanel { get => gameOverPanel; private set => gameOverPanel = value; }

    private List<AbstractRendererView> panels = new List<AbstractRendererView>();
    private GameController.StartGameSignal startGameSignal;

    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus _signalBus)
    {
        signalBus = _signalBus;
        signalBus.Subscribe<GameController.StartGameSignal>(OnGameStarted);
        signalBus.Subscribe<GameController.PauseSignal>(OnGamePaused);
        signalBus.Subscribe<PlayerController.PlayerDeathSignal>(OnPlayerDied);
    }

    private void Start()
    {
        panels.Add(MainMenuPanel);
        panels.Add(GamePanel);
        panels.Add(PausePanel);
        panels.Add(BestScorePanel);
        panels.Add(GameOverPanel);

        ResetToDefault();
    }

    private void OnDestroy()
    {
        if (signalBus != null)
        {
            signalBus.Unsubscribe<GameController.StartGameSignal>(OnGameStarted);
            signalBus.Unsubscribe<GameController.PauseSignal>(OnGamePaused);
            signalBus.Unsubscribe<PlayerController.PlayerDeathSignal>(OnPlayerDied);
        }
    }

    private void OnGameStarted()
    {
        OpenPanel(GamePanel);
    }

    private void OnGamePaused()
    {
        OpenCooperativePanel(PausePanel);
    }

    private void OnPlayerDied()
    {
        OpenCooperativePanel(GameOverPanel);
    }

    /// <summary>
    /// Восстановить интерфейс в дефолтное состояние.
    /// </summary>
    private void ResetToDefault()
    {
        OpenPanel(MainMenuPanel);
    }

    /// <summary>
    /// Открыть только одну панель.
    /// </summary>
    /// <param name="panel"></param>
    public void OpenPanel(AbstractRendererView panel)
    {
        foreach (AbstractRendererView view in panels)
        {
            view.CloseView();
        }

        panel.ShowView();
    }

    /// <summary>
    /// Открыть панель поверх других панелей 
    /// </summary>
    /// <param name="panel"></param>
    public void OpenCooperativePanel(AbstractRendererView panel)
    {
        panel.ShowView();
    }

    /// <summary>
    /// Закрыть панель
    /// </summary>
    /// <param name="panel"></param>
    public void ClosePanel(AbstractRendererView panel)
    {
        panel.CloseView();

        bool isSomeonePanelRenderer = false;
        foreach (AbstractRendererView view in panels)
        {
            if (view.IsRendererActive)
            {
                isSomeonePanelRenderer = true;
            }
        }

        if (!isSomeonePanelRenderer)
        {
            ResetToDefault();
        }
    }
}
