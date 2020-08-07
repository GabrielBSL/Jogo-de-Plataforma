using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public bool isMoving;
    public bool isVertical;
    public bool dirDest;
    public float speed;
    public float moveTime;

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (dirDest)
            {
                if (isVertical)
                {
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
            }
            else
            {
                if (isVertical)
                {
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
            }

            timer += Time.deltaTime;
            if (timer >= moveTime)
            {
                timer = 0;
                dirDest = !dirDest;
            }
        }
    }
}
