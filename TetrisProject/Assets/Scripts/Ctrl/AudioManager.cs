using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//声音播放
public class AudioManager : MonoBehaviour {

    private Ctrl ctrl;

    public AudioClip cursor;    //鼠标声音
    public AudioClip drop;      //下落声音
    public AudioClip control;   //摆放声音
    public AudioClip lineClear; //整行消除声音

    private AudioSource audioSource; //通过AudioSource播放声音

    private bool isMute = false;    //是否静音

    private void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayCursor()
    {
        PlayAudio(cursor);
    }

    public void PlayDrop()
    {
        PlayAudio(drop);
    }

    public void PlayControl()
    {
        PlayAudio(control);
    }

    public void PlayLineClear()
    {        
        
        PlayAudio(lineClear);
    }
    private void PlayAudio(AudioClip clip)
    {
        if (isMute)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuteActive(isMute);
        if(isMute == false)
        {
            PlayCursor();
        }
    }
}
