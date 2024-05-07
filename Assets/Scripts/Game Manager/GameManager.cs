using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        }
        else
        {
            // If an instance already exists and it's not this instance, destroy this one
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
    }

    // Player Fields
    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public bool playerIsGrounded;
    [HideInInspector] public bool playerIsFlying;

    [field: SerializeField] public int playerMaxHealth { get; private set; } = 20;

    [HideInInspector] public int playerCurrentHealth;

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
    [HideInInspector] public ActiveShotType activeShot;
    [HideInInspector] public Vector3 handPosition;

    // Flight Fields
    [HideInInspector] public float flightDuration;


    [HideInInspector] public bool enemiesShouldMove = true;
    
    
    private int _playerHits = 0;

    [HideInInspector] public bool canUseSuper { get; private set; } = false;


    public enum ActiveShotType
    {
        Shotgun,
        Rocket,
        Spray,
        Beam
    }


    private void Start()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Kill Count: " + killCount);
        if (Input.GetKeyDown(KeyCode.Return) && canUseSuper)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy e in enemies)
            {
                if (e.gameObject.activeInHierarchy)
                {
                    e.takeDamage(1);
                }
            }

            Debug.Log("used Super");
            _playerHits = 0;
            canUseSuper = false;
        }
    }


    public void IncreaseSuperMeter()
    {
        _playerHits++;
        if (_playerHits >= 100)
        {
            canUseSuper = true;
        }
    }
}