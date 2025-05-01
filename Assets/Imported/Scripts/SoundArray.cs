using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Array", menuName = "Sound Array")]
public class SoundArray : ScriptableObject
{
    public AudioClip[] _sounds;
}
