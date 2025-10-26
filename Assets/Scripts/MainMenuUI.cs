using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private GameObject sceneManager;
    private MenuManager menuManager;

    void Awake(){
        sceneManager = GameObject.Find("SceneManager");
        menuManager = sceneManager.GetComponent<MenuManager>();
    }

    public void GoToSettings(){
        menuManager.GoToSettings();
    }

    public void PvP(){
        StartCoroutine(menuManager.PlayerVPlayer());
    }

    public void AI(){
        StartCoroutine(menuManager.PlayerVAI());
    }
}
