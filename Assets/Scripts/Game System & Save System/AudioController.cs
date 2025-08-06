using UnityEngine;

public class AudioController : MonoBehaviour
{
    const float MAX_VOLUME = 0.1f;

    #region Fields
    [Header("FX General")]
    [SerializeField] private AudioSource _walk;
    [SerializeField] private AudioSource _run;
    [Header("FX Battle")]
    [SerializeField] private AudioSource _bossScream;
    [SerializeField] private AudioSource _bossLaugh;
    [SerializeField] private AudioSource _bossTalking;
    [SerializeField] private AudioSource _bossMagicPower;
    [SerializeField] private AudioSource _playerHealingPower;
    [SerializeField] private AudioSource _playerTornadoSound;
    [Header("FX Puzzles")]
    [SerializeField] private AudioSource _progressSound;
    [SerializeField] private AudioSource _puzzleCompletedSound;
    [SerializeField] private AudioSource _citizenWateringSound;
    [SerializeField] private AudioSource _grabObject;
    [SerializeField] private AudioSource _movingClock;
    [SerializeField] private AudioSource _clock; 
   
    [Space(10)]

    [Header("Music")]
    [SerializeField] private AudioSource _calmMusic;
    [SerializeField] private AudioSource _battleMusic;
    #endregion
    void Start()
    {
        _calmMusic.Play();
    }

    public void PlayCalmMusic() => _calmMusic.Play();
    public void PlayBattleMusic() => _battleMusic.Play();

    public void PlayWalkingSounds() => _walk.Play();
    public void PlayRunSounds() => _run.Play();

    public void PlayBossScream() => _bossScream.Play();
    public void PlayBossLaugh() => _bossLaugh.Play();
    public void PlayBossTalking() => _bossTalking.Play();
    public void PlayBossMagicPower() => _bossMagicPower.Play();

    public void PlayPlayerHealingPower() => _playerHealingPower.Play();
    public void PlayPlayerTornadoSound() => _playerTornadoSound.Play();

    public void PlayProgressSound() => _progressSound.Play();
    public void PlayPuzzleCompletedSound() => _puzzleCompletedSound.Play();
    public void PlayCitizenWateringSound() => _citizenWateringSound.Play();
    public void PlayGrabObject() => _grabObject.Play();
    public void PlayMovingClock() => _movingClock.Play();
    public void PlayClock() => _clock.Play();

}
