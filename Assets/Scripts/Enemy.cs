using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Config Params
    [Header("Enemy Settings")]
    [SerializeField] int health = 100;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float deathVFXDuration = 1f;
    [SerializeField] AudioClip deathSFX;
    [Range(0, 1)] [SerializeField] float deathSFXVolume = 1f;
    [SerializeField] int enemyKillPoints = 69;

    [Header("Enemy Projectile Settings")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.4f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float bulletSpeed = -4.5f;
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] AudioClip bulletSFX;
    [Range(0, 1)] [SerializeField] float bulletSFXVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        enemyBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        TriggerFireSFX();
    }

    private void TriggerFireSFX()
    {
        AudioSource.PlayClipAtPoint(bulletSFX, Camera.main.transform.position, bulletSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D bullet)
    {
        DamageDealer damageDealer = bullet.gameObject.GetComponent<DamageDealer>();
        if (damageDealer == null) { return; }
        DestroyEnemy(damageDealer);
    }

    private void DestroyEnemy(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<GameSession>().ScoreAdder(enemyKillPoints);
            TriggerVFX();
            TriggerSFX();
            Destroy(gameObject);
        }
    }

    private void TriggerSFX()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    private void TriggerVFX()
    {
        GameObject deathAnimation = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(deathAnimation, deathVFXDuration);
    }
}
