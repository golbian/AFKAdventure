using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Character", order = 0)]
public class Character : ScriptableObject {
    // public SpellList SpellList;
    public CharacterJob characterJob;
    public string CharacterName;
    public Spell spell1;
    public Spell spell2;
    public Spell spell3;
    public Spell spell4;
}
