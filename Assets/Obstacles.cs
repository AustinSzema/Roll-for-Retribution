using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleParents;
    private void Start()
    {
        foreach (GameObject obj in obstacleParents)
        {
            obj.SetActive(false);
        }

        if (GameManager.Instance.currentRound != 0)
        {
            int randomParent = Random.Range(0, obstacleParents.Length);
            obstacleParents[randomParent].SetActive(true);
        }
        else
        {
            obstacleParents[0].SetActive(true);
        }
    }
}
