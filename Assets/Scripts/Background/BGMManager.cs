using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip StageClip;

    public AudioSource bgmManagerAudio;
    
    void Awake()
    {
        bgmManagerAudio = GetComponent<AudioSource>();
        
        bgmManagerAudio.clip = StageClip;
        bgmManagerAudio.loop = true;
        bgmManagerAudio.playOnAwake = true;
        bgmManagerAudio.volume = 0.5f;
        
        bgmManagerAudio.Play();
    }
}
