using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 lastMousePos;
    public float sensitivity = 0.16f, clampDelta = 42f;

    public float bounds = 5;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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

    }


}
