using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    
    public static PlayerHealth instance;

    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip deathClip;

    
    
    private void Awake()
    {
        instance = this;
    }
    
    
    
    public int maxHealth = 10;
    public int currentHealth { get; private set; }
    private bool isDead = false;

    
    
   void Start()
   {
       currentHealth = maxHealth;
       //Debug.Log("Player Health Initialized: " + currentHealth);

   }



   public void TakeDamage(int damage)
   {
       if (isDead) return; // prevent taking damage after death


       currentHealth -= damage;
       currentHealth = Mathf.Max(currentHealth, 0); // prevent health from going below 0


       //Debug.Log("Player took " + damage + " damage! Current Health: " + currentHealth);

        PlaySound(hitClip);
        CameraShake.Instance.ShakeCamera(100f, 0.5f);

       if (currentHealth <= 0)
       {
           Die();
       }
   }


   void Die()
   {
       if (isDead) return; 


       isDead = true;
       //Debug.Log("Player Died! Game Over.");

       PlaySound(deathClip);
       
       LevelManager.instance.GameOver();


       // restart the game after a few seconds
       // Invoke("RestartGame", 3f);
   }

   private void PlaySound(AudioClip clip)
   {
        if (clip == null) return;

        GameObject tempAudio = new GameObject("TempPlayerSound");
        AudioSource source = tempAudio.AddComponent<AudioSource>();

        source.clip = clip;
        source.spatialBlend = 0f;
        source.Play();

        Destroy(tempAudio, clip.length);
   }


  
   void RestartGame()
   {
       Time.timeScale = 1f;
       UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
   }
}
