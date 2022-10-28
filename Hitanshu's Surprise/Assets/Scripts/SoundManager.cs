using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] SongListClips;
    AudioSource audioSource;
    int currentSong;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSong = (int)(Random.Range(0, SongListClips.Length));
        audioSource.PlayOneShot(SongListClips[currentSong]);
        Invoke("PlayNextSong", SongListClips[currentSong].length);
    }

    void PlayNextSong()
    {
        int i = (int)(Random.Range(0, SongListClips.Length));
        while(i == currentSong)
        {
            i = (int)(Random.Range(0, SongListClips.Length));
        }
        currentSong = i;
        audioSource.PlayOneShot(SongListClips[i]);
        Invoke("PlayNextSong", SongListClips[i].length);
    }
}
