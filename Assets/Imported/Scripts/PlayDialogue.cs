using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayDialogue : MonoBehaviour
{
    [SerializeField] private Dialogue dialogueSO;


    [SerializeField] private UnityEvent _transition;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private float textDelay = 4f;

    [SerializeField] private bool isTransitionScene;

    [SerializeField] private Voice voiceSO;

    [SerializeField] private AudioSource _audioSource;

    int index = 0;

    void Start()
    {
        dialogueText.text = dialogueSO.DialogueText[index];
        InvokeRepeating("nextString", 0.0f, textDelay);
    }


    void nextString()
    {
        if (index <= dialogueSO.DialogueText.Length - 1)
        {

            dialogueText.text = dialogueSO.DialogueText[index];

            _audioSource.PlayOneShot(voiceSO.VoiceActing[index]);

            if (dialogueSO.DialogueText[index].Length > 0 && !char.IsLetter(dialogueSO.DialogueText[index][0]))
            {
                dialogueText.fontStyle = FontStyles.Normal;

            }
            else
            {

                dialogueText.fontStyle = FontStyles.Italic | FontStyles.Bold;

            }

            index++;
        }
        if (index == dialogueSO.DialogueText.Length)
        {
            CancelInvoke("nextString");
            if (isTransitionScene)
            {
                Invoke("Transition", textDelay);
            }
            Debug.Log("Invoke stopped");
        }
    }

    private void Transition()
    {
        _transition.Invoke();
    }

}



