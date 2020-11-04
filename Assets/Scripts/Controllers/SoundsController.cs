using UnityEngine;
using Zenject;

/// <summary>
/// Управление звуком в игре
/// </summary>
public class SoundsController : MonoBehaviour
{
    /// <summary>
    /// Значение для выключения звука
    /// </summary>
    public const float MUTE_VOLUME = 0f;

    /// <summary>
    /// Значения для включения полной громкости звука
    /// </summary>
    public const float FULL_VOLUME = 1f;

    /// <summary>
    /// Ключ PlayerPrefs для значения громкости
    /// </summary>
    private const string SOUND_VOLUME = "SoundVolume";

    [SerializeField] private AudioSource flySound;
    [SerializeField] private AudioSource scoreSound;

    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus _signalBus)
    {
        signalBus = _signalBus;
        signalBus.Subscribe<PlayerController.MovePlayerSignal>(PlayerMoveSignalHandler);
    }

    /// <summary>
    /// Свойство - громкость звука
    /// </summary>
    public float SoundVolume
    {
        get
        {
            return AudioListener.volume;
        }
        private set
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(SOUND_VOLUME, value);
        }
    }

    private void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(SOUND_VOLUME, FULL_VOLUME);
    }

    /// <summary>
    /// Изменить состояние звука - включен или выключен
    /// </summary>
    public void ChangeSoundState()
    {
        if (SoundVolume > SoundsController.MUTE_VOLUME)
        {
            SoundVolume = MUTE_VOLUME;
        }
        else
        {
            SoundVolume = FULL_VOLUME;
        }
    }

    /// <summary>
    /// Проиграть звук увеличения игрового счёта
    /// </summary>
    public void PlayScoreSound()
    {
        scoreSound.Play();
    }

    private void PlayerMoveSignalHandler()
    {
        flySound.Play();
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<PlayerController.MovePlayerSignal>(PlayerMoveSignalHandler);
    }
}
