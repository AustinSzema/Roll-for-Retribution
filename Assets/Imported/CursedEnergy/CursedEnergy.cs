using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedEnergy : MonoBehaviour
{
    [SerializeField] private int _cursedEnergy = 0;
    [SerializeField] private ParticleSystem _cursedEnergyParticles;

    void Update()
    {
        _cursedEnergy++;
        var emissionModule = _cursedEnergyParticles.emission;
        emissionModule.rateOverTime = _cursedEnergy; 
    }
    

}
