using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScene : MonoBehaviour
{
    [SerializeField] private int _sceneID;


    public void LoadNewScene()
    {
        SceneManager.LoadScene(_sceneID);
    }
}
