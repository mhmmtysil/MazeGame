using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnMusicControl : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
       
        SoundConfiguration.Instance.MusicOn = !SoundConfiguration.Instance.MusicOn;
    }

}
