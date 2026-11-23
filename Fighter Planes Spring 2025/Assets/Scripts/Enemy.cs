using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public GameObject explosionPrefab;
    private GameManager gameManager;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private bool isDefault;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDefault)
        {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<Player>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PlaySound(explosionSound, 0.2f);
            Destroy(this.gameObject);
        } 
        else if(whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PlaySound(explosionSound, 0.2f);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}
