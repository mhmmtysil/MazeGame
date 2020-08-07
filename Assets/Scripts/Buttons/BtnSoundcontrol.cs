using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnSoundcontrol : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        
        SoundConfiguration.Instance.SoundOn = !SoundConfiguration.Instance.SoundOn;
    }
}
