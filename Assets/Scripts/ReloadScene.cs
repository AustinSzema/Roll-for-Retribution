using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("]"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        
    }
}
