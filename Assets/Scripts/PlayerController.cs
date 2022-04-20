using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rigidbody2d;
    public float speed = 1f;
    public GameObject panel;
    public GameObject gameOver_panel;
    [SerializeField]  public static bool isGameFinished;

    public GroundCheck groundCheck;
    public float jumpForce = 20;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;

    // Update is called once per frame
    void Update()
    {
        if (isGameFinished) return;

        velocity += gravity * gravityScale * Time.deltaTime;
        if (groundCheck.isGrounded && velocity < 0)
        {
            velocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = jumpForce;
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidbody2d.velocity = new Vector2(speed, 0f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            rigidbody2d.velocity = new Vector2(-speed, 0f);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            rigidbody2d.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            panel.SetActive(true);
            isGameFinished = true;
        }
        else if(other.tag == "Spike")
        {
            gameOver_panel.SetActive(true);
            isGameFinished = true;

        }
    }

    public void resetPosition()
    {
        transform.position = Vector3.zero;
        isGameFinished = false;
    }
}

//      else if (Input.GetAxis("Vertical") > 0)
//{
//    rigidbody2d.velocity = new Vector2(0f, speed);
//}
//        else if (Input.GetAxis("Vertical") < 0)
//{
//    rigidbody2d.velocity = new Vector2(0f, -speed);
//}