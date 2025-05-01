using UnityEngine;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{
   public GameObject enemyPrefab;
   public Transform player;
   public Transform[] spawnPoints; // Array of fixed spawn points
   public float spawnInterval = 10f; // Increased cooldown
   public int maxEnemies = 10; // Limit to 10 active enemies
   private int currentEnemyCount = 0;


   void Start()
   {
       if (!player)
       {
           player = PlayerHealth.instance.transform;
       }
       InvokeRepeating(nameof(SpawnEnemy), 0.25f, spawnInterval); // Start spawning after 5 sec
   }


   void SpawnEnemy()
   {
       //Debug.Log("Checking before spawn: Current enemies = " + currentEnemyCount);


       // If we already have max enemies, stop spawning
       if (currentEnemyCount >= maxEnemies)
       {
           //Debug.Log("Max enemies reached: " + currentEnemyCount + ". No more spawning.");
           return;
       }


       if (spawnPoints.Length == 0)
       {
           Debug.LogError("No spawn points assigned!");
           return;
       }


       // random spawn point from the array
       Transform chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];


       // spawn enemy at the chosen spawn point
       GameObject newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
       currentEnemyCount++;


       //Debug.Log("Enemy Spawned at: " + chosenSpawnPoint.name + " | Total Enemies: " + currentEnemyCount);


       // remove enemies when they die
       Enemy enemy = newEnemy.GetComponent<Enemy>();
       if (enemy != null)
       {
           enemy.OnEnemyDeath += RemoveEnemyFromList;
       }
   }


   void RemoveEnemyFromList(GameObject enemy)
   {
       currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1); // count never goes negative
       //Debug.Log("Enemy Removed! Total Remaining: " + currentEnemyCount);
   }
}
