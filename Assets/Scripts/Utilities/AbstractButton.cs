using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Абстрактная кнопка. 
/// В наследниках достаточно реализовать метод OnButtonClicked, который будет вызван по клику на кнопку
/// </summary>
public abstract class AbstractButton : MonoBehaviour
{ 
    private Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    protected virtual void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    protected abstract void OnButtonClicked();
}
