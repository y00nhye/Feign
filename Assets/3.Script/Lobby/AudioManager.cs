using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance = null;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("[Audio Source]")]
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource SFX;

    [Header("[Audio Clip]")]
    [SerializeField] AudioClip LobbyBackground;
    [SerializeField] AudioClip MainBackground;

    [Header("[SFX Clip]")]
    [SerializeField] AudioClip Button;
    [SerializeField] AudioClip Kill;
    [SerializeField] AudioClip Medicine;
    [SerializeField] AudioClip Clean;
    [SerializeField] AudioClip Paint;
    [SerializeField] AudioClip Police;
    [SerializeField] AudioClip Open;


    public void LobbyBackgroundSound()
    {
        Music.Stop();
        Music.PlayOneShot(LobbyBackground);
    }
    public void MainBackgroundSound()
    {
        Music.Stop();
        Music.PlayOneShot(MainBackground);
    }
    public void BtnSound()
    {
        SFX.PlayOneShot(Button);
    }

    public void KillSound()
    {
        SFX.PlayOneShot(Kill);
    }
    public void MedicineSound()
    {
        SFX.PlayOneShot(Medicine);
    }
    public void CleanSound()
    {
        SFX.PlayOneShot(Clean);
    }
    public void PaintSound()
    {
        SFX.PlayOneShot(Paint);
    }
    public void PoliceSound()
    {
        SFX.PlayOneShot(Police);
    }

    public void OpenSound()
    {
        SFX.PlayOneShot(Open);
    }

    public void MusicVolume(float volume)
    {
        Music.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        SFX.volume = volume;
    }
}
