using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
