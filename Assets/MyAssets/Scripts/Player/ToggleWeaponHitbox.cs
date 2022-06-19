using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWeaponHitbox : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
}
