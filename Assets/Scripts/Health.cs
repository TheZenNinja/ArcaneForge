using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public float percent => (float)currentHP / maxHP;

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
    public void InvulernableFor(float duration) => StartCoroutine(InvulTimer(duration));
    IEnumerator InvulTimer(float duration)
    {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }
    public void Hit(int dmg)
    {
        currentHP -= dmg;

        onHit?.Invoke();

        if (currentHP <= 0)
            onDeath?.Invoke();
    }
}