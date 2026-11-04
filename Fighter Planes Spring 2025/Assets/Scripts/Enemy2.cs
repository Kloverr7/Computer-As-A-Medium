using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
private float xBound = 8.3f;
private bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -xBound)
        {
            movingRight = false;
            transform.Translate(0,-2f,0);
        }
        else if (transform.position.x >= xBound)
        {
            movingRight = true;
            transform.Translate(0,-2f,0);
        }
        if(movingRight == true)
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * 6f);
        else if(movingRight == false)
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 6f);

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
