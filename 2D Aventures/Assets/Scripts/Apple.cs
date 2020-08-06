using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    public GameObject collected;
    public int score;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sr.enabled = false;
            cc.enabled = false;

            collected.SetActive(true);
            
            GameController.instance.UpdateScore(score);

            Destroy(gameObject, 0.25f);
        }
    }
}
