using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Main References")]
    public Transform paddle_L;
    public Transform paddle_R;
    public Collider2D paddle_Left_Collider;
    public Collider2D paddle_Right_Collider;
    public Rigidbody2D paddle_L_rb;
    public Rigidbody2D paddle_R_rb;
    public Rigidbody2D ball;
    public GameObject VictoryScreen;
    public CameraShake camera;

    [Header("Wall References")]
    public Transform wall_Top;
    public Transform wall_Down;
    public Transform wall_Left;
    public Transform wall_Right;
    public Collider2D collider_Top;
    public Collider2D collider_Down;
    public Collider2D collider_Left;
    public Collider2D collider_Right;

    [Header("Animation Settings")]
    public float timeDelay;
    private float startTime2;
    public float timeDelay2;
    public float animationSpeed;
    public Renderer arrow_renderer;
    public Transform arrow;
    private bool animationOver;

    [Header("Game Settings")]
    public float ballSpeed;

    [Header("Particles")]
    public ParticleSystem arrow_PS;

    #region Private Variables

    private Vector2 direction;

    private Vector2 directionAnimation;
    private float startTime;
    private bool start;
    private int loops;

    private int lastPaddleCollision;
    private int lastWallCollision;

    private MenuManager sound;

    #endregion

    #region Meta Variables

    bool gameOver;

    #endregion
    
    void Start()
    {
        SetupGame();

        sound = GameObject.Find("SceneManager").GetComponent<MenuManager>();
    }

    void FixedUpdate()
    {
        if (gameOver){
            return;
        }

        if (Time.time - startTime < timeDelay){
            //Wait
        }

        else if (!start){
            StartAnimation();
            startTime2 = Time.time;
        }

        else if (Time.time - startTime2 < timeDelay2){
            //Wait
        }

        else if (!animationOver){
            arrow_PS.Play();
            arrow_renderer.enabled = false;


            ball.linearVelocity = ballSpeed * direction;
            animationOver = true;
        }

        else {
            HandleCollisions();
        }
    }

    void SetupGame(){
        gameOver = false;
        animationOver = false;

        lastPaddleCollision = 0;
        lastWallCollision = 0;

        startTime = Time.time;
        start = false;
        direction.y = Random.Range(-0.5f, 0.5f);
        direction.x = Mathf.Sign(Random.Range(-1f,1f)) * Mathf.Sqrt(1 - Mathf.Pow(direction.y,2));
        directionAnimation = new Vector2(1f,0f);

        ball.position = new Vector2(0,0);
        ball.linearVelocity = new Vector2(0,0);

        loops = (int)(Random.Range(2f, 5f));


        Camera cam = Camera.main;

        Vector3 bottomLeft  = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight    = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        paddle_L.position = bottomLeft + new Vector3(1, 2, 0);
        paddle_R.position = topRight - new Vector3(1, 2, 0);

        wall_Top.position = new Vector2(0f, topRight.y);
        wall_Down.position = new Vector2(0f, bottomLeft.y);
        wall_Left.position = new Vector2(bottomLeft.x, 0f);
        wall_Right.position = new Vector2(topRight.x, 0f);
    }

    void StartAnimation(){
        directionAnimation.x = Mathf.Cos(animationSpeed * (Time.time - startTime));
        directionAnimation.y = Mathf.Sin(animationSpeed * (Time.time - startTime));

        arrow.position = directionAnimation;

        if (Mathf.Abs(directionAnimation.x - direction.x) < 0.05f && Mathf.Abs(directionAnimation.y - direction.y) < 0.05f){
            loops--;
        }

        if (loops == 0){
            start = true;
        }
    }

    void HandleCollisions(){
        if (ball.IsTouching(collider_Top) && lastWallCollision != 1){
            ball.linearVelocity = new Vector2(ball.linearVelocity.x, -ball.linearVelocity.y);
            lastWallCollision = 1;
            camera.TriggerShake(0.1f, 0.1f);
            sound.CollisionSFX();
        }

        if (ball.IsTouching(collider_Down) && lastWallCollision != -1){
            ball.linearVelocity = new Vector2(ball.linearVelocity.x, -ball.linearVelocity.y);
            lastWallCollision = -1;
            camera.TriggerShake(0.1f, 0.1f);
            sound.CollisionSFX();
        }

        if (ball.IsTouching(collider_Left)){
            GameOver(false);
            camera.TriggerShake(0.5f, 0.8f);
        }

        if (ball.IsTouching(collider_Right)){
            GameOver(true);
            camera.TriggerShake(0.5f, 0.8f);
        }

        if (ball.IsTouching(paddle_Right_Collider) && lastPaddleCollision != 1){
            ball.linearVelocity = new Vector2(-ball.linearVelocity.x, ball.linearVelocity.y + paddle_R_rb.linearVelocity.y * 0.2f);
            lastPaddleCollision = 1;
            camera.TriggerShake(0.2f, 0.3f);
            sound.CollisionSFX();
        }

        if (ball.IsTouching(paddle_Left_Collider) && lastPaddleCollision != -1){
            ball.linearVelocity = new Vector2(-ball.linearVelocity.x, ball.linearVelocity.y + paddle_L_rb.linearVelocity.y * 0.2f);
            lastPaddleCollision = -1;
            camera.TriggerShake(0.2f, 0.3f);
            sound.CollisionSFX();
        }
    }

    void GameOver(bool leftWon){
        gameOver = true;

        sound.GameOverMusic();

        Instantiate(VictoryScreen, new Vector3(0,0,0), Quaternion.identity);
        TMP_Text winnerText = GameObject.Find("Winner").GetComponent<TMP_Text>();
        if (GameObject.Find("Paddle_R").GetComponent<EnemyAI>() != null){
            if(leftWon){
                winnerText.text = "Player Wins!";
            }
            else{
                winnerText.text = "AI Wins :(";
            }
        }
        else{
            if(leftWon){
                winnerText.text = "Player 1 Wins!";
            }
            else{
                winnerText.text = "Player 2 Wins!";
            }
        }

        ball.linearVelocity = new Vector2(0,0);
    }
}
