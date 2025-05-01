using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NarrativeTextSingleton : MonoBehaviour{
    public TextMeshProUGUI narrativeText;

    public static NarrativeTextSingleton Instance;

    public AudioSource audioSource;
    public AudioClip pickupItemClip;

    public FadeToColor nextSceneTransition;


    public bool isFading = false;
    
    private bool isGrabbed = false; //Don't double display text, if we're grabbing it dont show the object info from the highlight

    public void Awake(){
        Instance = this;
    }

    public void SetText(string txt, int staminaCost){
        narrativeText.text = txt + "\n \nCosts " + staminaCost + " stamina";
        audioSource.PlayOneShot(pickupItemClip);
        isGrabbed = true;
    }

    public void ClearText(){
        narrativeText.text = "";
        isGrabbed = false;
    }


    public void SetHighlightText(string txt)
    {
        if (!isGrabbed)
        {
            narrativeText.text = txt;
        }
    }


    public void ClearHighlightText()
    {
        // Only clear if grab text isn't showing
        if (!isGrabbed)
        {
            narrativeText.text = "";
        }
    }

    public void StartNextDay()
    {
        isFading = true;
        
        if(DayManagerSingleton.Instance.day >= DayManagerSingleton.Instance.lastDay){
            nextSceneTransition.FadeImage(SceneManager.GetActiveScene().buildIndex+1); // set the second scene to be the same as the default room but nothing is interactable except the vault door
        }else{
            DayManagerSingleton.Instance.day++;
            nextSceneTransition.FadeImage(SceneManager.GetActiveScene().buildIndex);
        }

    }
}