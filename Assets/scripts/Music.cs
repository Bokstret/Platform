using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public List<AudioClip> musicList;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("MusicMuted") == 1)
        {
            GetComponent<AudioSource>().mute = true;
        }
        DontDestroyOnLoad(this.gameObject);
        DJ();
    }


    void DJ()
    {
        int trackNum = PlayerPrefs.GetInt("LastTrack");
        trackNum %= musicList.Count;
        AudioSource player = this.gameObject.GetComponent<AudioSource>();
        player.clip = musicList[trackNum];
        player.Play();
        trackNum++;
        PlayerPrefs.SetInt("LastTrack", trackNum);
        Invoke("DJ", player.clip.length);
    }
}
