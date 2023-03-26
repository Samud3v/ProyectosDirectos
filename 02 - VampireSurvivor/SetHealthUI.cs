using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthUI : MonoBehaviour
{
    PlayerStats playerStats;
    Slider slider;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.maxValue = playerStats.MaxHealth;
        slider.value = playerStats.Health;
    }
}
