using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DokkaebiBag.Utils;
using System;
using DokkaebiBag.Generic;

namespace DokkaebiBag.Editor
{
    public class ItemDataEditor : EditorWindow
    {
    
    static string textasssetpath="Assets/Scripts/DokkaebiBag/src/Scripts/ItemAssetDB.json";
    static TextAsset asset;
    static System.Type[] types;
    int tab;
    static string[] ItemTypeoptions;
    int selectedtype;
    Item.Data data=new Item.Consumable("10","lmayou","NO",Stacking.X1,MainTag.Armor,SubTag.Shield,1);
    int mainselect;
    // Dictionary<Type,Action<Item.Data>> GUICreator=new Dictionary<Type, Action<Item.Data>>{
    //     {
    //     typeof(Int32),(data)=>{
    //         int n=0;
    //         EditorGUILayout.IntField("IntField",n,GUIStyle.none,null);
    //     }
    //     },
    //     {
    //     typeof(string),(data)=>{

    //     }
    //     },
    //     {
    //     typeof(float),(data)=>{

    //     }
    //     },
    //     {
    //     typeof(MainTag),(data)=>{
    //         Item.Consumable temp=data as Item.Consumable;
    //         var fields=Utils.Utils.getAllFieldsof(typeof(MainTag));
    //         string[] options=new string[fields.Length];
    //         for(int i=0;i<options.Length;i++)
    //         {
    //             options[i]=fields[i].Name;
            
    //         }
    //         mainselect=EditorGUILayout.Popup(mainselect,options,GUILayout.MinHeight(10));
           
    //         data=temp;
    //     }
    //     }
    // };
    
    [MenuItem("Editor/Itemdata")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ItemDataEditor));
        asset=AssetDatabase.LoadAssetAtPath(textasssetpath,typeof(TextAsset)) as TextAsset;
        types=Utils.Utils.getAllLeafTypesof<DokkaebiBag.Generic.Item.Data>();
        
        ItemTypeoptions=new string[types.Length];
        for(int i=0;i<ItemTypeoptions.Length;i++)
        {
            ItemTypeoptions[i]=types[i].Name;
        }
        
    }
    void OnGUI()
    {
            EditorGUILayout.BeginVertical();

        tab=GUI.SelectionGrid(new Rect(5,10,100,100),tab,new string[] { "ADD", "SEARCH"},1);

        EditorGUI.DrawRect(new Rect(110,10,2,position.height-20),Color.gray);
            EditorGUILayout.EndVertical();

        switch(tab)
        {
            case 0:

                selectedtype=EditorGUILayout.Popup(selectedtype,ItemTypeoptions);
           
             var fieldsinfo=Utils.Utils.getAllFieldsof(types[selectedtype]);
            EditorGUILayout.BeginVertical();
                foreach(var field in fieldsinfo)
                {
                    // Debug.Log(field.FieldType);
                    // if(field.FieldType!=typeof(SubTag))
                    // GUICreator[field.FieldType](data);
                    createfield(field.FieldType);
                }
            EditorGUILayout.EndVertical();

                break;
            case 1:

                break;
        }
    }
    void createfield(Type type)
    {
        if (type == typeof(Int32))
        {
        }
        else if(type==typeof(MainTag))
        {
            Debug.Log(data.mainTag);
            Item.Consumable temp=data as Item.Consumable;
            var fields=Utils.Utils.getAllFieldsof(typeof(MainTag));
            string[] options=new string[fields.Length];
            for(int i=0;i<options.Length;i++)
            {
                options[i]=fields[i].Name;
            }
            mainselect=EditorGUILayout.Popup(mainselect,options,GUILayout.MinHeight(10));
            if(fields[mainselect].Name==Enum.GetName(typeof(MainTag),MainTag.Weapon))
            {
                temp.mainTag=MainTag.Weapon;
            }

            data=temp;
        }
    }
    }
    
    
}
