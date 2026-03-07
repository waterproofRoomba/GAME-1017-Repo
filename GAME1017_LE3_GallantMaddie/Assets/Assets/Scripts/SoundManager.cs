using UnityEngine;

public class SoundManager : Singleton<SoundManager>


{  
    
    
    
    [SerializeField] private AudioSource musicSource, sfxSource;



   

   
    public void ChangeMusicVolume(float newVolume) 
    {


        musicSource.volume = newVolume;
    }

    
    public void ChangeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }

   // public void PlaySound(AudioClip audioClip)
    //{

   // }

}

