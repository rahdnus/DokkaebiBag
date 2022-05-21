using System.Collections.Generic;
using UnityEngine;
using DokkaebiBag.Generic;
using UnityEngine.AddressableAssets;

namespace DokkaebiBag.UI{
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]TextAsset asset;    
    Dictionary<string,ItemAssetInfo> consumableAssetDictionary=new Dictionary<string, ItemAssetInfo>();
 
    void Start()
    {
        var list=JsonUtility.FromJson(asset.text,typeof(InfoList)) as InfoList;
        foreach(ItemAssetInfo info in list.assetInfoList)
        {
            consumableAssetDictionary.Add(info.RID,info);
            Debug.Log(info.RID);
        }
    }
    public ItemAssetInfo GetAsset(string RID)
    {
        if(consumableAssetDictionary==null)
        {
            Debug.LogError("AssetDictionary is Empty!");
            return null;
        }
        Debug.Log(RID);
        return consumableAssetDictionary[RID];
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
    public AssetReferenceGameObject objectreference;
    public AssetReferenceSprite spritereference;

    public ItemAssetInfo(string RID,AssetReferenceGameObject objectreference,AssetReferenceSprite spritereference)
    {
        this.RID=RID;
        this.objectreference=objectreference;
        this.spritereference=spritereference;

    }
}

}
