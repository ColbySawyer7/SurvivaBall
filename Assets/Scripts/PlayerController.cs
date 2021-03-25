using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float jump;
    private GameObject focalPoint;
    /*bool hasPowerup = false;
    private float powerUpStrength = 15;
    private float powerUpTime = 5;*/
    public GameObject powerupIndicator;
    private Vector3 powerupOffset;
    private AudioSource playerAudio;
    public AudioClip popSound;
    public AudioClip powerupSound;
    public AudioClip jumpSound;
    public AudioClip fallOffStage;
    public AudioClip finishSound;
    public GameObject Islands;
    public GameObject finish;
    private bool jumping = false;
    private bool gameOver = false;
    private bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        powerupOffset = powerupIndicator.transform.position;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !jumping)
        {
            jumping = true;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
        else if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < -9)
        {
            gameOver = true;
            playerAudio.PlayOneShot(fallOffStage, 1.0f);
            Islands.GetComponent<AudioSource>().Stop();
        }
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        playerRb.AddForce(focalPoint.transform.right * sideInput * speed);
        powerupIndicator.transform.position = transform.position + powerupOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !gameOver)
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            //hasPowerup = true;
            //StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            playerAudio.PlayOneShot(popSound, 1.0f);
            Islands.GetComponent<AudioSource>().Stop();
            Destroy(gameObject);
            gameOver = true;
        }
        else if (collision.gameObject.CompareTag("Islands"))
        {
            jumping = false;
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            playerAudio.PlayOneShot(finishSound, 1.0f);
            gameOver = true; 
        }
        else if (collision.gameObject.CompareTag("Enemy") && !hasPowerup)
        {
            gameOver = true;
            playerAudio.PlayOneShot(popSound, 1.0f);
            Islands.GetComponent<AudioSource>().Stop();
        }
    }

    /*IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }*/
}