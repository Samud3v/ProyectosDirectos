using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector2 direction;
    float speed = 2;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = GameObject.Find("Player").transform.position - transform.position;
        direction = direction.normalized;
        speed = Random.Range(1f, 10f);
        rb.velocity = direction * speed;
    }

    void Update(){
        transform.Rotate(0, 0, speed * 10 * Time.deltaTime);
    }
}
