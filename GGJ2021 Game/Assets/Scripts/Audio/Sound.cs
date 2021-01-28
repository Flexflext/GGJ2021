using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// every Sound in the Game is a Sound-Object
/// </summary>
[System.Serializable] [CreateAssetMenu(fileName = "New Sound", menuName = "ScriptableObjects/Sound")]
public class Sound : ScriptableObject
{
    [Tooltip("The sound clip that will be played")]
    public AudioClip clip; // the wav file

    [Range(0,1)] [Tooltip("The volume with which the sound will be played")]
    public float volume; // the Sound volume
    [Range(.1f,3)] [Tooltip("The pitch with which the sound will be played")]
    public float pitch; // the Sound pitch

    [HideInInspector]
    public AudioSource source; // the AudioSource reference which will be added to the AudioManager
}
