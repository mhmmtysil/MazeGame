using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnMainMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        SoundConfiguration.Instance.PlaySetMainSourceClipMenu();
        LoadingScreen.Instance.WhenLoad();
        SceneManager.LoadScene(0);
    }
}
