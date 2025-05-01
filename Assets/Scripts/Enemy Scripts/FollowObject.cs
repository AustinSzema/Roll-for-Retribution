using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform follower;

    private void Update()
    {
        follower.position = target.position;
    }
}
