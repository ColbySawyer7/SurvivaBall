using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    public Vector3 offset;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameOver = !GameObject.Find("Player");
        if(!gameOver){
            cam.position = player.position + offset;
        }
    }
}
