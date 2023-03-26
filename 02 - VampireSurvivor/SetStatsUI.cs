using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStatsUI : MonoBehaviour
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
        text.text = "Level:\t\t\t\t\t\t\t\t\t"+ Mathf.Floor(playerStats.Level) + "\n" + 
                    "Firing rate:\t\t\t\t\t\t\t"+ playerStats.Strength + "\n" + 
                    "Movility:\t\t\t\t\t\t\t\t"+ playerStats.Speed.ToString("0.00") + "\n" + 
                    "Record time:\t\t\t\t\t\t"+ playerStats.RecordTime.ToString("0.00") + "\n" +
                    "Current time:\t\t\t\t\t\t"+ playerStats.CurrentTime.ToString("0.00") + "\n" + 
                    "Max. targets eliminated:\t\t"+ playerStats.RecordScore + "\n" + 
                    "Targets eliminated:\t\t\t\t" + playerStats.CurrentScore;
    }
}
