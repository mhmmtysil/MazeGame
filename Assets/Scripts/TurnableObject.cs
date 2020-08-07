using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnableObject : MonoBehaviour
{
    public float speed;
    void Update()
    {
        if (GameConfiguration.Instance.Paused)
            return;
        transform.Rotate(Vector3.forward,speed*Time.deltaTime * -1);       
    }
}
