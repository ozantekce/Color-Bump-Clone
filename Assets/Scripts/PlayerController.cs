using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 lastMousePos;
    public float sensitivity = 0.16f, clampDelta = 42f;
    public float bounds = 5 ;

    private bool canMove;
    private bool gameOver;

    

    private CameraMovement cameraMovement;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Awake()
    {
        Application.targetFrameRate = 60;
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {

        ControlBounds();
        if (canMove)
        {
            FollowCamera();
        }


        if (!canMove&&!gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canMove = true;
                GameManager.Instance.RemoveUI();
            }
        }


        if(!canMove && gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        }

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

        if (canMove)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 vector = lastMousePos - Input.mousePosition;
                lastMousePos = Input.mousePosition;
                vector = new Vector3(vector.x, 0, vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
                rigidbody.AddForce((-moveForce * sensitivity) - (rigidbody.velocity / 5f), ForceMode.VelocityChange);
            }
        }



        rigidbody.velocity.Normalize();

    }


    private void GameOver()
    {

        
        GameObject shatterShpere =  GameObject.Instantiate(GameManager.Instance.breakablePlayer,transform.position,Quaternion.identity);

        foreach(Transform child in shatterShpere.transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddExplosionForce(6f, transform.position, 0.6f, 0.6f, ForceMode.VelocityChange);
        }

        rigidbody.isKinematic = true;

        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        canMove = false;
        gameOver = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!gameOver)
                GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(buildIndex+" "+SceneManager.sceneCountInBuildSettings);
            if (buildIndex < (SceneManager.sceneCountInBuildSettings-1))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }

    }


}
