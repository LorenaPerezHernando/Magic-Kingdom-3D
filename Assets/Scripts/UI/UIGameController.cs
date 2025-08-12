using Magic;
using Magic.Interact;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.UI
{


    public class UIGameController : MonoBehaviour
    {
        #region Fields
        private PlayerInteraction _interactionSystem;

        [Header("Interaction")]
        [SerializeField] private UIInteraction _interactionPanel;

        [Header("Spirits")]
        [SerializeField] private Image _portraitImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _storyText;
        [SerializeField] private UIUpgradeIcons _heartsUI;
        [SerializeField] private UIUpgradeIcons _starsUI;

        [Header("Fight")]
        [SerializeField] private Slider _playerSlider;
        [SerializeField] private Slider _bossSlider;
        [SerializeField] private GameObject _fightPanel;
        [SerializeField] private GameObject _deathPanel;


        #endregion
        private void Awake()
        {
            _interactionSystem = GameController.Instance.InteractionSystem;
            if (_interactionSystem == null)
                _interactionSystem = FindAnyObjectByType<PlayerInteraction>();
        
        }
        void Start()
        {
            _interactionSystem.OnShowInteraction += ShowInteraction;
            _interactionSystem.OnHideInteraction += HideInteraction;

            _deathPanel.SetActive(false);
            _fightPanel.SetActive(false);
        }

        private void OnEnable()
        {
            GameController.Instance.OnSpiritAdded += UpdateSpiritUI;
        }

        private void OnDisable()
        {
            if (GameController.Instance != null)
                GameController.Instance.OnSpiritAdded -= UpdateSpiritUI;
        }

        #region Public Methods
        public void UpdateSpiritUI(SpiritsPlayer spirit)
        {
            _portraitImage.sprite = spirit.spiritInfo.portrait;
            _nameText.text = spirit.spiritInfo?.name;
            _storyText.text = spirit.spiritInfo.story;
            _heartsUI.UpdateIcons(spirit.spiritInfo.heartSprite, spirit.affection);
            _starsUI.UpdateIcons(spirit.spiritInfo.starsSprite, spirit.level);
        }

        internal void VictoryOnFightWithBoss1()
        {
            print("Load Cinematic Scene after fight");
            GameController.Instance.LoadScene(2);
        }
        public void ShowInteraction(string message)
        {
            _interactionPanel.Show(message);
        }
        public void HideInteraction()
        {
            _interactionPanel.Hide();

        }

        public void ShowDeathPanel()
        {
            _deathPanel.SetActive(true);
        }

        public void ShowFightPanel()
        {
            _fightPanel.SetActive(true);
        }
        public void HideFightPanel()
        {
            _fightPanel.SetActive(false);
        }
        #endregion

        #region Private Methods
        internal void UpdatePlayerHealth(float value)
        {
            _playerSlider.value = value;
        }

        internal void UpdateBossHealth(float value)
        {
            _bossSlider.value = value;
        }
        #endregion
    }
}

