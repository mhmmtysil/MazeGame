﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnPause : MonoBehaviour,  IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameConfiguration.Instance.Paused = true;
    }
}
