using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Solution : MonoBehaviour
{
    public int index = 0;
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player")) {
            Destroy(gameObject);
            GameConfiguration.Instance.letters[index].SetActive(true);
            c.gameObject.GetComponent<FpsController>().StageController();
        }
    }
}
