using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int health = 200;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetewwnShots = 2f;
    [SerializeField] int pointsForKilling = 10;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionTime = 0.5f;
    [Header("SFX")]
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] float laserVolume = 0.5f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetewwnShots);
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
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetewwnShots);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<GameState>().AddToScore(pointsForKilling);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
            Destroy(gameObject);
            GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(explosion, explosionTime);
        }
    }

}
