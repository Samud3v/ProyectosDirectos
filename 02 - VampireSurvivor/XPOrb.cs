using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    float xp = 1f;
    PlayerStats playerStats;
    [SerializeField] ParticleSystem particles;
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        xp = Random.Range(1f, 10f)/50f;
        var main = particles.main;
        main.startSize = xp * 5;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Jugador")
        {
            
            if(playerStats.Level + xp > Mathf.FloorToInt(playerStats.Level) + 1)
            {
                playerStats.AddSpeed(0.1f);
                playerStats.AddStrength(1);
                playerStats.AddHealth(10);
                playerStats.AddMaxHealth(10);
            }
            playerStats.AddLevel(xp);
            
            Destroy(gameObject);
        }
    }
}
