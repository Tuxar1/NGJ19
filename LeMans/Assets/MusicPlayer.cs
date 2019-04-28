using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> musicTracks;
    public AudioSource audioSource;
    
    void Start()
    {
    }

    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            putOnRandomTrack();
        }
    }

    private void putOnRandomTrack()
    {
        int trackNum = Random.Range(0, musicTracks.Count);
        audioSource.clip = musicTracks[trackNum];
        audioSource.Play();
    }
}
