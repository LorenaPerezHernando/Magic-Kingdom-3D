using TMPro;
using UnityEngine;
using Magic.Inventory;
using Magic.UI;
using System.Collections;
using UnityEngine.UI;

namespace Magic.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Reward Message")]
        [SerializeField] private GameObject _rewardPanel;
        [SerializeField] private TextMeshProUGUI _rewardText;

        [Header("Dialogue")]
        [SerializeField] private Image _imageTalker;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _pausePanel;


        public static DialogueManager Instance;

        private DialogueData _currentData;
        private int _index;
        private bool _isCompleting;

        private void Awake() => Instance = this;

        public void StartDialogue(DialogueData data)
        {
            if (data == null) return;

            _currentData = data;
            if (_imageTalker != null) _imageTalker.sprite = _currentData.imageTalker;

            _index = 0;
            _isCompleting = false;

            _panel.SetActive(true);
            _pausePanel.SetActive(false);
            GameController.Instance?.PauseGame();
            ShowLine();
        }

        private void Update()
        {
            if (_panel.activeInHierarchy && !_isCompleting && Input.GetMouseButtonDown(0))
                Continue();
        }

        private void ShowLine()
        {
            if (_currentData == null) return;

            if (_currentData.frases != null && _index < _currentData.frases.Length)
            {
                _text.text = _currentData.frases[_index];
            }
            else
            {
                StartCoroutine(CompleteDialogue());
            }
        }

        public void Continue()
        {
            _index++;
            ShowLine();
        }

        private IEnumerator CompleteDialogue()
        {
            if (_isCompleting) yield break;
            _isCompleting = true;

            if (_currentData.hasReward)
            {
                if (_currentData.rewardItem != null)
                {
                    InventoryManager.Instance.AddItem(_currentData.rewardItem, _currentData.rewardAmount);
                    yield return ShowRewardMessage($"Has recibido {_currentData.rewardItem.name}");
                }
                else if (!string.IsNullOrEmpty(_currentData.rewardText))
                {
                    yield return ShowRewardMessage(_currentData.rewardText);
                }
            }

            _panel.SetActive(false);
            GameController.Instance?.ResumeGame();

            _isCompleting = false;
            _currentData = null;
        }

        public IEnumerator ShowRewardMessage(string message)
        {
            if (_rewardPanel == null || _rewardText == null) yield break;

            _rewardPanel.transform.SetAsLastSibling();
            _rewardPanel.SetActive(true);
            _rewardText.text = message;
            yield return new WaitForSecondsRealtime(2.5f);
            _rewardPanel.SetActive(false);
        }

        public void CloseDialogue()
        {
            _panel.SetActive(false);
            _pausePanel.SetActive(true);
            GameController.Instance?.ResumeGame();
            _isCompleting = false;
            _currentData = null;
        }
    }
}