using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;
    public float speed = 1f;
    public GameObject panel;
    public GameObject gameOver_panel;
    public GameObject gamePause_panel;
    public static bool isGameFinished;

    public float jumpForce = 20;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;


    public bool isGrounded = false;

    void Update()
    {
        // Game pause 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause();
        }

        // checking for gameFinished
        if (isGameFinished) return;


        // code for player jump
        velocity += gravity * gravityScale * Time.deltaTime;

        if (isGrounded && velocity < 0)
        {
            velocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = jumpForce;
        }

        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);


        // player movement code
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


    public void gamePause()
    {
        isGameFinished = !isGameFinished;
        gamePause_panel.SetActive(isGameFinished);
    }

    public void resetPosition()
    {
        transform.position = new Vector3(-4.932122f, -2.970854f, 0);
        isGameFinished = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
