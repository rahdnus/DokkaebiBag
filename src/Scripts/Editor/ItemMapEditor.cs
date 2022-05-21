using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DokkaebiBag.Editor{

public class ItemMapEditor : EditorWindow
{
    ItemMapperSeal seal;
    GameObject itemPrefab;
    Sprite sprite;
    [MenuItem("Editor/ItemMapper")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ItemMapEditor));
    }
    void OnGUI()
    {
        ItemMapper.asset=EditorGUILayout.ObjectField(ItemMapper.asset,typeof(TextAsset),false) as TextAsset;
        // itemPrefab=EditorGUILayout.ObjectField(itemPrefab,typeof(GameObject),false) as GameObject;
        // sprite=EditorGUILayout.ObjectField(sprite,typeof(Sprite),false) as Sprite;
        seal=EditorGUILayout.ObjectField(seal,typeof(ItemMapperSeal),false) as ItemMapperSeal;

        if(GUILayout.Button("Add"))
        {
            ItemMapper.Link(seal);
            // Debug.Log(AssetDatabase.GetAssetPath(itemPrefab));
            // Debug.Log(AssetDatabase.GetAssetPath(sprite));
        }
    }
}
}