using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksStart : MonoBehaviour
{
    public ParticleSystem ps;
    public bool GameWon = false;
    // Use this for initialization
    void Start()
    {
        SetGameWon();
    }   

    public AudioClip OnDeathSound;

    private int _numberOfParticles = 0;

    public void SetGameWon()
    {
        GameWon = true;
        ps.Play();
    }

    void Update()
    {
        if (!GameWon) return;
        if (!OnDeathSound) { return; }

        var count = ps.particleCount;
        if (count < _numberOfParticles)
        {
            GetComponent<PlayAudioAndDestroy>().PlayClip(OnDeathSound, false, 0.2f);
        }
        _numberOfParticles = count;
    }
}
