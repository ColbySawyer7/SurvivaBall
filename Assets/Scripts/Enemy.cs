using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float speed = 3;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent <Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = !GameObject.Find("Player");
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!gameOver){
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
    
        //Debug.Log(player.transform.position);
        //Debug.Log(transform.position);
       // Debug.Log(lookDirection);
    }
}
