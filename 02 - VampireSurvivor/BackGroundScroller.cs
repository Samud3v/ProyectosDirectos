using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    public float speed = 0.5f;
    public float speedMultiplier = 0.1f;
    [SerializeField] private Renderer rend;

    PlayerStats playerStats;
    Vector2 input;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        speed = playerStats.Speed;
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = input.normalized;
        rend.material.mainTextureOffset += input * speed * speedMultiplier * Time.deltaTime;
    }
}
