using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject lootHistory;
    [SerializeField] private GameObject gear;
    [SerializeField] private GameObject inventory;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var result = eventData.pointerCurrentRaycast;
        if (result.gameObject.TryGetComponent(out IUIInteract interact))
        {
            interact.Click();
        }

    }

}
