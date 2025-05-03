using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictAudio : MonoBehaviour
{
    
    // The idea is to have it so that the audio transitions to the district specific track once you enter that district. Idk if im doing this right
    
    [SerializeField] private PlayDistrict[] districts;

    
    public void SilenceAll()
    {
        foreach (PlayDistrict district in districts)
        {
            district.getLoud = false; // Force all districts to stop increasing volume
        }
    }

}
