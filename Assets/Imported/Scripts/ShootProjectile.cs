using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 50f;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform shootingPoint;

    [SerializeField] private Mesh[] Letters;

    [SerializeField] private string TextToShoot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (String.IsNullOrEmpty(TextToShoot) == false)
            {
                char letter = TextToShoot[0];

                int index = (int)char.ToUpper(letter) - 65;

                TextToShoot = TextToShoot.Substring(1);
                if (index > -1 && index < Letters.Length-1)
                {
                    GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().linearVelocity = shootingPoint.forward * projectileSpeed;
                    projectile.transform.forward = shootingPoint.transform.forward;
                    projectile.transform.Rotate(0f, 180f, 0f);
                    projectile.transform.position += new Vector3(0f, 0.5f, 0f);
                    projectile.GetComponent<MeshFilter>().mesh = Letters[index];
                }
            }
        }
    }
}






/*                int letterPos = 0;



                switch (TextToShoot[0].ToString().ToUpper())
                {
                    case "A":
                        letterPos = 0;
                        break;
                    case "B":
                        letterPos = 1;
                        break;
                    case "C":
                        letterPos = 2;
                        break;
                    case "D":
                        letterPos = 3;
                        break;
                    case "E":
                        letterPos = 4;
                        break;
                    case "F":
                        letterPos = 5;
                        break;
                    case "G":
                        letterPos = 6;
                        break;
                    case "H":
                        letterPos = 7;
                        break;
                    case "I":
                        letterPos = 8;
                        break;
                    case "J":
                        letterPos = 9;
                        break;
                    case "K":
                        letterPos = 10;
                        break;
                    case "L":
                        letterPos = 11;
                        break;
                    case "M":
                        letterPos = 12;
                        break;
                    case "N":
                        letterPos = 13;
                        break;
                    case "O":
                        letterPos = 14;
                        break;
                    case "P":
                        letterPos = 15;
                        break;
                    case "Q":
                        letterPos = 16;
                        break;
                    case "R":
                        letterPos = 17;
                        break;
                    case "S":
                        letterPos = 18;
                        break;
                    case "T":
                        letterPos = 19;
                        break;
                    case "U":
                        letterPos = 20;
                        break;
                    case "V":
                        letterPos = 21;
                        break;
                    case "W":
                        letterPos = 22;
                        break;
                    case "X":
                        letterPos = 23;
                        break;
                    case "Y":
                        letterPos = 24;
                        break;
                    case "Z":
                        letterPos = 25;
                        break;
                    default:
                        letterPos = 26;
                        break;
                }*/