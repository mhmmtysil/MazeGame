using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    public Rigidbody rb;
    public float cameraRotationSpeed = 5f;
    public float rotationSmoothSpeed = 10f;
    public float walkSpeed = 15f;
    private GameObject collisionWith;
    float bodyRotationX;
    float camRotationY;
    float speed;
    public int lenght;
    private int counter;
    [HideInInspector]
    public Joystick joystick;
    public Vector3 startingPos;
    public void Initialize()
    {
        transform.position = startingPos;
    }
    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("PlayerMover").GetComponent<Joystick>();
        startingPos = transform.position;
    }

    void Update()
    {
       // LookRotation();
        Movement();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("SpeedUp"))
        {
            SoundConfiguration.Instance.PlaySpeedSound();
            collisionWith = c.gameObject;
            Destroy(collisionWith.gameObject);
            walkSpeed += 5;
        }
       
        if (c.CompareTag("Letters")) {
            Destroy(c.gameObject);
            GameConfiguration.Instance.letters[c.GetComponent<Solution>().index].SetActive(true);
            StageController();
            SoundConfiguration.Instance.PlayCollectSound();
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
        //Change our characters velocity in this direction
        rb.velocity = transform.forward * joystick.Vertical * speed + transform.right * joystick.Horizontal * speed + transform.up * rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkSpeed);
        speed = walkSpeed;
    }
}
