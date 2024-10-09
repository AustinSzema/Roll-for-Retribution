using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]

[RequireComponent(typeof(EnemyBase))]
public abstract class EnemyComponent : MonoBehaviour
{
    [SerializeField] protected EnemyBase enemyBase;
    
    [SerializeField] protected Rigidbody rb;
}
