using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
   [SerializeField] private bool canLoadScene = true;

  
    public void Click(string PathfindingScene)
    {
        Debug.Log("I clicked a button");
        LoadScene(PathfindingScene);
    }

    private void LoadScene(string PathfindingScene)
    {
        if (canLoadScene)
        {
            SceneManager.LoadScene(PathfindingScene);
        }
    }

}
