using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;

    [SerializeField] private Sprite _shotgunCrosshairSprite;
    [SerializeField] private Sprite _rocketCrosshairSprite;
    
    [SerializeField] private ShotTypeSO _shotTypeSO;

    private void Update()
    {
        switch (_shotTypeSO.activeShotType)
        {
            case ShotTypeSO.ShotType.Shotgun:
                _crosshairImage.sprite = _shotgunCrosshairSprite;
                _crosshairImage.rectTransform.sizeDelta = new Vector2(75, 50); // bad dumb code
                break;
            case ShotTypeSO.ShotType.Rocket:
                _crosshairImage.sprite = _rocketCrosshairSprite;
                _crosshairImage.rectTransform.sizeDelta = new Vector2(50, 50); // bad dumb code
                break;
        }
    }


    //[SerializeField] private boolVariable _demonInHand;
    
    //private Camera _mainCam;

    /*private void Start()
    {
        _mainCam = Camera.main;
    }*/

    
    /*private void Update()
    {
        if (_demonInHand.Value)
        {
            // Define the size of the area to cover around the crosshair center
            float crosshairSize = 0.1f;

            // Cast rays within the defined area around the crosshair center
            for (float x = -crosshairSize; x <= crosshairSize; x += crosshairSize / 2)
            {
                for (float y = -crosshairSize; y <= crosshairSize; y += crosshairSize / 2)
                {
                    // Calculate viewport point with an offset from the center of the screen
                    Vector3 viewportPoint = new Vector3(0.5f + x, 0.5f + y, 0);

                    // Cast a ray from the calculated viewport point
                    Ray ray = _mainCam.ViewportPointToRay(viewportPoint);
                    RaycastHit hit;

                    // Check if the ray hits an object
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            // Change the color of the target image to green
                            _crosshair.localScale = 0.7f * Vector3.one;
                            return; // Exit the method if an enemy is found
                        }
                    }
                }
            }

            // Reset the color of the target image if no enemy is found
            _crosshair.localScale = Vector3.one;
        }
        else
        {
            // // Reset the color of the target image if no enemy is found
            // _crosshair.localScale = Vector3.one;
        }

    }*/

}