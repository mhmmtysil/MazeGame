using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnPlay : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        
        SoundConfiguration.Instance.PlaySetMainSourceClipInGame();
        SceneManager.LoadScene(1);
    }

}
