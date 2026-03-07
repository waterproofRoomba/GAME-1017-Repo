using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string m_sceneName;
    [SerializeField] private AudioClip m_buttonClip;
   public void ClickMe()
    {   
        
        //SoundManager.Instance.PlaySound(m_buttonClip);
        MenuManager.Instance.Click(m_sceneName);
        
    }

   
}
