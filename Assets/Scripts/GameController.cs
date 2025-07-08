using Magic.Interaction;
using UnityEngine;

namespace Magic
{
    public class GameController : Singleton<GameController>
    {
        public InteractionSystem InteractionSystem => _interactionSystem;

        [Header("Player")]
        [SerializeField] protected InteractionSystem _interactionSystem;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
