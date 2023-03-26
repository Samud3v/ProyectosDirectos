using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    // move sprite slowly up and down on its local Y axis

    public float speed = 1.0f;
    public float distance = 1.0f;

    // get player location so the sprite looks at the player
    public Transform player;

    public float initalHeight = 0.0f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        initalHeight = transform.localPosition.y;
    }

    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.y = (Mathf.Sin(Time.time * speed) * distance) + initalHeight;
        transform.localPosition = pos;

        // look at the player on the Y axis
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        transform.rotation = rotation;
    }
}
