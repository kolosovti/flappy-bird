using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Кнопка включения\выключения панели настроек
/// </summary>
public class SettingsPanelButton : AbstractButton
{
    /// <summary>
    /// Перечисляемый тип - действие по нажатию кнопки
    /// </summary>
    public enum ButtonType
    {
        OpenButton,
        CloseButton
    }

    [Inject]
    private UIController uiController;

    [SerializeField]
    private ButtonType buttonType;

    protected override void OnButtonClicked()
    {
        if (buttonType == ButtonType.OpenButton)
        {
            uiController.OpenCooperativePanel(uiController.SettingsPanel);
        }
        else
        {
            uiController.ClosePanel(uiController.SettingsPanel);
        }
    }
}
