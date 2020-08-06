using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnNextStage : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        GameConfiguration.Instance.NextLevel();
        gameObject.transform.parent.gameObject.SetActive(false);
    }

}
