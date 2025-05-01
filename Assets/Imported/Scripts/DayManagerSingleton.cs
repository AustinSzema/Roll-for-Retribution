using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManagerSingleton : MonoBehaviour
{

    [HideInInspector] public int day = 1; // using 0 indexing
    [HideInInspector] public int lastDay = 3;

    public static DayManagerSingleton Instance;

    public void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

    }
}