using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class HealthSegment
{
    public enum HealthType
    { 
        flesh = 0,
        shield = 1,
        armor = 2,
    }


    public int currentHP;
    public int maxHP;
    public float percent => (float)currentHP / maxHP;
    public EntityType entityType;
    public HealthType healthType;
    public bool dmgGating;

    public DamageData TakeDamage(DamageData damageData)
    {
        int dmg = damageData.damage; //CalculateDmg(healthType, entityType, damageData);
        int remainingDmg = 0;

        currentHP -= dmg;
        remainingDmg = -currentHP;
        
        if (remainingDmg > 0)
            remainingDmg = 0;
        if (currentHP < 0)
            currentHP = 0;

        if (dmgGating)
            return DamageData.none;

        var clone = damageData.Clone();
        clone.damage = remainingDmg;

        return clone;
    }
    public int CalculateDmg(HealthType hpType, EntityType entityType, DamageData data)
    {
        float dmg = data.damage;

        switch (hpType)
        {
            case HealthType.shield:
                if (data.type == DamageType.impact)
                    dmg *= 1.25f;
                    break;
            case HealthType.armor:
                if (data.type != DamageType.piercing)
                    dmg *= 0.75f;
                break;
            default:
            case HealthType.flesh:
                if (data.type != DamageType.slash)
                    dmg *= 1.5f;
                break;
        }
        switch (entityType)
        {
            default:
            case EntityType.none:
                break;
            case EntityType.player:
                break;
            case EntityType.monster:
                break;
        }

        return UnityEngine.Mathf.Clamp(UnityEngine.Mathf.RoundToInt(dmg), 1, int.MaxValue);
    }
}