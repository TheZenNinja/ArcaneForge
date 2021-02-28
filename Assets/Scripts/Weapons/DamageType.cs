using System;

public enum DamageType
{
    none = 0,
    slash = 1,      //highest dmg
    impact = 2,     //impair 
    piercing = 3,   //armor piercing
}
public enum DamageElement
{
    physical = 0, // crit
    fire = 1, // DoT
    air = 2, // elemental vulrenablity
    ice = 3, // freeze
    earth = 4, // launch (rocks jut out of the ground to send them into the air, or vines come out of the ground to slam them down)
    lightning = 5, // chain dmg
    arcane = 6, // increased effect damage/chance
}
