using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject sceneManager;
    private MenuManager menuManager;

    void Awake(){
        sceneManager = GameObject.Find("SceneManager");
        menuManager = sceneManager.GetComponent<MenuManager>();
    }

    public void MainMenu(){
        StartCoroutine(menuManager.MainMenuScene());
    }
}
