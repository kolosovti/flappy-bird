using UnityEngine;
using Zenject;

/// <summary>
/// Кнопка открытия / закрытия панели лучшего счёта
/// </summary>
public class BestScorePanelButton : AbstractButton
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
            uiController.OpenCooperativePanel(uiController.BestScorePanel);
        }
        else
        {
            uiController.ClosePanel(uiController.BestScorePanel);
        }
    }
}
