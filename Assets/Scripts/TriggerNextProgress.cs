using Magic;
using UnityEngine;

public enum ProgressType { Spirits, Villages, Puzzles, Bosses, HealingPlants }
public class TriggerNextProgress : MonoBehaviour
{
    [SerializeField] private ProgressType type;
    [SerializeField] private int _requiredAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentValue = GetValueFromProgress(type);
            if (currentValue >= _requiredAmount)
            {
                Debug.Log($"Acceso permitido: tienes {currentValue} {type}");
                // TODO Acción permitida
            }
            else
            {
                Debug.Log($"Acceso denegado: solo tienes {currentValue} {type}");
                // TODO Acción bloqueada
            }
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
}

