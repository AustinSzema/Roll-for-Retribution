using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{

    [SerializeField] private GameObject oppositeWall;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "MovingPlatform":
                if (other.gameObject.GetComponent<MovingPlatform>().movingRight)
                {
                    other.gameObject.transform.position = new Vector3(oppositeWall.transform.position.x + other.gameObject.transform.localScale.x / 2f + 1f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                }
                else
                {
                    other.gameObject.transform.position = new Vector3(oppositeWall.transform.position.x - other.gameObject.transform.localScale.x / 2f - 1f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                }

                break;

            case "Player":

                break;



        }
        
    }
}
