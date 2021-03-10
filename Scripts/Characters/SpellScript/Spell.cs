using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 1)]
public class Spell : ScriptableObject {
    public string SpellName;
    public string Effect;
    public int Damage;
    public float Modifier;
    public int Cost;
    public int Cooldown;
    public string Type;
    public int RangeMax;
    public int RangeMin;
    public int ZoneX;
    public int ZoneY;
}