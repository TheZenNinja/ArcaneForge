using Crafting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public Zone zone;
    public WeaponObject weapon;
    public Vector3 offset;
    public Vector3 rotation;
    void Start()
    {
        zone.onEnter += ValidateWeaponAdd;
        zone.onExit += ValidateWeaponRemove;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (weapon)
        {
            if (weapon.beingDragged)
                return;

            if (weapon.transform.parent != transform)
            {
                weapon.TogglePhys(false);
                weapon.transform.SetParent(transform);
                weapon.position = transform.TransformPoint(offset);
                weapon.localAngles = (rotation);
            }
        }
    }
    public void ValidateWeaponAdd(GameObject g)
    {
        var w = g.GetComponent<WeaponObject>();
        if (w != null && weapon == null)
        {
            weapon = w;
            w.TogglePhys(false);
        }
    }
    public void ValidateWeaponRemove(GameObject g)
    {
        var w = g.GetComponent<WeaponObject>();
        if (w != null && weapon == w)
        {
            if (!w.beingDragged)
                w.TogglePhys(true);
            weapon.transform.SetParent(null);
            weapon = null;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.TransformPoint(offset), .1f);
    }
}
