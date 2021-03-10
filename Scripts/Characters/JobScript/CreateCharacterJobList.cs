using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateCharacterJobList {
    [MenuItem("Assets/Create/Character Item List")]
    public static CharacterJobList  Create()
    {
        CharacterJobList asset = ScriptableObject.CreateInstance<CharacterJobList>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/Characters/JobScript/CharacterJobList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
