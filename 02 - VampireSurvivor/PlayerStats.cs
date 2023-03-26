using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    ParticleSystem destroyedPlayerParticle;
    [SerializeField]
    List<GameObject> objectsToHide;
    [SerializeField]
    AudioSource audioSources;
    [SerializeField]
    int strength = 2;
    [SerializeField]
    float speed = 2;
    [SerializeField]
    float level = 1;
    [SerializeField]
    int health = 100;
    [SerializeField]
    int maxHealth = 100;
    [SerializeField]
    float currentTime = 0;
    [SerializeField]
    int currentScore = 0;
    [SerializeField]
    float recordTime = 0;
    [SerializeField]
    int recordScore = 0;

    bool isDead = false;

    // Getters and Setters
    public int Strength { get => strength; set => strength = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Level { get => level; set => level = value; }
    public int Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public float RecordTime { get => recordTime; set => recordTime = value; }
    public int RecordScore { get => recordScore; set => recordScore = value; }

    // a method to each value that adds a certain amount to it
    public void AddStrength(int amount)
    {
        strength += amount;
    }
    public void AddSpeed(float amount)
    {
        speed += amount;
    }
    public void AddLevel(float amount)
    {
        level += amount;
    }
    public void AddHealth(int amount)
    {
        health += amount;
    }
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    void Start()
    {
        // if there is a record time, it is loaded
        if (PlayerPrefs.HasKey("RecordTime"))
            recordTime = PlayerPrefs.GetFloat("RecordTime");
        // if there is a record score, it is loaded
        if (PlayerPrefs.HasKey("RecordScore"))
            recordScore = PlayerPrefs.GetInt("RecordScore");
    }

    void Update()
    {
        // if health is less than 0, player dies
        currentTime += Time.deltaTime;
        if (health <= 0 && !isDead)
        {
            if(currentTime > recordTime)
                PlayerPrefs.SetFloat("RecordTime", currentTime);
            if(currentScore > recordScore)
                PlayerPrefs.SetInt("RecordScore", currentScore);
            isDead = true;
            destroyedPlayerParticle.Play();
            GetComponent<Rigidbody2D>().simulated = false;
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
            Debug.Log("You died");
            audioSources.Play();
            Initiate.Fade("Menu", Color.black, 2f);
        }
    }
}
