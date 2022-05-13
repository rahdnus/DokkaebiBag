using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DokkaebiBag.Generic;

namespace DokkaebiBag.Editor{
public class ItemMapper 
{
    // json file ref
    public static TextAsset asset;
    public static InfoList listinfo=new InfoList();
    public static void Link(string prefabAddress,string spriteAddress)
    {

        listinfo=JsonUtility.FromJson<InfoList>(asset.text);
        string newRID=listinfo.assetInfoList.Count.ToString();

        var prefab=AssetDatabase.LoadAssetAtPath(prefabAddress,typeof(GameObject)) as GameObject;

        prefab.GetComponent<Item>().myData.AssignRID(newRID);

        listinfo.assetInfoList.Add(new ItemAssetInfo(newRID,prefabAddress,spriteAddress));
        string json=JsonUtility.ToJson(listinfo);
        var assetinfo= listinfo.assetInfoList[listinfo.assetInfoList.Count-1];
        File.WriteAllText(AssetDatabase.GetAssetPath(asset),json);
 
          Debug.Log(assetinfo.RID);  
          Debug.Log(assetinfo.prefabAddress);
          Debug.Log(assetinfo.spriteAddress);
        
        AssetDatabase.Refresh();
        
    }
}
}




