using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // direcction
    Vector2 direction;

    // speed
    float speed = 1f;

    // rigidbody
    Rigidbody2D rb;

    void Start()
    {
        // random direction
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        // random speed
        speed = Random.Range(1f, 10f);

        // get rigidbody
        rb = GetComponent<Rigidbody2D>();

        // move
        rb.velocity = direction * speed;
    }
    void Update()
    {
        // rotate
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
