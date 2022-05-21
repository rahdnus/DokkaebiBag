using System.IO;
using UnityEngine;
using UnityEditor;
using DokkaebiBag.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
namespace DokkaebiBag.Editor{
public class ItemMapper 
{
    // json file ref
    public static TextAsset asset;
    public static InfoList listinfo=new InfoList();
    public static void Link(ItemMapperSeal seal)
    {
        var newRID="0000";

        // Addressables.Release(temp.Objectreference);
        listinfo=JsonUtility.FromJson<InfoList>(asset.ToString());
        if(listinfo==null)
        {
          listinfo=new InfoList();
        }
        else
        {
          newRID=listinfo.assetInfoList.Count.ToString("0000");
        }
        seal.Objectreference.LoadAssetAsync<GameObject>().Completed+=(op)=>{
            if(op.Status==AsyncOperationStatus.Succeeded)
            {
              op.Result.GetComponent<DokkaebiBag.Generic.Item>().RID=newRID;
            }
        };
        seal.Objectreference.ReleaseAsset();
        listinfo.assetInfoList.Add(new ItemAssetInfo(newRID,seal.Objectreference,seal.Spritereference));
        string json=JsonUtility.ToJson(listinfo);
        // var assetinfo= listinfo.assetInfoList[listinfo.assetInfoList.Count-1];
        File.WriteAllText(AssetDatabase.GetAssetPath(asset),json);
 
        AssetDatabase.Refresh();
        
    }
}
}



