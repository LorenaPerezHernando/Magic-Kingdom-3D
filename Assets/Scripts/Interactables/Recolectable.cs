using UnityEngine;
using Magic.Interaction;


namespace Magic.Recolectables { 

    public class Recolectable : MonoBehaviour, IInteractable 
    {
        public enum RecolectableType
        {
            Plant1,
            Plant2,
        }
#region Properties

        [field: SerializeField] public int Count { get; set; }
        [field: SerializeField] public RecolectableType Type { get; set; }

#endregion

#region Fields
        [SerializeField] private InteractableInfo _interactableInfo;

#endregion

#region Public Methods
        public InteractableInfo GetInfo()
        {
            _interactableInfo.Type = Type.ToString();

            return _interactableInfo;



        }
        public void Interact()
        {
            Debug.Log("Recolectado " + Type);
            Destroy(gameObject); // o lo que tú quieras
        }

#endregion

    }
}
