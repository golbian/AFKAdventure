using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SpellEditor : EditorWindow {

    public SpellList SpellList;
    private int viewIndex = 1;

    [MenuItem ("Window/Character Spell Editor %#e")]
    static void  Init () 
    {
        EditorWindow.GetWindow (typeof (SpellEditor));
    }

    void  OnEnable () {
        if(EditorPrefs.HasKey("ObjectPath")) 
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            SpellList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(SpellList)) as SpellList;
        }

    }

    void  OnGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Character Spell Editor", EditorStyles.boldLabel);
        if (SpellList != null) {
            if (GUILayout.Button("Show Spell List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = SpellList;
            }
        }
        if (GUILayout.Button("Open Spell List")) 
        {
                OpenSpellList();
        }
        if (GUILayout.Button("New Spell List")) 
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = SpellList;
        }
        GUILayout.EndHorizontal ();

        if (SpellList == null) 
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Spell List", GUILayout.ExpandWidth(false))) 
            {
                CreateNewSpellList();
            }
            if (GUILayout.Button("Open Existing Spell List", GUILayout.ExpandWidth(false))) 
            {
                OpenSpellList();
            }
            GUILayout.EndHorizontal ();
        }

            GUILayout.Space(20);

        if (SpellList != null) 
        {
            GUILayout.BeginHorizontal ();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex > 1)
                    viewIndex --;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex < SpellList.SpellListInstance.Count) 
                {
                    viewIndex ++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Spell", GUILayout.ExpandWidth(false))) 
            {
                AddSpell();
            }
            if (GUILayout.Button("Delete Spell", GUILayout.ExpandWidth(false))) 
            {
                DeleteSpell(viewIndex - 1);
            }

            GUILayout.EndHorizontal ();
            if (SpellList.SpellListInstance == null)
                Debug.Log("Spell is empty");
            if (SpellList.SpellListInstance.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Spell", viewIndex, GUILayout.ExpandWidth(false)), 1, SpellList.SpellListInstance.Count);
                EditorGUILayout.LabelField ("of   " +  SpellList.SpellListInstance.Count.ToString() + "  Spells", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                SpellList.SpellListInstance[viewIndex-1].SpellName = EditorGUILayout.TextField ("Spell Name", SpellList.SpellListInstance[viewIndex-1].SpellName as string);
                SpellList.SpellListInstance[viewIndex-1].Effect = EditorGUILayout.TextField ("Spell Effect", SpellList.SpellListInstance[viewIndex-1].Effect as string);
                SpellList.SpellListInstance[viewIndex-1].Type = EditorGUILayout.TextField ("Spell Type", SpellList.SpellListInstance[viewIndex-1].Type as string);
                SpellList.SpellListInstance[viewIndex-1].Damage = EditorGUILayout.IntField ("Spell Damage", SpellList.SpellListInstance[viewIndex-1].Damage);
                SpellList.SpellListInstance[viewIndex-1].Modifier = EditorGUILayout.FloatField ("Spell Modifier", SpellList.SpellListInstance[viewIndex-1].Modifier);
                SpellList.SpellListInstance[viewIndex-1].Cost = EditorGUILayout.IntField ("Spell Cost", SpellList.SpellListInstance[viewIndex-1].Cost);
                SpellList.SpellListInstance[viewIndex-1].Cooldown = EditorGUILayout.IntField ("Spell Cooldown", SpellList.SpellListInstance[viewIndex-1].Cooldown);
                SpellList.SpellListInstance[viewIndex-1].RangeMax = EditorGUILayout.IntField ("Spell RangeMax", SpellList.SpellListInstance[viewIndex-1].RangeMax);
                SpellList.SpellListInstance[viewIndex-1].RangeMin = EditorGUILayout.IntField ("Spell RangeMin", SpellList.SpellListInstance[viewIndex-1].RangeMin);
                SpellList.SpellListInstance[viewIndex-1].ZoneX = EditorGUILayout.IntField ("Spell ZoneX", SpellList.SpellListInstance[viewIndex-1].ZoneX);
                SpellList.SpellListInstance[viewIndex-1].ZoneY = EditorGUILayout.IntField ("Spell ZoneY", SpellList.SpellListInstance[viewIndex-1].ZoneY);


                GUILayout.Space(10);

            } 
            else 
            {
                GUILayout.Label ("This Spell List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(SpellList);
        }
    }

    void CreateNewSpellList () 
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        SpellList = CreateSpellList.Create();
        if (SpellList) 
        {
            SpellList.SpellListInstance = new List<Spell>();
            string relPath = AssetDatabase.GetAssetPath(SpellList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenSpellList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Spell List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SpellList = AssetDatabase.LoadAssetAtPath (relPath, typeof(SpellList)) as SpellList;
            if (SpellList.SpellListInstance == null)
                SpellList.SpellListInstance = new List<Spell>();
            if (SpellList) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddSpell () 
    {
        Spell newSpell = new Spell();
        newSpell.SpellName = "New Spell";
        SpellList.SpellListInstance.Add (newSpell);
        viewIndex = SpellList.SpellListInstance.Count;
    }

    void DeleteSpell (int index) 
    {
        SpellList.SpellListInstance.RemoveAt (index);
    }
}