using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealth : MonoBehaviour
{
    PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Jugador")
        {
            playerStats.Health = playerStats.MaxHealth;
            Destroy(gameObject);
        }
    }
}
