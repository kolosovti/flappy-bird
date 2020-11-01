using System;
using UnityEngine;
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
    private bool lastTimeMouseButtonClicked;

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
            if (!lastTimeMouseButtonClicked)
            {
                signalBus.Fire(new MouseInputDetectSignal() { PressedMouseButtonKey = MOUSE_LEFT_BUTTON_KEY });
            }
        }
        else
        {
            lastTimeMouseButtonClicked = false;
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        signalBus.Fire(new TouchInputDetectSignal() { objectTag = hit.collider.tag });
                    }
                }
            }
        }
    }

    /// <summary>
    /// Класс для отправки сигнала о клике по объекту в сцене
    /// </summary>
    public class TouchInputDetectSignal
    {
        /// <summary>
        /// Название объекта, в который попал рейкаст
        /// </summary>
        public string objectTag;
    }

    /// <summary>
    /// Класс для отправки сигнала о клике по объекту в сцене
    /// </summary>
    public class MouseInputDetectSignal
    {
        /// <summary>
        /// Название объекта, в который попал рейкаст
        /// </summary>
        public int PressedMouseButtonKey;
    }

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
