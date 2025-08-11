using UnityEngine;

public enum MoveState { None, Walk, Run }
public class AudioController : MonoBehaviour
{
    const float MAX_VOLUME = 0.1f;
   

    #region Fields
    [Header("FX Movement")]
    [SerializeField] private AudioSource _walk;
    [SerializeField] private AudioSource _run;
    private AudioSource _currentMovementSound;
    private MoveState _currentMoveState = MoveState.None;
    [Header("FX Battle")]
    [SerializeField] private AudioSource _bossScream;
    [SerializeField] private AudioSource _bossLaugh;
    [SerializeField] private AudioSource _bossTalking;
    [SerializeField] private AudioSource _bossMagicPower;
    [SerializeField] private AudioSource _playerHealingPower;
    [SerializeField] private AudioSource _playerTornadoSound;
    [SerializeField] private AudioSource _playertired;
    [Header("FX Puzzles")]
    [SerializeField] private AudioSource _progressSound;
    [SerializeField] private AudioSource _puzzleCompletedSound;
    [SerializeField] private AudioSource _citizenWateringSound;
    [SerializeField] private AudioSource _grabObject;
    [SerializeField] private AudioSource _movingClock;
    [SerializeField] private AudioSource _clock; 
   
    [Space(10)]

    [Header("Music")]
    private AudioSource _currentMusic;
    [SerializeField] private AudioSource _calmMusic;
    [SerializeField] private AudioSource _battleMusic;
    #endregion
    void Start()
    {
        _calmMusic.Play();
    }

    public void PlayCalmMusic() => SwitchMusic(_calmMusic);
    public void PlayBattleMusic() => SwitchMusic(_battleMusic);

    public void PlayWalkingSounds() => SwitchToMovementSound(_walk);
    public void PlayRunSounds() => SwitchToMovementSound(_run);
    public void StopMovSounds()
    {
        _currentMovementSound.Stop();
        _currentMovementSound = null;
    }

    public void PlayBossScream() => _bossScream.Play();
    public void PlayBossLaugh() => _bossLaugh.Play();
    public void PlayBossTalking() => _bossTalking.Play();
    public void PlayBossMagicPower() => _bossMagicPower.Play();

    public void PlayPlayerHealingPower() => _playerHealingPower.Play();
    public void PlayPlayerTornadoSound() => _playerTornadoSound.Play();
    public void PlayPlayerTired() => _playertired.Play();
   

    public void PlayProgressSound() => _progressSound.Play();
    public void PlayPuzzleCompletedSound() => _puzzleCompletedSound.Play();
    public void PlayCitizenWateringSound() => _citizenWateringSound.Play();
    public void StopCitizenWateringSound() => _citizenWateringSound.Stop();
    public void PlayGrabObject() => _grabObject.Play();
    public void PlayMovingClock() => _movingClock.Play();
    public void PlayClock() => _clock.Play();

    private void SwitchMusic(AudioSource next)
    {
        if (_currentMusic != null && _currentMusic.isPlaying)
            _currentMusic.Stop();

        next.Play();
        _currentMusic = next;
    }

    public void SetMovementState(MoveState state)
    {
        if (state == _currentMoveState) return; // evitar retrigger

        _currentMoveState = state;

        if (state == MoveState.None)
        {
            StopMovementSounds();
            return;
        }

        var next = state == MoveState.Run ? _run : _walk;
        SwitchToMovementSound(next);
    }

    public void StopMovementSounds()
    {
        if (_currentMovementSound != null)
        {
            _currentMovementSound.Stop();
            _currentMovementSound = null;
        }
    }

    private void SwitchToMovementSound(AudioSource next)
    {
        if (next == null) return;

        // si ya es el mismo, asegúrate de que suena
        if (_currentMovementSound == next)
        {
            if (!_currentMovementSound.isPlaying)
                _currentMovementSound.Play();
            return;
        }

        // apagar el anterior
        if (_currentMovementSound != null && _currentMovementSound.isPlaying)
            _currentMovementSound.Stop();

        // encender el nuevo
        _currentMovementSound = next;
        if (!_currentMovementSound.isPlaying)
            _currentMovementSound.Play();
    }

}
