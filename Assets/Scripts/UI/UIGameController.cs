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
        [SerializeField] private Sprite _interrogationImage;

        [Header("Spirits")]
        [SerializeField] private Image _buttonOwlImage;
        [SerializeField] private GameObject _spiritsPanel;
        [SerializeField] private Image _portraitImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _storyText;
        [SerializeField] private UIUpgradeIcons _heartsUI;
        [SerializeField] private UIUpgradeIcons _starsUI;
        [Header("Spirits Powers")]
        [SerializeField] private GameObject _attack1Row;         // el contenedor de la fila (para poder ocultarla)
        [SerializeField] private Image _attack1Icon;
        [SerializeField] private TextMeshProUGUI _attack1Name;
        [SerializeField] private TextMeshProUGUI _attack1Desc;

        [SerializeField] private GameObject _attack2Row;
        [SerializeField] private Image _attack2Icon;
        [SerializeField] private TextMeshProUGUI _attack2Name;
        [SerializeField] private TextMeshProUGUI _attack2Desc;

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
            UpdateSpiritUI();
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
        public void UpdateSpiritUI(SpiritsPlayer spirit = null)
        {
            if (GameController.Instance.GameProgress.spirits == 0 ||  spirit == null || spirit.spiritInfo == null)
            {
                _spiritsPanel.SetActive(false);
                _buttonOwlImage.sprite = _interrogationImage;
                _portraitImage.sprite = _interrogationImage;
                _nameText.text = "??";
                _storyText.text = "??";


                _attack1Icon.sprite = _interrogationImage;
                _attack1Name.text = "??";
                _attack1Desc.text = "??";
                _attack2Icon.sprite = _interrogationImage;
                _attack2Name.text = "??";
                _attack2Desc.text = "??";
                return;
            }
            print("Spirit not null");

            _spiritsPanel.SetActive(true);
            _buttonOwlImage.sprite = spirit.spiritInfo.portrait;
            _portraitImage.sprite = spirit.spiritInfo.portrait;
            _nameText.text = string.IsNullOrEmpty(spirit.spiritInfo.spiritName)
                ? spirit.spiritInfo.name
                : spirit.spiritInfo.spiritName;
            _storyText.text = spirit.spiritInfo.story;

            _heartsUI.UpdateIcons(spirit.spiritInfo.heartSprite, spirit.affection);
            _starsUI.UpdateIcons(spirit.spiritInfo.starsSprite, spirit.level); 

            var pool = spirit.spiritInfo.attacks;
            if (pool == null || pool.Length == 0)
            {
                _attack1Row?.SetActive(false);
                _attack2Row?.SetActive(false);
                return;
            }

            // Attack 1
            int i1 = Mathf.Clamp(spirit.selectedAttack1, 0, pool.Length - 1);

            // Attack 2 
            int i2 = Mathf.Clamp(spirit.selectedAttack2, 0, pool.Length - 1);
            if (pool.Length > 1 && i2 == i1) i2 = (i1 + 1) % pool.Length;

            var a1 = pool[i1];
            _attack1Row?.SetActive(true);
            if (_attack1Icon) _attack1Icon.sprite = a1.icon;
            if (_attack1Name) _attack1Name.text = string.IsNullOrEmpty(a1.name) ? "Attack 1" : a1.name;
            if (_attack1Desc) _attack1Desc.text = string.IsNullOrEmpty(a1.description) ? "" : a1.description;

            if (pool.Length >= 2)
            {
                var a2 = pool[i2];
                _attack2Row?.SetActive(true);
                if (_attack2Icon) _attack2Icon.sprite = a2.icon;
                if (_attack2Name) _attack2Name.text = string.IsNullOrEmpty(a2.name) ? "Attack 2" : a2.name;
                if (_attack2Desc) _attack2Desc.text = string.IsNullOrEmpty(a2.description) ? "" : a2.description;
            }
            else
            {
                _attack2Row?.SetActive(false);
            }
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

