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


    private List<Sprite> topSprites = new List<Sprite>();
    private List<Sprite> bottomSprites = new List<Sprite>();

    [SerializeField] private List<WeaponPoop> otherPoops = new List<WeaponPoop>();
    
    [SerializeField] private List<Image> topImages = new List<Image>();
    [SerializeField] private List<Image> bottomImages = new List<Image>();


    
    [SerializeField] private WeaponShop weaponShop;

    [SerializeField] private int index = 0;
    
    public void Setup()
    {
        topSprite = topImage.sprite;
        bottomSprite = bottomImage.sprite;

        for(int i = 0; i < topImages.Count; i++)
        {
            topSprites.Add(topImages[i].sprite);
        }
        
        for(int i = 0; i < bottomImages.Count; i++)
        {
            bottomSprites.Add(bottomImages[i].sprite);
        }
    }

    public bool flipped = false;
    
    
    public void Swap()
    {
        if (topImage.sprite == bottomImage.sprite)
        {
            flipped = false;
        }
        else
        {
            flipped = !flipped;
        }
        weaponShop.flippedList[index] = flipped;
        for (int i = 0; i < weaponShop.flippedList.Count; i++)
        {
            if (i != index)
            {
                weaponShop.flippedList[i] = false;
            }
        }
        if (flipped)
        {
            
            for(int i = 0; i < topImages.Count; i++)
            {
                topImages[i].sprite = bottomSprites[i];
            }
        
            for(int i = 0; i < bottomImages.Count; i++)
            {
                bottomImages[i].sprite = topSprites[i];
            }
            
            topImage.sprite = bottomSprite;
            bottomImage.sprite = topSprite;

        }
        else
        {
            topImage.sprite = topSprite;
            bottomImage.sprite = bottomSprite;

        }
        
        weaponShop.hasSwappedWeapon = true;
        weaponShop.ShouldShowNextRoundButton();
    }

    public void Revert()
    {
        flipped = false;

        weaponShop.flippedList[index] = flipped;
        
        for (int i = 0; i < weaponShop.flippedList.Count; i++)
        {
            if (i == index)
            {
                weaponShop.flippedList[i] = false;
            }
        }

        topImage.sprite = topSprite;
        bottomImage.sprite = bottomSprite;
    }

}
