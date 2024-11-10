using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public AudioClip successButtonSound;
    public AudioClip failButtonSound;
    public AudioClip discardSound;
    public AudioClip attackSound;
    public AudioClip moveSound;

    public AudioSource effectSource;
    public AudioSource bgmSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
        PlayBGM(bgmClip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlayEffect(string clipName)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip != null)
        {
            effectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Clip {clipName} not found in AudioManager.");
        }
    }

    private AudioClip GetClipByName(string clipName)
    {
        switch (clipName)
        {
            case "successButton": return successButtonSound;
            case "failButton": return failButtonSound;
            case "discard" : return discardSound;
            case "attack": return attackSound;
            case "move": return moveSound;
            default: return null;
        }
    }
}
