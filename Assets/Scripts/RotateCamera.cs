using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    public Vector3 offset;
<<<<<<< Updated upstream
=======
    private bool gameOver;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
<<<<<<< Updated upstream
        cam.position = player.position + offset;
=======
        gameOver = !GameObject.Find("Player");
        if(!gameOver){
            cam.position = player.position + offset;
        }
>>>>>>> Stashed changes
    }
}
