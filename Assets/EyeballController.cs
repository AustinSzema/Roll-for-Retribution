using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EyeballController : MonoBehaviour
{
    public CollectableList collectableList;

    public static EyeballController Instance;

    
    private List<EyeballSO> eyeballs = new List<EyeballSO>(5);

    [SerializeField] private List<Eyeball> eyeballsInHand = new List<Eyeball>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (EyeballSO coll in collectableList.list)
        {
            eyeballs.Add(coll);
        }

        if (eyeballsInHand[0] == null)
        {
            Debug.LogWarning("No eyeballs in hand in eyeball controller");
        }
        else
        {
            foreach (Eyeball eye in eyeballsInHand)
            {
                eye.gameObject.SetActive(false);
            }

            for (int i = 0; i < eyeballs.Count; i++)
            {
                eyeballsInHand[i].gameObject.SetActive(true);
                eyeballsInHand[i].SetMaterial(eyeballs[i].eyeMaterial);
            }
            
        }

        
    }

    private int maxEyesCount = 5;
    
    public void ModifyStats(PlayerController playerController)
    {
        if (eyeballs.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < eyeballs.Count; i++)
        {
            playerController.playerStats.moveSpeed += eyeballs[i].moveSpeed;
            playerController.playerStats.groundDrag += eyeballs[i].groundDrag;

            playerController.playerStats.jumpForce += eyeballs[i].jumpForce;
            playerController.playerStats.jumpCooldown += eyeballs[i].jumpCooldown;

            playerController.playerStats.airMultiplier += eyeballs[i].airMultiplier;
            playerController.playerStats.walkSpeed += eyeballs[i].walkSpeed;

            playerController.playerStats.sprintSpeed += eyeballs[i].sprintSpeed;
            //playerController.playerStats.gravityMultiplier += eyeballs[i].gravityMultiplier;
        }

    }
    
    public void ModifyStats(Weapon weapon)
    {
        if (eyeballs.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < eyeballs.Count; i++)
        {
            weapon.shootForce += eyeballs[i].shootForce;
            weapon.slamForce += eyeballs[i].slamForce;
            weapon.pullSpeed += eyeballs[i].pullSpeed;
            weapon.damage += eyeballs[i].damage;
            weapon.transform.localScale += new Vector3(eyeballs[i].scale, eyeballs[i].scale, eyeballs[i].scale);
        }

    }
    

}
