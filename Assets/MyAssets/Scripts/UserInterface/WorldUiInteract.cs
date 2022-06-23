using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldUiInteract : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string txt;
 
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(txt);
    }
}
