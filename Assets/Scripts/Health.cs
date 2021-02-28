using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public List<HealthSegment> segments;

    public UnityAction onHit;
    public UnityAction onDeath;


    private int prevLayer = 0;
    private bool _i;
    public bool invulnerable
    {
        get => _i;
        set
        {
            _i = value;
            if (value)
            {
                prevLayer = gameObject.layer;
                gameObject.layer = 11;
            }
            else
                gameObject.layer = prevLayer;
        }
    }
    public float getPercent()
    {
        float currentHP = 0;
        int totalHP = 0;
        foreach (var s in segments)
        {
            currentHP += s.currentHP;
            totalHP += s.maxHP;
        }

        return currentHP / totalHP;
    }
    public bool noHPLeft()
    {
        foreach (var s in segments)
            if (s.currentHP > 0)
                return false;
        return true;
    }
    public void InvulernableFor(float duration) => StartCoroutine(InvulTimer(duration));
    IEnumerator InvulTimer(float duration)
    {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }
    public void Hit(DamageData data)
    {
        foreach (var s in segments)
        {
            data = s.TakeDamage(data);
            if (data.isNone)
                break;
        }

        onHit?.Invoke();

        if (getPercent() <= 0)
            onDeath?.Invoke();
    }
}