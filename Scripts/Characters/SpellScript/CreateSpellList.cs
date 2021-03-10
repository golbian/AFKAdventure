using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateSpellList {
    [MenuItem("Assets/Create/Spell List")]
    public static SpellList  Create()
    {
        SpellList asset = ScriptableObject.CreateInstance<SpellList>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/Characters/SpellScript/SpellList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}