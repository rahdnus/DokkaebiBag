using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DokkaebiBag.Editor{

public class ItemMapEditor : EditorWindow
{
    GameObject itemPrefab;
    Sprite sprite;
    [MenuItem("Editor/ItemMapper")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ItemMapEditor));
    }
    void OnGUI()
    {
        itemPrefab=EditorGUILayout.ObjectField(itemPrefab,typeof(GameObject),false) as GameObject;
        sprite=EditorGUILayout.ObjectField(sprite,typeof(Sprite),false) as Sprite;

        if(GUILayout.Button("Add"))
        {
            ItemMapper.Link(AssetDatabase.GetAssetPath(itemPrefab),AssetDatabase.GetAssetPath(itemPrefab));
            Debug.Log(AssetDatabase.GetAssetPath(itemPrefab));
            Debug.Log(AssetDatabase.GetAssetPath(sprite));
        }
    }
}
}