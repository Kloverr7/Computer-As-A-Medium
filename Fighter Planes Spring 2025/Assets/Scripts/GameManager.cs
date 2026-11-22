using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject coinPrefab;
    public int score;
    private AudioSource audioSource;
    [SerializeField] private TMP_Text scoreText;
    
    public GameObject playerPrefab;
    public GameObject cloudPrefab;

    public TextMeshProUGUI livesText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    // Start is called before the first frame update
    //making changes
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 1, 7);
        InvokeRepeating("CreateCoin", 1, 5);
    }

   // Update is called once per frame
// Updates the UI to display the player's current score
void Update()
{
   scoreText.text = "Score: " + score;
}
    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }

    public void PlaySound(AudioClip sound, float vol)
    {
        audioSource.PlayOneShot(sound, vol);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }
}

