using UnityEngine;
using Zenject;

/// <summary>
/// Базовый класс для UI-объекта, состоянием которого нужно управлять с помощью CanvasGroup.
/// Позволяет реализовать простую анимацию объекта с появлением через прозрачность
/// </summary>
[RequireComponent(typeof(Canvas))]
public class AbstractRendererView : MonoBehaviour
{
    [Inject]
    private UIController uiController;

    private Canvas canvas;
    private bool isRendererActive;

    /// <summary>
    /// Включено ли отображение объекта?
    /// </summary>
    public bool IsRendererActive { get => isRendererActive; private set => isRendererActive = value; }

    protected virtual void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    /// <summary>
    /// Показать объект
    /// </summary>
    public virtual void ShowView()
    {
        SetRenderer(true);
    }

    /// <summary>
    /// Скрыть объект
    /// </summary>
    public virtual void CloseView()
    {
        SetRenderer(false);
    }

    private void SetRenderer(bool isActive)
    {
        canvas.enabled = isActive;
        isRendererActive = isActive;
    }
}
