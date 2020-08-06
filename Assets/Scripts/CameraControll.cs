using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Joystick joystick;
    public float yOffset = 0;
    public float smoothRot = 2f;
    void LateUpdate()
    {
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 5);
        yOffset += joystick.Horizontal * smoothRot;
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, yOffset, transform.rotation.eulerAngles.z));
        //transform.LookAt(player.transform);
        transform.Rotate(Vector3.up, joystick.Horizontal * smoothRot);
    }
}
