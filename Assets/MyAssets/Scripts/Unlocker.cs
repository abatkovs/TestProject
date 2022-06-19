using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

public class Unlocker : SerializedMonoBehaviour
{
    [SerializeField] private TMP_Text remainingResourcesTxt;
    [SerializeField] private List<UnlockPrice> unlockPrice;
    [Space(20)]
    [SerializeField] private UnlockableArea areaToUnlock;
    [SerializeField] private float payTimer = 0.1f;
    private float _curTimer;

    private void Start()
    {
        _curTimer = payTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Unlocker");
        //areaToUnlock.UnlockArea();
    }

    private void OnTriggerStay(Collider other)
    {
        _curTimer -= Time.deltaTime;
        if (_curTimer < 0)
        {
            foreach (var item in unlockPrice)
            {
                item.BuyResource();
                remainingResourcesTxt.text = $"{item.price} W";
                if (item.price == 0)
                {
                    unlockPrice.Remove(item);
                    return;
                }
            }

            if (unlockPrice.Count == 0)
            {
                areaToUnlock.UnlockArea();
                transform.gameObject.SetActive(false);
                return;
            }
            _curTimer = payTimer;
        }
    }
}

[Serializable]
public class UnlockPrice
{
    public Item requiredItem;
    public int price;

    public bool BuyResource()
    {
        if (price <= 0) return false;
        if (requiredItem.GetCount() <= 0) return false;
        else
        {
            requiredItem.DecreaseCount();
            price--;
            return true;
        }
    }
}
