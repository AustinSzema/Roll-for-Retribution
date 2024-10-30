using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPoop : MonoBehaviour
{

    private Sprite topSprite;
    private Sprite bottomSprite;

    [SerializeField] private Image topImage;
    [SerializeField] private Image bottomImage;

    
    private Sprite[] topSprites;
    private Sprite[] bottomSprites;

    
    [SerializeField] private Image[] topImages;
    [SerializeField] private Image[] bottomImages;


    
    [SerializeField] private WeaponShop weaponShop;

    [SerializeField] private int index = 0;
    
    public void Setup()
    {
        topSprite = topImage.sprite;
        bottomSprite = bottomImage.sprite;

        for(int i = 0; i < topImages.Length; i++)
        {
            topSprites[i] = topImages[i].sprite;
        }
        
        for(int i = 0; i < bottomImages.Length; i++)
        {
            bottomSprites[i] = bottomImages[i].sprite;
        }
    }

    private bool flipped = false;
    
    public void Swap()
    {
        flipped = !flipped;
        weaponShop.flippedList[index] = flipped;
        if (flipped)
        {
            topImage.sprite = bottomSprite;
            bottomImage.sprite = topSprite;

        }
        else
        {
            topImage.sprite = topSprite;
            bottomImage.sprite = bottomSprite;

        }

        weaponShop.hasSwappedWeapon = true;

    }
}
