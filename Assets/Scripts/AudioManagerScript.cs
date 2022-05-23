using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    List<AudioSource> audioSources = new List<AudioSource>();

    public static AudioManagerScript Instance;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            s.GetSource().clip = s.GetClip();

            s.GetSource().loop = s.GetLoop();
            s.GetSource().volume = s.GetVolume();
            s.GetSource().pitch = s.GetPitch();

            s.GetSource().name = s.GetName();

            audioSources.Add(s.GetSource());
        }

        Instance = this;
    }

    public void Play(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == name)
            {
                sound.GetSource().Play();
                break;
            }
        }
    }

    public void StopPlayingSound(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == soundName)
            {
                sound.GetSource().Stop();
                break;
            }
        }
    }

    public void SpeedUp(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == soundName)
            {
                sound.GetSource().pitch = 1.2f;
                break;
            }
        }
    }

    public bool isPlaying(string soundName)
    {
        bool temp = false;

        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == soundName)
            {
                if (sound.GetSource().isPlaying)
                    temp = true;
                else
                    temp = false;
            }
        }

        return temp;
    }
}