using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{

    [SerializeField] private Image screenFade;

    private float alpha = 0f;

    [SerializeField] private float fadeSpeed = 0.1f;

    public bool fadeToBlack { get; set; }

    [SerializeField] private PlayDialogue playDialogue;

    private Color black = new Color(0f, 0f, 0f, 1f);
    private Color transparent = new Color(0f, 0f, 0f, 0f);
    [SerializeField] private int nextScene;
    private bool tran = true;

    private void Start()
    {
        fadeToBlack = false;
        alpha = 1f;
        screenFade.color = new Color(0f, 0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack == true && screenFade.color.a != 100f)
        {
            //Debug.Log(alpha);
            screenFade.color = new Color(0f, 0f, 0f, alpha);
            alpha += fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp(alpha, 0, 1);
        }
        if (fadeToBlack == true && screenFade.color == black)
        {
            
            if (!tran) { return; }
            Debug.Log("dog");
            tran = false;
            Invoke("Transition", 1);
            //if (SceneManager.GetSceneByBuildIndex(nextScene).IsValid())
            //{
            //    Debug.Log("cat");
            //    if (!tran) { return; }
            //    tran = false;
            //    Invoke("Transition", 1);
            //}
            
        }

        if (fadeToBlack == false && screenFade.color.a != 0f)
        {
            //Debug.Log(alpha);
            screenFade.color = new Color(0f, 0f, 0f, alpha);
            alpha -= fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp(alpha, 0, 1);
        }

        //if (fadeToBlack)
        //{
        //    screenFade.color = Color.Lerp(screenFade.color, black, fadeSpeed * Time.deltaTime);
        //} else
        //{
        //    screenFade.color = Color.Lerp(screenFade.color, transparent, fadeSpeed * Time.deltaTime);
        //}


    }

    public void FadeMe()
    {
        fadeToBlack = true;
    }

    void Transition()
    {
        SceneManager.LoadScene(nextScene);
    }
}
