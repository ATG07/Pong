using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Main_Menu_UI;
    public GameObject Settings_UI;

    public GameObject Loading_Screen;

    private GameObject Paddle_L;
    private GameObject Paddle_R;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip ButtonPress;
    public AudioClip Collision;
    public AudioClip GameOver;
    public AudioClip MainMenuTrack;
    public AudioClip GameSoundtrack;
    public AudioClip GameOverTrack;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Instantiate(Main_Menu_UI, new Vector3(0, 0, 0), Quaternion.identity);
        PlayMusic(MainMenuTrack);
    }

    void Start()
    {
    }

    public void GoToSettings(){
        PlaySFX(ButtonPress);
        DestroyPrevUI();
        Instantiate(Settings_UI, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void BackToMainMenu(){
        PlaySFX(ButtonPress);
        DestroyPrevUI();
        Instantiate(Main_Menu_UI, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void DestroyPrevUI(){
        PlaySFX(ButtonPress);
        Destroy(GameObject.Find("Main_Menu_UI(Clone)"));
        Destroy(GameObject.Find("Settings_UI(Clone)"));
    }

    public IEnumerator PlayerVPlayer(){
        PlaySFX(ButtonPress);
        PlayMusic(GameSoundtrack);

        Instantiate(Loading_Screen, new Vector3(0, 0, 0), Quaternion.identity);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game_Pong", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Main_Menu");

        Paddle_L = GameObject.Find("Paddle_L");
        Paddle_R = GameObject.Find("Paddle_R");

        Controller Controller_L = Paddle_L.AddComponent<Controller>();
        Controller_L.SetUpController(3f, true, Paddle_L.GetComponent<Rigidbody2D>());

        Controller Controller_R = Paddle_R.AddComponent<Controller>();
        Controller_R.SetUpController(3f, false, Paddle_R.GetComponent<Rigidbody2D>());
    }

    public IEnumerator PlayerVAI(){
        PlaySFX(ButtonPress);
        PlayMusic(GameSoundtrack);

        Instantiate(Loading_Screen, new Vector3(0, 0, 0), Quaternion.identity);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game_Pong", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Main_Menu");

        Paddle_L = GameObject.Find("Paddle_L");
        Paddle_R = GameObject.Find("Paddle_R");

        Controller Controller_L = Paddle_L.AddComponent<Controller>();
        Controller_L.SetUpController(3f, true, Paddle_L.GetComponent<Rigidbody2D>());

        EnemyAI Controller_R = Paddle_R.AddComponent<EnemyAI>();
        Controller_R.SetUpAI(3f, Paddle_R.GetComponent<Rigidbody2D>());
    }

    public IEnumerator MainMenuScene()
    {
        PlaySFX(ButtonPress);
        PlayMusic(MainMenuTrack);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Game_Pong");

        BackToMainMenu();
    }
    
    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void GameOverMusic()
    {
        PlaySFX(GameOver);
        PlayMusic(GameOverTrack);
    }

    public void CollisionSFX()
    {
        PlaySFX(Collision);
    }
    
    public void DoNothingButton()
    {
        PlaySFX(ButtonPress);
    }
}
