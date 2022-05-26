using UnityEngine;
using UnityEditor;
using DokkaebiBag.Generic;

namespace DokkaebiBag.Editor{

[CustomEditor(typeof(DokkaebiBag.Generic.Item))]
public class ItemEditor:UnityEditor.Editor
{
    System.Type[] types;
    string[] options;
    int selected=0; 
    UnityEngine.Object item;
    public void OnEnable()
    {
      types= DokkaebiBag.Utils.Utils.getAllLeafTypesof<Item.Data>();
      options=new string[types.Length];
      for(int i=0;i<options.Length;i++)
      {
          options[i]=types[i].Name;
      }
    }
  public override void OnInspectorGUI()
  { 

      base.OnInspectorGUI();
      selected=EditorGUILayout.Popup("Choose",selected,options);
    //   EditorGUILayout.ObjectField(
    //  );
    // item=System.Activator.CreateInstance(types[selected]) as  UnityEngine.Object;
  } 
}
public class Example{

}
}
