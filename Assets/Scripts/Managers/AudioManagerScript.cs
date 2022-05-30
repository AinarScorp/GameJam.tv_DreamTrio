using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript Instance;

    [SerializeField] Sound[] sounds;
    List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    [SerializeField] float desiredVolume;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            s.GetSource().clip = s.GetClip();

            s.GetSource().loop = s.GetLoop();
            s.GetSource().volume = s.GetVolume();
            s.GetSource().pitch = s.GetPitch();

            audioSources.Add(s.GetSource());
        }

        Instance = this;
        PlayLoop("Music");
    }

    public void PlayLoop(string name)
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

    public void Play(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == name)
            {
                sound.GetSource().PlayOneShot(sound.GetClip());
                break;
            }
        }
    }

    public void PlayRandomPitch(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == name)
            {
                sound.GetSource().pitch = Random.Range(0.9f, 1.5f);
                break;
            }
        }

        Play(name);
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
    public IEnumerator FadeIn(string soundName)
    {
        AudioSource tempSound = null;

        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == soundName)
            {
                tempSound = sound.GetSource();
                break;
            }
        }

        float elapsedTime = 0;
        float currentVolume = 0;

        tempSound.volume = 0;

        while (fadeInTime > elapsedTime)
        {
            tempSound.volume = Mathf.Lerp(currentVolume, desiredVolume, (elapsedTime / fadeInTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tempSound.volume = desiredVolume;

        yield return null;
    }

    public IEnumerator FadeOut(string soundName)
    {
        AudioSource tempSound = null;

        foreach (Sound sound in sounds)
        {
            if (sound.GetName() == soundName)
            {
                tempSound = sound.GetSource();
                break;
            }
        }

        float elapsedTime = 0;
        float desiredVolume = 0;

        float currentVolume = tempSound.volume;

        while (fadeOutTime > elapsedTime)
        {
            tempSound.volume = Mathf.Lerp(currentVolume, desiredVolume, (elapsedTime / fadeOutTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tempSound.volume = desiredVolume;

        StopPlayingSound(soundName);

        yield return null;
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