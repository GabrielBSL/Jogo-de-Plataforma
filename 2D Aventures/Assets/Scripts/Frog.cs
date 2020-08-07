using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool colliding;

    public float speed;
    public float jumpForce;

    private bool playerDestroyed = false;

    public BoxCollider2D boxC;
    public CircleCollider2D circleC;

    public Transform upCol;
    public Transform downCol;
    public Transform headPoint;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        colliding = Physics2D.Linecast(upCol.position, downCol.position, layer);

        if(colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if (height > 0 && !playerDestroyed)
            {
                speed = 0f;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetTrigger("hit");

                boxC.enabled = false;
                circleC.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;

                Destroy(gameObject, 0.33f);
            }
            else
            {
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(collision.gameObject);
            }
        }
    }
}
