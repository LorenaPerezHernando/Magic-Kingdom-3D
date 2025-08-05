using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CinematicDialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image imageUI;

    [Header("Typing palabra a palabra")]
    [SerializeField] private float wordsPerSecond = 6f;
    [SerializeField] private bool allowInstantFinishSignal = true;

     private DialogueData _data;
    private int _index;
    private bool _active;

    private bool _isTyping;
    private Coroutine _typingRoutine;

    public void StartDialogue(DialogueData data)
    {
        _data = data;
        _index = 0;
        _active = true;

        if (imageUI && _data.imageTalker) imageUI.sprite = _data.imageTalker;
        panel.SetActive(true);
        ShowCurrentLine();
    }

    public void ShowNextLine()
    {
        if (!_active || _data == null) return;
        if (_isTyping) return;
        _index++;
        ShowCurrentLine();
    }

    public void FinishTypingInstantSignal()
    {
        if (!allowInstantFinishSignal) return;
        if (_isTyping) FinishTypingInstant();
    }

    public void EndDialogue()
    {
        _active = false;
        //if (_typingRoutine != null) { StopCoroutine(_typingRoutine); _typingRoutine = null; }
        _isTyping = false;
        panel.SetActive(false);
    }

    private void ShowCurrentLine()
    {
        if (_typingRoutine != null) { StopCoroutine(_typingRoutine); _typingRoutine = null; }
        _isTyping = false;

        if (_data == null || !_active)
        {
            EndDialogue();
            return;
        }

        if (_index < _data.frases.Length)
        {
            string line = _data.frases[_index];
            text.text = line;
            text.ForceMeshUpdate();
            text.maxVisibleWords = 0;

            if (imageUI && _data.imageTalker) imageUI.sprite = _data.imageTalker;

            _typingRoutine = StartCoroutine(TypeWordsRoutine());
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeWordsRoutine()
    {
        _isTyping = true;

        int totalWords = text.textInfo.wordCount;
        if (totalWords <= 0)
        {
            _isTyping = false;
            yield break;
        }

        float delay = 1f / Mathf.Max(0.0001f, wordsPerSecond);

        for (int i = 1; i <= totalWords; i++)
        {
            text.maxVisibleWords = i;

            float t = 0f;
            while (t < delay)
            {
                t += Time.deltaTime;
                yield return null;
            }
        }

        _isTyping = false;
    }

    private void FinishTypingInstant()
    {
        if (_typingRoutine != null) { StopCoroutine(_typingRoutine); _typingRoutine = null; }
        text.ForceMeshUpdate();
        text.maxVisibleWords = text.textInfo.wordCount;
        _isTyping = false;
    }
}
