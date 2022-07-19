using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 lastMousePos;
    public float sensitivity = 0.16f, clampDelta = 42f;

    public float bounds = 5;


    private CameraMovement cameraMovement;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {


        //transform.position = new Vector3(Mathf.Clamp(transform.position.x,-bounds,bounds),transform.position.y,transform.position.z);
        ControlBounds();
        FollowCamera();
    }

    private void FollowCamera()
    {
        if(cameraMovement == null)
        {
            cameraMovement = FindObjectOfType<CameraMovement>();
        }
        transform.position += cameraMovement.camVel;

    }

    private void ControlBounds()
    {
        float x = transform.position.x;

        if(x > bounds)
        {
            transform.position = new Vector3(bounds, transform.position.y, transform.position.z);
        }
        else if(x < -bounds)
        {
            transform.position = new Vector3(-bounds, transform.position.y, transform.position.z);
        }

    }


    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 vector = lastMousePos - Input.mousePosition;
            lastMousePos = Input.mousePosition;
            vector = new Vector3(vector.x, 0, vector.y);

            Vector3 moveForce = Vector3.ClampMagnitude(vector,clampDelta);
            rigidbody.AddForce((-moveForce * sensitivity) - (rigidbody.velocity / 5f), ForceMode.VelocityChange);

        }

        rigidbody.velocity.Normalize();

    }


}
