using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioAndDestroy : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip _audioClip;
    private bool isNowTriggered;
    private bool _isWithRandomPictch;

    public void PlayClip(AudioClip audioClip, bool isWithRandomPitch = false, float volumne = 1)
    {
        _audioClip = audioClip;
        _isWithRandomPictch = isWithRandomPitch;

        audioSource.clip = _audioClip;
        audioSource.Play();
        audioSource.pitch = (_isWithRandomPictch) ? 0.75f + Random.Range(0, 0.5f) : 1;
        audioSource.volume = volumne;
        isNowTriggered = true;
    }

    void Update()
    {
        if (isNowTriggered && audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
