using UnityEngine;

public class Controller : MonoBehaviour
{
    private int direction;
    private float speed;

    private bool WASD;

    private Rigidbody2D paddle;

    private Collider2D wall_Top;
    private Collider2D wall_Down;

    private bool isUpPressed;
    private bool isDownPressed;

    void Start()
    {
        direction = 0;

        wall_Top = GameObject.Find("Wall_Top").GetComponent<Collider2D>();
        wall_Down = GameObject.Find("Wall_Down").GetComponent<Collider2D>();

        isUpPressed = false;
        isDownPressed = false;
    }

    void Update(){
        if (WASD)
        {
            CheckInputs("w","s");
        }
        else
        {
            CheckInputs("up", "down");
        }        

        if (paddle.IsTouching(wall_Top))
        {
            direction = -1;
        }
        else if (paddle.IsTouching(wall_Down))
        {
            direction = 1;
        }

        paddle.linearVelocity = new Vector2(0, direction * speed);
    }

    private void CheckInputs(string up, string down){
        if (Input.GetKeyDown(up))
        {
            isUpPressed = true;
        }
        if (Input.GetKeyDown(down))
        {
            isDownPressed = true;
        }
        if (Input.GetKeyUp(up))
        {
            isUpPressed = false;
        }
        if (Input.GetKeyUp(down))
        {
            isDownPressed = false;
        }

        if (isUpPressed)
        {
            direction = 1;
        }
        else if (isDownPressed)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }
    }

    public void SetUpController(float paddleSpeed, bool isWASD, Rigidbody2D rb){
        speed = paddleSpeed;
        WASD = isWASD;
        paddle = rb;
    }
}
