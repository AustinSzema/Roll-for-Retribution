using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Static reference to the GameManager instance
    private static GameManager _instance;

    // Property to access the GameManager instance
    public static GameManager Instance
    {
        get
        {
            // If instance hasn't been set yet, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                // If it still hasn't been found, create a new GameObject and add GameManager to it
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            // If an instance already exists and it's not this instance, destroy this one
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    

    // Player Fields
    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public bool playerIsGrounded;
    [HideInInspector] public bool playerIsFlying;

    [field: SerializeField] public int playerMaxHealth { get; private set; } = 20;

    [HideInInspector] public float playerCurrentHealth;

    // Game State Fields
    [HideInInspector] public int overworldDimensionSceneIndex;
    [HideInInspector] public bool outOfFuel;


    [HideInInspector] public int killCount;


    [HideInInspector] public bool gameIsPaused;

    // Enemy Fields
    [HideInInspector] public int enemyCount;

    [HideInInspector] public bool demonInHand;
    [HideInInspector] public bool pullingInDemons;

    // Weapon Fields
    //[HideInInspector] public ActiveShotType activeShot;
    [HideInInspector] public Vector3 handPosition;

    // Flight Fields
    [HideInInspector] public float flightDuration;


    [HideInInspector] public int currentRound = 0;


    public bool enemiesShouldMove = true;


    [HideInInspector] public int enemiesHit = 0;

    [HideInInspector] public bool canUseSuper { get; private set; } = false;
    [HideInInspector] public int superMeterActivationAmount { get; private set; } = 2000;

    [HideInInspector] public int soulCount = 0;

    [HideInInspector] public float snapRangeAroundHand = 3f;
    
    public bool shopActive = false;
    
    [HideInInspector] public List<Weapon> weapons = new List<Weapon>();

    
    
    // public enum ActiveShotType
    // {
    //     Shotgun,
    //     Rocket,
    //     Spray,
    //     Beam
    // }


    private AudioManager _audioManager;

    private void Start()
    {
        Setup();
    }

    private void OnEnable()
    {
        Setup();
    }

    // Example function that you want to call at the start of each scene
    public void OnSceneStart()
    {
        Debug.Log("Scene has started!");
        Setup();
        // Add the logic you want to execute at the start of each scene
    }


    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when this object is destroyed
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This function is called when a new scene is loaded
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        OnSceneStart();
    }

    private void Setup()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        _audioManager = AudioManager.Instance;
        _audioManager.PlayMainMusic();
        Debug.Log("Game Manager Start");

    }



}