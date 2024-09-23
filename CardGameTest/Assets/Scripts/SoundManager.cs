using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFXWin;
    [SerializeField] AudioSource SFXLose;
    [SerializeField] AudioSource SFXCorrect;
    [SerializeField] AudioSource SFXWrong;
    [SerializeField] AudioSource SFXFlipping;



    public void PlayWin()
    {
        SFXWin.Play();
    }
    public void PlayFlipping()
    {
        SFXFlipping.Play();
    }
    public void PlayLose()
    {
        SFXLose.Play();
    }

    public void PlayCorrect()
    {
        SFXCorrect.Play();
    }

    public void PlayWrong()
    {
        SFXWrong.Play();
    }
}
