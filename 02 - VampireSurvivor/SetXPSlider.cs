using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetXPSlider : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerStats playerStats;
    Slider slider;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.maxValue = 1;
        slider.value = playerStats.Level - Mathf.Floor(playerStats.Level);
    }
}
