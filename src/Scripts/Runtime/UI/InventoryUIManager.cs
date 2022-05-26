using System.Collections.Generic;
using UnityEngine;
using DokkaebiBag.Generic;
using UnityEngine.AddressableAssets;

namespace DokkaebiBag.UI{
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]TextAsset asset;    
    Dictionary<string,ItemAssetInfo> consumableAssetDictionary=new Dictionary<string, ItemAssetInfo>();
    List<string> loadedAssetRID=new List<string>();
 
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
    public bool isLoaded(string RID)
    {
        if(loadedAssetRID.Contains(RID))
            return true;    
        return false;
    }
    public void LoadAssetInfo(string RID)
    {
        if(loadedAssetRID.Contains(RID))
            return;

        loadedAssetRID.Add(RID);
        GetAsset(RID).objectreference.LoadAssetAsync();
        GetAsset(RID).spritereference.LoadAssetAsync();
    }
    public void InstantiateItem(string RID,int count=1,Vector3 position=default(Vector3))
    {
        if(loadedAssetRID.Contains(RID))
            {
                var item=Instantiate(consumableAssetDictionary[RID].objectreference.Asset,position,Quaternion.identity) as GameObject;
                
                item.GetComponent<Item>().setTrigger(false);
                StartCoroutine(Utils.Utils.delay(1f,item.GetComponent<Item>().setTrigger));
                
                item.GetComponent<Item>().count=count;
                item.GetComponent<Item>().RID=RID;
                
                item.GetComponent<Item>().Init();
                
            }
        else
        {
            Debug.LogError(RID+" has not been loaded");
        }
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
