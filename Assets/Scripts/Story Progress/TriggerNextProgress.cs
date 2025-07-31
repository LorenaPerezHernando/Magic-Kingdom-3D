using Magic;
using UnityEngine;

public enum ProgressType { Spirits, Villages, Puzzles, Bosses, HealingPlants }
public class TriggerNextProgress : MonoBehaviour
{
    [Header("Progress")]
    [SerializeField] private ProgressType type;
    [SerializeField] private int _requiredAmount;
    [SerializeField] private int _currentValue;
    [Header("Goal Completed")]
    [SerializeField] private GameObject[] _objectsToDesactivate;
    private void Update()
    {
        _currentValue = GetValueFromProgress(type);
        if(_currentValue >= _requiredAmount)
        {
            OpenNextPuzzle();
        }
    }
    private int GetValueFromProgress(ProgressType type)
    {
        var progress = GameController.Instance.GameProgress;

        return type switch
        {
            ProgressType.Spirits => progress.spirits,
            ProgressType.Villages => progress.villages,
            ProgressType.Puzzles => progress.puzzlesCompleted,
            ProgressType.Bosses => progress.bossesDefeated,
            ProgressType.HealingPlants => progress.healingPlants,
            _ => 0
        };
    }

    private void OpenNextPuzzle()
    {
        foreach (var obj in _objectsToDesactivate)
            obj.SetActive(false);
        Destroy(gameObject);
    }
}

