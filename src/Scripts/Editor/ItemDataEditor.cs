using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DokkaebiBag.Utils;
using System;
using System.Reflection;
using DokkaebiBag.Generic;

namespace DokkaebiBag.Editor
{
    public class ItemDataEditor : EditorWindow
    {
    static string textasssetpath="Assets/Scripts/DokkaebiBag/src/Scripts/ItemAssetDB.json";
    List<string> ignoreField=new List<string>{
        "RID","UID"
    };
    static TextAsset asset;
    static System.Type[] types;
    static int[] intfields;//TODOmake this dictionary
    static string[] stringfields; //TODOMake this dictionary
    int IFcounter,SFcounter;
    int tab;
    static string[] ItemTypeoptions;
     int selectedtype;
    static Item.Data data;
    int tagselect,stackingselect;
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
        
        FieldInfo[][] intfieldsinfo,stringfieldsinfo;
        
        ItemTypeoptions=new string[types.Length];
        intfieldsinfo=new FieldInfo[types.Length][];
        stringfieldsinfo=new FieldInfo[types.Length][];
        
        for(int i=0;i<ItemTypeoptions.Length;i++)
        {
            ItemTypeoptions[i]=types[i].Name;
        intfieldsinfo[i]=Utils.Utils.GetAllFieldofType(types[i],typeof(int));
        stringfieldsinfo[i]=Utils.Utils.GetAllFieldofType(types[i],typeof(string));

        }
            int intmaxlength=-1,stringmaxlength=-1;
           for(int i=0;i<types.Length;i++)
           {

            if(intfieldsinfo[i].Length>intmaxlength)
                {
                    intmaxlength=intfieldsinfo[i].Length;
                }
                for(int j=0;j<intfieldsinfo[i].Length;j++)
                {

                    // Debug.Log(types[i]+" "+intfieldsinfo[i][j].Name);
                }

            if(stringfieldsinfo[i].Length>stringmaxlength)
                {
                    stringmaxlength=stringfieldsinfo[i].Length;
                }
                for(int j=0;j<stringfieldsinfo[i].Length;j++)
                {
                    // Debug.Log(types[i]+" "+stringfieldsinfo[i][j].Name);
                }

           }
           intfields=new int[intmaxlength];
           stringfields=new string[stringmaxlength];
        
    }
    void OnGUI()
    {
        tab=GUI.SelectionGrid(new Rect(5,10,100,100),tab,new string[] { "ADD", "SEARCH"},1);

        EditorGUI.DrawRect(new Rect(110,10,2,position.height-20),Color.gray);

        switch(tab)
        {
            case 0:
                GUILayout.BeginArea(new Rect(125,5,position.width-125,position.height));
                selectedtype=EditorGUILayout.Popup(selectedtype,ItemTypeoptions);
                var fieldsinfo=Utils.Utils.getAllFieldsof(types[selectedtype]);
                for(int i=0;i<fieldsinfo.Length;i++)
                {
                    if(!ignoreField.Contains(fieldsinfo[i].Name))
                        createfield(fieldsinfo[i].FieldType,fieldsinfo[i].Name,types[selectedtype]);
                }
                GUILayout.EndArea();
                break;
            case 1:

                break;
        }
    }
    void createfield(Type type,string fieldName,Type Datatype)
    {
        if (type == typeof(int))
        {
            if(Datatype ==typeof(Item.Weapon))
            {
                Item.Weapon temp=new Item.Weapon("","","",Tag.Ammo,0,0);
                intfields[IFcounter]=EditorGUILayout.IntField(fieldName,intfields[IFcounter],GUILayout.Height(20));
                var prop=Utils.Utils.getFieldInfo(typeof(Item.Weapon),fieldName);
                prop.SetValue(temp,intfields[IFcounter]);
                IFcounter=(IFcounter+1)%intfields.Length;
                data=temp;
            }
        }
        else if(type==typeof(string))
        {
            if(Datatype==typeof(Item.Weapon))
            {
                Item.Weapon temp=new Item.Weapon("","","",Tag.Ammo,0,0);
                float height=20;
                if(fieldName=="description")
                    height=100;
                stringfields[SFcounter]=EditorGUILayout.TextField(fieldName,stringfields[SFcounter],GUILayout.Height(height));
                var prop=Utils.Utils.getFieldInfo(typeof(Item.Weapon),fieldName);
                prop.SetValue(temp,stringfields[SFcounter]);
                SFcounter=(SFcounter+1)%stringfields.Length;
                data=temp;
            }
        }
        else if(type==typeof(Tag))
        {
            if(Datatype==typeof(Item.Weapon))
            {
            // Debug.Log(data.tag);
            Item.Weapon temp=data as Item.Weapon;
            var fields=Utils.Utils.getAllFieldsof(typeof(Tag));
            string[] options=new string[fields.Length];
            for(int i=0;i<options.Length;i++)
            {
                options[i]=fields[i].Name;
            }
            tagselect=EditorGUILayout.Popup(tagselect,options,GUILayout.MinHeight(10));
            
            Array tagvalues=Enum.GetValues(typeof(Tag));
            
            foreach(object tag in tagvalues)
            {
                if((int)tag==0)
                    continue;

                if (fields[tagselect].Name == Enum.GetName(typeof(Tag), tag)) 
                {
                    temp.tag = (Tag)tag;
                }
            }
            data=temp;
            }
        }
        else if(type==typeof(Stacking))
        {
            // Debug.Log(data.tag);
            if(Datatype==typeof(Item.Consumable))
            {
                Item.Consumable temp=data as Item.Consumable;
                var fields=Utils.Utils.getAllFieldsof(typeof(Stacking));
                string[] options=new string[fields.Length];
                for(int i=0;i<options.Length;i++)
                {
                    options[i]=fields[i].Name;
                }
                stackingselect=EditorGUILayout.Popup(stackingselect,options,GUILayout.MinHeight(10));

                Array stackingvalues=Enum.GetValues(typeof(Stacking));

                foreach(object stack in stackingvalues)
                {
                    if((int)stack==0)
                        continue;

                    if (fields[tagselect].Name == Enum.GetName(typeof(Stacking), stack)) 
                    {
                        temp.stacking= (Stacking)stack;
                    }
                }
                data=temp;
            }
        }
        else
        {
            Debug.LogError("define "+type+ fieldName);
        }
    }
    }
    
    
}
