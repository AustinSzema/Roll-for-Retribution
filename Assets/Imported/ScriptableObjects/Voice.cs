using UnityEngine;

[CreateAssetMenu(menuName = "Voice", fileName = "New Voice")]
public class Voice : ScriptableObject
{
    [SerializeField] public AudioClip[] VoiceActing;

}
