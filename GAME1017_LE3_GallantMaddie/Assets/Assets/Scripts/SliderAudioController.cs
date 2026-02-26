using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

/*[RequireComponent(typeof(Slider))]

public class SliderAudioController : MonoBehaviour
{

    [SerializeField] private ESoundType soundType;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }


    private void Start()
    {
        ChangeSoundVolume(slider.value);
        
            

    }
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeSoundVolume); 
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeSoundVolume);
    }

    private void OnSliderValueChanged()
    {
        switch(soundType)
        {
            case ESoundType.Music:
                
                break;

            case ESoundType.SFX:
                break;
            
        }
    }

    private void ChangeSoundVolume(float newVolume)
    {
        switch(soundType)
        {  
            case ESoundType.Music:
                SoundManager.ChangeMusicVolume(newVolume);
                break;

            case ESoundType.SFX:
                SoundManager.ChangeSFXVolume(newVolume);
                break;
            
        }
    }

   

   
}
public enum ESoundType
    {
        Music,
        SFX,
        None
    }*/