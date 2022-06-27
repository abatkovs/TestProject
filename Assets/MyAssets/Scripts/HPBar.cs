using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;

    [ContextMenu("Update hp")]
    public void UpdateHp(int maxHp, int hp)
    {
        var fill = (float)hp / maxHp;
        if(!gameObject.activeSelf) gameObject.SetActive(true);
        hpBar.fillAmount = fill;
    }
}
