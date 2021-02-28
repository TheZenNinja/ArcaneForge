using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public struct DamageData
{
    public static DamageData none = new DamageData(-1);
    public bool isNone => damage < 0;
    public int damage;
    public DamageType type;
    public DamageElement element;
    public float statusChance;
    #region Constructors
    public DamageData(int damage, DamageType type, DamageElement element, float statusChance)
    {
        this.damage = damage;
        this.type = type;
        this.element = element;
        this.statusChance = statusChance;
    }

    public DamageData(int damage)
    {
        this.damage = damage;
        this.type = DamageType.none;
        this.element = DamageElement.physical;
        this.statusChance = 0;
    }
    #endregion
    public DamageData Clone() => new DamageData(damage, type, element, statusChance);

}