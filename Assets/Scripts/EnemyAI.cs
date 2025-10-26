using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private int direction;
    private float speed;

    private Rigidbody2D paddle;
    private Transform ball;
    private Transform pos;

    void Start()
    {
        direction = 0;
        ball = GameObject.Find("Ball").GetComponent<Transform>();
        pos = this.GetComponent<Transform>();
    }

    void Update(){
        if (ball.position.y - pos.position.y > 1f){
            direction = 1;
        }
        else if (ball.position.y - pos.position.y < -1f){
            direction = -1;
        }
        paddle.linearVelocity = new Vector2(0, direction * speed);
    }

    public void SetUpAI(float paddleSpeed, Rigidbody2D rb){
        speed = paddleSpeed;
        paddle = rb;
    }
}
