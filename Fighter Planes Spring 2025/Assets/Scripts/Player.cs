using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float playerSpeed;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip bulletSound;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private GameObject shield;

    private float horizontalScreenLimit = 9.5f;
    private float xMin = -3.5f;
    private float xMax = 0f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    public int lives;

    private bool isShielded;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        gameManager.ChangeLivesText(lives);
    }

    void Update()
    {
        Movement();
        Shooting();
    }


    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            gameManager.PlaySound(bulletSound, 0.5f);
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //Player leaves the screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //stop player from leaving vertical bounds
        float clampedY = Mathf.Clamp(transform.position.y, xMin, xMax);

        transform.position = new Vector2(transform.position.x, clampedY);

    }

    public void LoseALife()
    {
        if(!isShielded)
        {
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        }
        else
        {
            gameManager.PlaySound(powerdownSound, 0.25f);
            shield.SetActive(false);
            gameManager.ManagePowerupText(0);
        }
    }


    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(powerupSound, 0.5f);
            gameManager.ManagePowerupText(4);
            shield.SetActive(true);
            isShielded = true;
        }
    }
}

