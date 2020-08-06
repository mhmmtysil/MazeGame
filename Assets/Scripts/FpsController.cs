using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rb;
    public float cameraRotationSpeed = 5f;
    public float rotationSmoothSpeed = 10f;
    public float walkSpeed = 15f;
    private GameObject collisionWith;
    float bodyRotationX;
    float camRotationY;
    Vector3 directionIntentX;
    Vector3 directionIntentY;
    float speed;
    public int lenght;
    private int counter;
    public AudioSource LetterCollectSource;
    public AudioClip LetterCollectClip;
    public AudioClip SpeedUpClip;
    private Joystick joystick;

    void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    void Update()
    {
       // LookRotation();
        Movement();
    }
    void LookRotation()
    {
       
      
        //get the camera and body rotational values
        bodyRotationX += Input.GetAxis("Mouse X") * cameraRotationSpeed;

        //stop our camera from rotating 360 degrees
        camRotationY = Mathf.Clamp(camRotationY, 0,0);
        //Camera rotation targets and handle rotations of the body and camera
        Quaternion camTargetRotation = Quaternion.Euler(-camRotationY, 0, 0);
        Quaternion bodyTargetRotation = Quaternion.Euler(0, bodyRotationX, 0);
        // handle rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, Time.deltaTime * rotationSmoothSpeed);
    }
    void OnTriggerEnter(Collider collisionSpeed)
    {
        if (collisionSpeed.CompareTag("SpeedUp"))
        {
            collisionWith = collisionSpeed.gameObject;
            Destroy(collisionWith.gameObject);
            walkSpeed += 5;
            LetterCollectSource.PlayOneShot(clip: SpeedUpClip);
        }
       
        if (collisionSpeed.CompareTag("Letters")) {
            LetterCollectSource.PlayOneShot(clip: LetterCollectClip);
        }

    }

    public void StageController(){
        counter += 1;
        if (counter >= GameConfiguration.Instance.letters.Count) {
            GameConfiguration.Instance.StageCompleted();
        }

    
    }
    void Movement()
    {
        //Direction must Match Camera Direction
        directionIntentX = cam.right;
        directionIntentX.y = 0;
        directionIntentX.Normalize();
        directionIntentY = cam.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();
        //Change our characters velocity in this direction
        rb.velocity = directionIntentY * joystick.Vertical * speed + directionIntentX * joystick.Horizontal * speed + Vector3.up * rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkSpeed);
        speed = walkSpeed;
    }
}
