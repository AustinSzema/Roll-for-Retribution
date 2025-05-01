using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateEnvironmentalObstacles : MonoBehaviour
{
    [SerializeField] private GameObject _pokerTable;
    [SerializeField] private GameObject _pokerChip;
    [SerializeField] private GameObject _playingCard;
    [SerializeField] private GameObject _whiskeyGlass;
    [SerializeField] private GameObject _martiniGlass;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObsctale(_pokerChip, 1000, 3.4f);
        SpawnObsctale(_playingCard, 52, 3.3f);
        SpawnObsctale(_whiskeyGlass, 10, 6.5f);
        SpawnObsctale(_martiniGlass, 14, 11f);
    }

    private void SpawnObsctale(GameObject obstacle, int count, float yPosition)
    {
        float xPos = _pokerTable.transform.localScale.x / 2f;
        float yPos = yPosition;
        float zPos = _pokerTable.transform.localScale.z / 2f;
        for (int i = 0; i < count; i++)
        {
            GameObject newObstacle = Instantiate(obstacle,
                transform.position + new Vector3(Random.Range(-xPos, xPos), yPos, Random.Range(-zPos, zPos)),
                Quaternion.identity);
            newObstacle.transform.parent = transform;
        }
    }
}