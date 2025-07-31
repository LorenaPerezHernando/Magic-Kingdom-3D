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
    [SerializeField] private GameObject[] _objectsToActivate;
    [SerializeField] private GameObject[] _objectsToDesactivate;

    private void Update()
    {
        _currentValue = GetValueFromProgress(type);
        if(_currentValue >= _requiredAmount)
        {
            DesactivateObjects();
            ActivateObjects();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateObjects();
        }
    }
    private void ActivateObjects()
    {
        foreach (var obj in _objectsToActivate)
            obj.SetActive(true);
    }
    private void DesactivateObjects()
    {
        foreach (var obj in _objectsToDesactivate)
            obj.SetActive(false);
    }
}

