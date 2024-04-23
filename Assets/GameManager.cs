using System.Collections;
using System.Collections.Generic;
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

    // Optional: Add GameManager properties and methods here

    // Ensure GameManager instance is not destroyed when loading new scenes
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
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
    [Header("Player")]
    [Tooltip("The position of the player in the game world.")]
    public Vector3 playerPosition;
    [Tooltip("Indicates whether the player is currently grounded.")]
    public bool playerIsGrounded;
    [Tooltip("Indicates whether the player is currently flying.")]
    public bool playerIsFlying;
    [Tooltip("The maximum health of the player.")]
    public int playerHealth;
    [Tooltip("The current health of the player.")]
    public int playerCurrentHealth;

    // Game State Fields
    [Header("Game State")]
    [Tooltip("The index of the scene for the overworld dimension.")]
    public int overworldDimensionSceneIndex;
    [Tooltip("Indicates whether the player is out of fuel.")]
    public bool outOfFuel;
    [Tooltip("The count of enemies killed by the player.")]
    public int killCount;
    [Tooltip("Indicates whether the game is currently paused.")]
    public bool gameIsPaused;

    // Enemy Fields
    [Header("Enemies")]
    [Tooltip("The count of enemies currently in the game.")]
    public int enemyCount;
    [Tooltip("Indicates whether the player is currently holding a demon.")]
    public bool demonInHand;
    [Tooltip("Indicates whether demons are being pulled in by the player.")]
    public bool pullingInDemons;

    // Weapon Fields
    [Header("Weapons")]
    [Tooltip("The type of active shot the player currently has.")]
    public ActiveShotType activeShot;
    [Tooltip("The position of the player's hand.")]
    public Vector3 handPosition;

    // Flight Fields
    [Header("Flight")]
    [Tooltip("The duration of the player's flight.")]
    public float flightDuration;

    public enum ActiveShotType
    {
        Shotgun,
        Rocket,
        Spray,
        Beam
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
