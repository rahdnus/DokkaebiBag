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
    [SerializeField]InventoryUIManager manager;

    List<ItemView> Items=new List<ItemView>();
    void Start()
    {
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

             Addressables.LoadAssetAsync<Sprite>(manager.GetAsset(item.RID).spritereference).Completed+=(op)=>{
                if(op.Status==UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    itemObject.GetComponentInChildren<Image>().sprite=op.Result;
                }
                else
                {
                    Debug.Log(op.OperationException);
                }
        };

            itemObject.GetComponent<ItemView>().Init(item,(data)=>{
                updateDetailsPanel(data);
            });
        }
    }
    public void updateDetailsPanel(Item.Data data)
    {

        // TODO Update Based on Type of Data
        nameFieldObject.text=data.name;
        typeFieldObject.text=data.mainTag.ToString();
    }
    /*
     UseItem(int ID)
     DropItem(int ID,amount) 
     */
}

}
