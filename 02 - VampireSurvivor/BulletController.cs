using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Transform target;
    float speed = 10f;
    [SerializeField] float damage = 1000f;
    public Transform enemies;
    float timer = 0f;
    float timerMax = 1f;

    [SerializeField]
    AudioSource shootSFX;

    PlayerStats playerStats;

    void Start(){
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        timer = timerMax;
    }

    void Update(){
        timerMax = 1f / playerStats.Strength;
        timer -= Time.deltaTime;
        if(timer <= 0 && target == null){
            target = enemies.GetChild(0);
            foreach (Transform enemy in enemies)
            {
                if (Vector3.Distance(GameObject.Find("Player").transform.position, enemy.position) < Vector3.Distance(GameObject.Find("Player").transform.position, target.position))
                {
                    target = enemy;
                }
            }
            if(!shootSFX.isPlaying) shootSFX.Play();
        }
        if(target != null){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
            transform.position = new Vector3(0, 0, 0);
            target = null;
            timer = timerMax;
        }
    }
}
