using UnityEngine;

public class SoundManager : MonoBehaviour
{  
    
    
    
    [SerializeField] private AudioSource musicSource, sfxSource;



    private void Awake()
    {
       
    }

   
    public void ChangeMusicVolume(float newVolume) 
    {


        musicSource.volume = newVolume;
    }

    
    public void ChangeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }

}

