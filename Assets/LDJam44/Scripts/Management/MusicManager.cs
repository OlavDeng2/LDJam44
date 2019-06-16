using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [Header("References")]
    public AudioClip[] gameMusic;


    [Header("Data")]
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!musicSource.isPlaying)
        {
            AudioClip audioToPlay = gameMusic[Random.Range(0, gameMusic.Length)];
            musicSource.clip = audioToPlay;
            musicSource.Play();
        }
    }

}
