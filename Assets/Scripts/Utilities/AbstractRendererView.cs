using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Базовый класс для UI-объекта, состоянием которого нужно управлять с помощью CanvasGroup.
/// Позволяет реализовать простую анимацию объекта с появлением через прозрачность
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class AbstractRendererView : MonoBehaviour
{
    private const float ACTIVE_TARGET_ALPHA = 1f;
    private const float INACTIVE_TARGET_ALPHA = 0f;

    [SerializeField, Header("Скорость анимации появления / скрытия объекта")] 
    private float AnimationSpeed = 1 / 30;

    private CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Показать объект
    /// </summary>
    public virtual void ShowView()
    {
        while (canvasGroup.alpha < ACTIVE_TARGET_ALPHA)
        {
            canvasGroup.alpha += AnimationSpeed * Time.deltaTime;
        }

        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Скрыть объект
    /// </summary>
    public virtual void CloseView()
    {
        while (canvasGroup.alpha > INACTIVE_TARGET_ALPHA)
        {
            canvasGroup.alpha -= AnimationSpeed * Time.deltaTime;
        }

        canvasGroup.blocksRaycasts = false;
    }
}
