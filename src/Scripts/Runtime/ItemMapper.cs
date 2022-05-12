using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DokkaebiBag.Editor{
public class ItemMapper 
{
    // json file ref
    public static TextAsset asset;
    public static InfoList listinfo=new InfoList();
    public static void Link(string prefabAddress,string spriteAddress)
    {
         listinfo=JsonUtility.FromJson<InfoList>(asset.text);
         int newRID=listinfo.assetInfoList.Count;
         listinfo.assetInfoList.Add(new ItemAssetInfo(newRID,prefabAddress,spriteAddress));
         string json=JsonUtility.ToJson(listinfo);
        var assetinfo= listinfo.assetInfoList[listinfo.assetInfoList.Count-1];
         File.WriteAllText(AssetDatabase.GetAssetPath(asset),json);
        //  EditorUtility.SetDirty(asset);
        //  assetinfo=listinfo.assetInfoList[2];
          Debug.Log(assetinfo.RID);  
          Debug.Log(assetinfo.prefabAddress);
          Debug.Log(assetinfo.spriteAddress);
        AssetDatabase.Refresh();
        
    }
}
[System.Serializable]
public class InfoList{
    public List<ItemAssetInfo> assetInfoList=new List<ItemAssetInfo>();
}
[System.Serializable]
public class ItemAssetInfo{
    public int RID;
    public string prefabAddress;
    public string spriteAddress;
    public ItemAssetInfo(int RID,string prefab,string sprite)
    {
        this.RID=RID;
        this.prefabAddress=prefab;
        this.spriteAddress=sprite;
    }
}
}

