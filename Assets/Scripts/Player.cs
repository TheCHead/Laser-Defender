using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int playerHealth = 500;
    [Header("Player Movement")]
    [SerializeField] float gameSpeed = 10f;
    [SerializeField] float xPadding = 0.5f;
    [SerializeField] float yPadding = 1f;
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.2f;
    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathVolume = 1f;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] float laserVolume = 1f;


    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

  

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (playerHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
            FindObjectOfType<SceneLoader>().LoadGameOverScene();
            Destroy(gameObject);
            
        }
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * gameSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * gameSpeed;
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newPosX, newPosY);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
