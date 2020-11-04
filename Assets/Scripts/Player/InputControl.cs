using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

/// <summary>
/// Контроллер ввода на игровой сцене
/// </summary>
public class InputControl : ITickable
{
    /// <summary>
    /// Код левой кнопки мыши
    /// </summary>
    public const int MOUSE_LEFT_BUTTON_KEY = 0;

    [Inject]
    private Settings settings;

    private SignalBus signalBus;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_signalBus">Шина сигналов (событий)</param>
    public InputControl(SignalBus _signalBus)
    {
        signalBus = _signalBus;
    }

    /// <summary>
    /// Реализация интерфейса ITickable
    /// </summary>
    public void Tick()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON_KEY))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                signalBus.Fire(new MouseInputDetectSignal() { });
            }
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    signalBus.Fire(new TouchInputDetectSignal() { });

                }
            }
        }
    }

    /// <summary>
    /// Класс для отправки сигнала о клике по объекту в сцене
    /// </summary>
    public class TouchInputDetectSignal { }

    /// <summary>
    /// Класс для отправки сигнала о клике по объекту в сцене
    /// </summary>
    public class MouseInputDetectSignal { }

    /// <summary>
    /// Настройки ввода
    /// </summary>
    [Serializable]
    public class Settings
    {
        [Header("Основная камера на сцене")]
        public Camera mainCamera;
    }
}
