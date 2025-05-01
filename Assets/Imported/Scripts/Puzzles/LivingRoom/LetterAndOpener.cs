using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class LetterAndOpener : MonoBehaviour
{
    public ValueUpdated ValueUpdated;

    public void Deactivate()
    {
        ValueUpdated?.Invoke();
        gameObject.SetActive(false);
    }
}
