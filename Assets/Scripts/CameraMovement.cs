using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    

    public float cameraSpeed = 0.15f;
    public Vector3 camVel;

    private PlayerController playerController;


    private void Start()
    {
        GameObject bg = transform.GetChild(3).gameObject;
        bg.GetComponent<SpriteRenderer>().color
            = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));

    }

    public PlayerController PlayerController { get { 
            if(playerController == null)
                playerController = FindObjectOfType<PlayerController>();
            return playerController;
        } 
    }

    void Update()
    {
        if (PlayerController.CanMove)
        {
            transform.position += Vector3.forward * cameraSpeed ;

        }
        camVel = Vector3.forward * cameraSpeed;

    }




}
