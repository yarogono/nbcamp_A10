using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip bgMusic;
    public AudioClip match;
    public AudioClip flip;
    public AudioSource bgSource;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        bgSource.clip = bgMusic;
        bgSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MatchSound() {
        audioSource.PlayOneShot(match);
    }
    public void FlipSound() {
        audioSource.PlayOneShot(flip);
    }
}
