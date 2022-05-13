using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DokkaebiBag.Generic;

namespace DokkaebiBag.UI{
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]TextAsset asset;    
    Dictionary<string,ItemAssetInfo> assetDictionary=new Dictionary<string, ItemAssetInfo>();
    void Start()
    {
        var list=JsonUtility.FromJson(asset.text,typeof(InfoList)) as InfoList;
        foreach(ItemAssetInfo info in list.assetInfoList)
        {
            assetDictionary.Add(info.RID,info);
        }
    }
    public ItemAssetInfo GetAsset(string RID)
    {
        if(assetDictionary==null)
        {
            Debug.LogError("AssetDictionary is Empty!");
            return null;
        }
        return assetDictionary[RID];
    }
}
}
namespace DokkaebiBag.Generic
{
[System.Serializable]public class InfoList
{
    public List<ItemAssetInfo> assetInfoList=new List<ItemAssetInfo>();
}
[System.Serializable]public class ItemAssetInfo
{
    public string RID;
    public string prefabAddress;
    public string spriteAddress;
    public ItemAssetInfo(string RID,string prefab,string sprite)
    {
        this.RID=RID;
        this.prefabAddress=prefab;
        this.spriteAddress=sprite;
    }
}

}