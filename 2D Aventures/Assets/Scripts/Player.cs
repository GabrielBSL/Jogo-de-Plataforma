using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public Transform upCol;
    public Transform downCol;
    public LayerMask layer;

    private Rigidbody2D rig;
    private Animator anim;

    private bool isJumping;
    private bool doubleJumping;
    private bool isFacingWall;
    private bool isBlowing;

    private bool collidingWall;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        collidingWall = Physics2D.Linecast(upCol.position, downCol.position, layer);
        Move();
        Jump();
        
    }

    void Move()
    {
        if (!collidingWall)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * speed;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        else
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isBlowing)
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                anim.SetBool("jump", true);
            }
            else if (doubleJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce + (rig.velocity.y * -1)), ForceMode2D.Impulse);
                doubleJumping = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8 && !isFacingWall)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        else if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
            doubleJumping = true;
        }
    }
/*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            isBlowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            isBlowing = false;
        }
    }
*/
}
