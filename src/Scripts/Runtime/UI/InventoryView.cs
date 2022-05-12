using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DokkaebiBag.Generic;
namespace DokkaebiBag.UI{
public class InventoryView : MonoBehaviour
{
    [SerializeField]GameObject itemPrefab,inventoryPanel;
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
            itemObject.GetComponent<ItemView>().Init(item);
        }
    }
    
    /*
     UseItem(int ID)
     DropItem(int ID,amount) 
     */
}

}
