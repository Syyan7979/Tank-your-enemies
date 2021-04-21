using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Config Params
    [Header("Player")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float padding = 0.75f;
    [SerializeField] int playerHealth = 200;
    [SerializeField] AudioClip playerDeathSFX;
    [Range(0, 1)] [SerializeField] float playerDeathSFXVolume = 1f;

    [Header("Player Shoot")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.2f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shootingSFX;
    [Range(0, 1)] [SerializeField] float bulletSFXVolume = 1f;

    // state changes
    float xMin, xMax, yMin, yMax;
    Coroutine fireCoroutine;
    SceneLoader fuck;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Fire();

    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContiuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void MovePlayer()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    IEnumerator FireContiuously()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position, bulletSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D objectCollision)
    {
        if (objectCollision.tag == "Enemy Bullet")
        {
            DamageDealer enemyBullet = objectCollision.gameObject.GetComponent<DamageDealer>();
            DestroyPLayer(enemyBullet);
        }
    }

    private void DestroyPLayer(DamageDealer enemyBullet)
    {
        playerHealth -= enemyBullet.GetDamage();
        enemyBullet.Hit();
        if (playerHealth <= 0)
        {
            FindObjectOfType<SceneLoader>().LoadGameOver();
            TriggerDeathSFX();
            Destroy(gameObject);
        }
    }

    private void TriggerDeathSFX()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, playerDeathSFXVolume);
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
