using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    public enum BGM { easy, hard, busy };


    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int sfxChannels;
    int sfxIndex;
    AudioSource[] sfxPlayers;
    public enum SFX { flip, matchSuccess, matchFail };
    
    void Awake() {
        Init();
    }

    void Init() {
        // 배경음
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // 효과음
        GameObject sfxObject = new GameObject("SfxPlayer");
        bgmObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];
        for (int i = 0; i < sfxChannels; i++) {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        } 
    }
    public void ChangeBGM(BGM bgm) {
        if(bgmPlayer.isPlaying)
            bgmPlayer.Stop();
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.Play();
    }
    public void PlaySFX(SFX sfx) {
        for(int i = 0; i < sfxChannels; i++) {
            int loopIndex = (i + sfxIndex) % sfxChannels;

            if(sfxPlayers[loopIndex].isPlaying)
                continue;

            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
