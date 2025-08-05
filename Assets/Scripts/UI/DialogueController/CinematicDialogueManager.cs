using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CinematicDialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image imageTalker;
    private DialogueData _data;

    private int _index;
    private bool _active;

    private void Update()
    {
        //if (_active && Input.GetMouseButtonDown(0))
        //    Continue();
    }

    public void StartDialogue(DialogueData data)
    {
        _data = data;
        _index = 0;
        _active = true;

        if (imageTalker) imageTalker.sprite = _data.imageTalker;
        panel.SetActive(true);
        ShowLine();

    }

    private void ShowLine()
    {
        if (_index < _data.frases.Length && _active)
        {
            text.text = _data.frases[_index];
            
            StartCoroutine(ContinueLines());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator ContinueLines()
    {
        yield return new WaitForSeconds(1);
        _index++;
        ShowLine();
    }
    //public void Continue()
    //{
    //    _index++;
    //    ShowLine();
    //}

    public void EndDialogue()
    {
        _active = false;
        panel.SetActive(false);

    }
}
