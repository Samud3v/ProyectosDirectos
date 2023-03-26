using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    Vector2 input;
    PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        if(playerStats.Health <= 0) return;
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = input.normalized;
        foreach (Transform child in transform)
        {
            child.position -= new Vector3(input.x, input.y, 0) * playerStats.Speed * Time.deltaTime;
        }
    }
}
