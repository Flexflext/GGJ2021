using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/// <summary>
/// Manages all Sounds in the game and plays them when called
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // AudioManager instance
    [Tooltip("Array of all Sounds.")]
    public Sound[] sounds; // Array of Sounds

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        // for every Sound in the SoundsArray a AudioSource is added to the AudioManager
        foreach (Sound s in sounds)
        {
            if (s == null) // That array index is empty
                return;

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }


    }

    /// <summary>
    /// Plays Sound, Sound-ScriptableObject parameter
    /// </summary>
    /// <param name="_sound"></param>
    public void PlaySound(Sound _sound)
    {
        Sound s = Array.Find(sounds, sound => sound == _sound); // finds the sound that should be played in the sounds array

        if (s == null)
        {
            if(_sound == null)
            {
                Debug.Log("No Sound given");
                return;
            }
            Debug.Log("Sound '" + _sound.name + "' not found");
            return;
        }
        else if (s.source == null)
        {
            Debug.Log("No Sound source");
            return;
        }
        s.source.Play();
    }

    /// <summary>
    /// Plays Sound, string parameter
    /// </summary>
    /// <param name="name"></param>
    //public void PlaySound(string name)
    //{
    //    Sound s = Array.Find(sounds, sound => sound.name == name);
    //    if(s == null)
    //    {
    //        Debug.Log("Sound '" + name + "' not found");
    //        return;
    //    }
    //    else if(s.source == null)
    //    {
    //        Debug.Log("No Sound source");
    //        return;
    //    }
    //    s.source.Play();
    //}


}
