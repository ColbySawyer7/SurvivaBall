using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5;
    private GameObject focalPoint;
    bool hasPowerup = false;
    private float powerUpStrength = 15;
    private float powerUpTime = 5;
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
            // Whoever is adding the jumping needs to edit the line below to the actual jump mechanism
            playerRb.AddForce(Vector3.up, ForceMode.Impulse);
        }
        else if (transform.position.y < -10 && !gameOver)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < -9 && !gameOver)
        {
            gameOver = true;
            playerAudio.PlayOneShot(fallOffStage, 1.0f);
        }
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + powerupOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !gameOver)
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Enemy") && !hasPowerup && !gameOver)
        {
            gameOver = true;
            playerAudio.PlayOneShot(popSound, 1.0f);
            Islands.GetComponent<AudioSource>().Stop();
        }
        else if (collision.gameObject.CompareTag("Islands"))
        {
            jumping = false;
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            playerAudio.PlayOneShot(finishSound, 1.0f);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}