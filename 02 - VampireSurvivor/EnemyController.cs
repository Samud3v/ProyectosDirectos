using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    int enemyDamage = 10;
    [SerializeField]
    float enemyLife = 10;

    [SerializeField]
    GameObject destroyedMetParticle;

    [SerializeField]
    AudioSource hitSFX;
    [SerializeField]
    AudioSource destroyedSFX;

    PlayerStats playerStats;
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        enemyLife = Random.Range(5, 20);
        transform.localScale = new Vector3(enemyLife / 2, enemyLife / 2, enemyLife / 2);
        enemyDamage = ((int)((enemyLife / 2) * playerStats.Level));
    }

    public void TakeDamage(float damage)
    {
        enemyLife -= damage;
        if (enemyLife <= 0)
        {
            playerStats.CurrentScore += 1;
            destroyedSFX.Play();
            destroyedSFX.transform.parent = null;
            Destroy(destroyedSFX.gameObject, destroyedSFX.clip.length);
            GameObject particles = Instantiate(destroyedMetParticle, transform.position, destroyedMetParticle.transform.rotation, transform.parent);
            particles.transform.localScale = transform.localScale / 10;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Jugador")
        {
            hitSFX.Play();
            hitSFX.transform.parent = null;
            Destroy(hitSFX.gameObject, hitSFX.clip.length);
            playerStats.AddHealth(-enemyDamage);
            GameObject particles = Instantiate(destroyedMetParticle, transform.position, destroyedMetParticle.transform.rotation, transform.parent);
            particles.transform.localScale = transform.localScale / 10;
            Destroy(gameObject);
        }
    }
}
