using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    [HideInInspector]
    public Text txtLetter;

    void Awake()
    {
        txtLetter.GetComponentInChildren<Text>();
    }
}
