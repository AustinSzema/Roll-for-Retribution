using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

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


    public bool enemiesShouldMove = true;
    
    
    [HideInInspector] public int enemiesHit = 0;

    [HideInInspector] public bool canUseSuper { get; private set; } = false;
    [HideInInspector] public int superMeterActivationAmount { get; private set; } = 2000;

    [HideInInspector] public int soulCount = 0;

    public bool shopActive = false;
    
    public enum ActiveShotType
    {
        Shotgun,
        Rocket,
        Spray,
        Beam
    }


    private AudioManager _audioManager;
    
    private void Start()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        _audioManager = AudioManager.Instance;
        _audioManager.PlayMainMusic();
        
    }

    // Update is called once per frame
    void Update() // This entire thing should be in the super meter not in the game manager. Probably
    {
        Debug.Log("Kill Count: " + killCount);
        if (canUseSuper && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            FindObjectOfType<DomainExpansion>().ExpandDomain(); // TODO: temporary hack, dont use find object
            Enemy[] enemies = FindObjectsOfType<Enemy>(); // TODO: Instead of doing this we should have an enemy manager that keeps track of all of the enemies and then call takeDamage on all of the active ones.
            foreach (Enemy e in enemies)
            {
                if (e.gameObject.activeInHierarchy)
                {
                    e.takeDamage(e.healthPoints);
                }
            }

            Debug.Log("used Super");
            enemiesHit -= superMeterActivationAmount;
        }
        
        canUseSuper = enemiesHit >= superMeterActivationAmount;
    }


    public void IncreaseSuperMeter()
    {
        enemiesHit++;
        Debug.Log("Player hits fr: " + enemiesHit);
    }
}