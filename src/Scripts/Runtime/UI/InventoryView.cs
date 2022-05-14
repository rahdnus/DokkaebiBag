using UnityEngine.UI;
using TMPro;
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
            itemObject.GetComponent<ItemView>().Init(item,(data)=>{
                updateDetailsPanel(data);
            });
        }
    }
    public void updateDetailsPanel(Item.Data data)
    {
        nameFieldObject.text=data.name;
        typeFieldObject.text=data.mainTag.ToString();
    }
    /*
     UseItem(int ID)
     DropItem(int ID,amount) 
     */
}

}
