using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public float timer = 1.5f;
    private bool activePassive;
    private float defaultTimer;
    public static LoadingScreen Instance { get; private set; }
    public Slider slider;
    void Awake()
    {
        defaultTimer = timer;
        slider.maxValue = defaultTimer;
        Instance = this;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        slider.value = defaultTimer - timer;
        if (timer <= 0) {
            gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        timer = 1.5f;
    }
    public void WhenLoad() 
    {
        gameObject.SetActive(true);
    }
   
}
