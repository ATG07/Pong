using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    private GameObject sceneManager;
    private MenuManager menuManager;

    void Awake(){
        sceneManager = GameObject.Find("SceneManager");
        menuManager = sceneManager.GetComponent<MenuManager>();
    }

    public void GoBack()
    {
        menuManager.BackToMainMenu();
    }
    
    public void DoNothingButton()
    {
        menuManager.DoNothingButton();
    }
}
