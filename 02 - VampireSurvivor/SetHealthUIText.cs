using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthUIText : MonoBehaviour
{
    PlayerStats playerStats;
    Text text;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = playerStats.Health + " / " + playerStats.MaxHealth;
    }
}
