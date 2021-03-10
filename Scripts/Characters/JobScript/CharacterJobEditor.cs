using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CharacterJobEditor : EditorWindow {

    public CharacterJobList CharacterJobList;
    private int viewIndex = 1;

    [MenuItem ("Window/Character Job Editor %#e")]
    static void  Init () 
    {
        EditorWindow.GetWindow (typeof (CharacterJobEditor));
    }

    void  OnEnable () {
        if(EditorPrefs.HasKey("ObjectPath")) 
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            CharacterJobList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(CharacterJobList)) as CharacterJobList;
        }

    }

    void  OnGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Character Job Editor", EditorStyles.boldLabel);
        if (CharacterJobList != null) {
            if (GUILayout.Button("Show Job List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = CharacterJobList;
            }
        }
        if (GUILayout.Button("Open Job List")) 
        {
                OpenJobList();
        }
        if (GUILayout.Button("New Job List")) 
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = CharacterJobList;
        }
        GUILayout.EndHorizontal ();

        if (CharacterJobList == null) 
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Job List", GUILayout.ExpandWidth(false))) 
            {
                CreateNewJobList();
            }
            if (GUILayout.Button("Open Existing Job List", GUILayout.ExpandWidth(false))) 
            {
                OpenJobList();
            }
            GUILayout.EndHorizontal ();
        }

            GUILayout.Space(20);

        if (CharacterJobList != null) 
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
                if (viewIndex < CharacterJobList.JobList.Count) 
                {
                    viewIndex ++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Job", GUILayout.ExpandWidth(false))) 
            {
                AddJob();
            }
            if (GUILayout.Button("Delete Job", GUILayout.ExpandWidth(false))) 
            {
                DeleteJob(viewIndex - 1);
            }

            GUILayout.EndHorizontal ();
            if (CharacterJobList.JobList == null)
                Debug.Log("Character is empty");
            if (CharacterJobList.JobList.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Job", viewIndex, GUILayout.ExpandWidth(false)), 1, CharacterJobList.JobList.Count);
                //Mathf.Clamp (viewIndex, 1, CharacterJobList.JobList.Count);
                EditorGUILayout.LabelField ("of   " +  CharacterJobList.JobList.Count.ToString() + "  Jobs", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                CharacterJobList.JobList[viewIndex-1].JobName = EditorGUILayout.TextField ("Job Name", CharacterJobList.JobList[viewIndex-1].JobName as string);
                CharacterJobList.JobList[viewIndex-1].BaseHP = EditorGUILayout.IntField ("Job baseHP", CharacterJobList.JobList[viewIndex-1].BaseHP);
                CharacterJobList.JobList[viewIndex-1].BaseMana = EditorGUILayout.IntField ("Job baseMana", CharacterJobList.JobList[viewIndex-1].BaseMana);
                CharacterJobList.JobList[viewIndex-1].AttackPower = EditorGUILayout.IntField ("Job attackPower", CharacterJobList.JobList[viewIndex-1].AttackPower);
                CharacterJobList.JobList[viewIndex-1].Resistance = EditorGUILayout.IntField ("Job resistance", CharacterJobList.JobList[viewIndex-1].Resistance);



                GUILayout.Space(10);

            } 
            else 
            {
                GUILayout.Label ("This Character List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(CharacterJobList);
        }
    }

    void CreateNewJobList () 
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        CharacterJobList = CreateCharacterJobList.Create();
        if (CharacterJobList) 
        {
            CharacterJobList.JobList = new List<CharacterJob>();
            string relPath = AssetDatabase.GetAssetPath(CharacterJobList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenJobList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Character Job List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            CharacterJobList = AssetDatabase.LoadAssetAtPath (relPath, typeof(CharacterJobList)) as CharacterJobList;
            if (CharacterJobList.JobList == null)
                CharacterJobList.JobList = new List<CharacterJob>();
            if (CharacterJobList) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddJob () 
    {
        CharacterJob newJob = new CharacterJob();
        newJob.JobName = "New Job";
        CharacterJobList.JobList.Add (newJob);
        viewIndex = CharacterJobList.JobList.Count;
    }

    void DeleteJob (int index) 
    {
        CharacterJobList.JobList.RemoveAt (index);
    }
}