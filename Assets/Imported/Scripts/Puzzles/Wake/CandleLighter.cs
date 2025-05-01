using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class CandleLighter : MonoBehaviour
{
    [SerializeField] private GameObject _candleFlameVFX;
    public ValueUpdated ValueUpdated;

    public void Light()
    {
        _candleFlameVFX.SetActive(true);
    }

    public void Deactivate()
    {
        ValueUpdated?.Invoke();
        gameObject.SetActive(false);
    }
}
