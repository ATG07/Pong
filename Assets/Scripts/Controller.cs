using UnityEngine;

public class Controller : MonoBehaviour
{
    private int direction;
    private float speed;

    private bool WASD;

    private Rigidbody2D paddle;

    private Collider2D wall_Top;
    private Collider2D wall_Down;

    void Start()
    {
        direction = 0;

        wall_Top = GameObject.Find("Wall_Top").GetComponent<Collider2D>();
        wall_Down = GameObject.Find("Wall_Down").GetComponent<Collider2D>();

        Debug.Log(wall_Top);
        Debug.Log(wall_Down);
    }

    void Update(){
        if (WASD)
        {
            if (Input.GetKeyDown("w"))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown("s"))
            {
                direction = -1;
            }
        }
        else
        {
            if (Input.GetKeyDown("up"))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown("down"))
            {
                direction = -1;
            }
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

    public void SetUpController(float paddleSpeed, bool isWASD, Rigidbody2D rb){
        speed = paddleSpeed;
        WASD = isWASD;
        paddle = rb;
    }
}
