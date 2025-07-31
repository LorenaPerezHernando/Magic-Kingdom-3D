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
        public static DialogueManager Instance;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _text;
        private DialogueData _currentData;
        private int _index;

        private void Awake() => Instance = this;

        public void StartDialogue(DialogueData data)
        {
            print("StartDialogue");
            _currentData = data;
            _imageTalker.sprite = _currentData.imageTalker;
            _index = 0;
            _panel.SetActive(true);
            GameController.Instance.PauseGame();
            ShowLine();
        }

        void Update()
        {
            if (_panel.activeInHierarchy && Input.GetMouseButtonDown(0))
                Continue();
        }

        void ShowLine()
        {
            print("ShowLine");
            if (_index < _currentData.frases.Length)
            {
                _text.text = _currentData.frases[_index];
            }
            else
            {
                if (_currentData.hasReward && _currentData.rewardItem != null)
                {
                    GiveReward(_currentData.rewardItem, _currentData.rewardAmount);
                }
                GameController.Instance.ResumeGame();
                
                
            }
        }
        void GiveReward(Item item, int amount)
        {
            InventoryManager.Instance.AddItem(item, amount);


            StartCoroutine(ShowRewardMessage($"Has recibido {item.name}"));
        }


        public void Continue()
        {
            _index++;
            ShowLine();
        }

        IEnumerator ShowRewardMessage(string message)
        {
            _rewardPanel.SetActive(true);
            _rewardText.text = message;
            yield return new WaitForSeconds(2f);
             _rewardPanel.SetActive(false);
            _panel.SetActive(false);
        }

        public void CloseDialogue()
        {
            _panel.SetActive(false);
            GameController.Instance.ResumeGame();
        }
    }
}

