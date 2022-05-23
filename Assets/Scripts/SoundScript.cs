using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] string name;
    [SerializeField] AudioClip clip;

    [SerializeField] [Range(0f, 3f)] float volume;
    [SerializeField] [Range(0.1f, 3f)] float pitch;
    [SerializeField] bool loop;

    AudioSource source;

    public string GetName()
    {
        return name;
    }
    public AudioClip GetClip()
    {
        return clip;
    }

    public float GetVolume()
    {
        return volume;
    }
    public float GetPitch()
    {
        return pitch;
    }

    public bool GetLoop()
    {
        return loop;
    }

    public AudioSource GetSource()
    {
        return source;
    }
    public void SetSource(AudioSource s)
    {
        source = s;
    }

}