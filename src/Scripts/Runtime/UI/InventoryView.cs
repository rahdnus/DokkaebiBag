using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine;
using DokkaebiBag.Generic;
namespace DokkaebiBag.UI{
public class InventoryView : MonoBehaviour
{
    [SerializeField]GameObject itemPrefab,inventoryPanel,detailsPanel;
    [SerializeField] string nameField,typefield;
    [SerializeField] TextMeshProUGUI nameFieldObject,typeFieldObject;
    [SerializeField]Inventory inventory;
    [SerializeField]InventoryUIManager invmanager;

    List<ItemView> Items=new List<ItemView>();
    void Start()
    {
        inventory.Init(invmanager.LoadAssetInfo);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            refreshInventoryView();
    }
    public void refreshInventoryView()
    {
        foreach(Transform childitem in inventoryPanel.transform)
        {
            Destroy(childitem.gameObject);
        }
        Items=new List<ItemView>();
        foreach(Item.Data item in inventory.Items)
        {
            GameObject itemObject=Instantiate(itemPrefab,inventoryPanel.transform) as GameObject;
                if(invmanager.isLoaded(item.RID))
                itemObject.GetComponentInChildren<Image>().sprite=invmanager.GetAsset(item.RID).spritereference.Asset as Sprite;
            else
                {
                    Debug.LogError(item.RID +"is not loaded!");
                    Debug.Break();
                }

            itemObject.GetComponent<ItemView>().Init(item,(data)=>{
                updateDetailsPanel(data);
            });
        }
    }
    public void updateDetailsPanel(Item.Data data)
    {

        // TODO Update Based on Type of Data
        inventory.RemoveFromInventory(data,2,invmanager.InstantiateItem);
        Debug.Log(data.UID);
        nameFieldObject.text=data.name;
        typeFieldObject.text=data.tag.ToString();
    }
    /*
     UseItem(int ID)
     DropItem(int ID,amount) 
     */
}

}
