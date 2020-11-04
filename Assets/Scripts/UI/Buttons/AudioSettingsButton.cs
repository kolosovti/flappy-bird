using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Кнопка настройки звука
/// </summary>
public class AudioSettingsButton : AbstractButton
{
    private const string SOUNDS_MUTED_BUTTON_TEXT = "Unmute sound";
    private const string SOUNDS_UNMUTED_BUTTON_TEXT = "Mute sound";

    [SerializeField]
    private Text buttonText;

    [Inject]
    private SoundsController soundsController;

    protected override void Awake()
    {
        base.Awake();

        SetButtonText();
    }

    protected override void OnButtonClicked()
    {
        soundsController.ChangeSoundState();
        SetButtonText();
    }

    private void SetButtonText()
    {
        if (buttonText)
        {
            if (soundsController.SoundVolume > SoundsController.MUTE_VOLUME)
            {
                buttonText.text = SOUNDS_UNMUTED_BUTTON_TEXT;
            }
            else
            {
                buttonText.text = SOUNDS_MUTED_BUTTON_TEXT;
            }
        }
    }
}
