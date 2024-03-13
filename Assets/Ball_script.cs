using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_script : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private bool canMove = true;
    private int isDead = 0;
    public AudioSource BackMusic, JumpSound, DeadSound, DeadMusic, Win;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);
            transform.Translate(movement * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space) && rb.IsTouchingLayers())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YouDead"))
        {
            canMove = false;
            playerRenderer.color = Color.red;
            
            if (isDead==0)
            {
                Debug.Log("You dead!");
                BackMusic.Stop();
                DeadSound.Play();
                DeadMusic.Play();
                isDead++;
            }
            
        }
        if (collision.gameObject.CompareTag("Flore"))
        {
            JumpSound.Play();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            canMove = false;
            BackMusic.Stop();
            Win.Play();
        }
    }
}
