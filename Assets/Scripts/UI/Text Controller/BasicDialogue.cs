using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BasicDialogue : MonoBehaviour
{
    #region Fields & Properties
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private TextMeshProUGUI _messageText;
    [Header("Learning Conversation")]
    [SerializeField] private bool _hasPlayed;
    [SerializeField] private string[] _learningMensajesIniciales;
    private int _learningCurrentMessageIndex = 0;
    [Header("Reward")]
    [SerializeField] private GameObject _reward;
    [Header("Basic Conversation")]
    [SerializeField] private string[] _basicMensajesIniciales;
    private int _basicCurrentMessageIndex = 0;
    #endregion

    #region UnityCallBacks
    private void Awake()
    {
        _messagePanel.SetActive(false);

    }


    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.Return)))
        {
            LearningConversation();
        }
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Trigger Enter");
            if(!_hasPlayed)
            {
            //TODO Bloquear el movimiento y animacion de conversacion 
                _messagePanel?.SetActive(true);
                LearningConversation();
            }
            else
            {
                _messagePanel.SetActive(true);
                BasicConversation();
            }
        }    

    }
    private void OnTriggerExit(Collider other)
    {
        if (!_hasPlayed)
        {
            _messagePanel.SetActive(false);
            _basicCurrentMessageIndex = 0;

        }
    }

    IEnumerator NextAutomaticMessage()
    {
        print("Next Message");
        yield return new WaitForSeconds(1f);
        BasicConversation();
    }

    private void LearningConversation()
    {

        if (_learningCurrentMessageIndex < _learningMensajesIniciales.Length )
        {
            _messageText.text = _learningMensajesIniciales[_learningCurrentMessageIndex];
            _learningCurrentMessageIndex++;
            print("Learning Conversaion");
            
            return;

        }

        else
        {
            _messagePanel.SetActive(false);
            _hasPlayed = true; //Flag for next conversation
            //TODO Reward for talking
        }
    }

    private void BasicConversation()
    {
        if (_basicCurrentMessageIndex < _basicMensajesIniciales.Length )
        {
            _messageText.enabled = true;
            _messageText.text = _basicMensajesIniciales[_basicCurrentMessageIndex];
            _basicCurrentMessageIndex++;
            StartCoroutine(NextAutomaticMessage());
            return;


        }

        else
        {
            _basicCurrentMessageIndex = 0;
            _messagePanel.SetActive(false) ;

        }
    }

    #endregion


}
