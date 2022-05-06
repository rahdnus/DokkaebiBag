using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DokkaebiBag.Generic{
    [System.Serializable]
public class Inventory 
{
    private List<Item.Data> Items=new List<Item.Data>();
    Item.Data temp1,temp2;
    public void Init()
    {
        temp1=new Item.Data("01","Hi",Stacking.X16,MainTag.Weapon,SubTag.Gun,4);
        temp2=new Item.Data("02","Hello",Stacking.X1,MainTag.Weapon,SubTag.Gun,1);

        AddToInventory(temp1);
        AddToInventory(temp2);

    }
    public void Click()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RemoveFromInventory("01",3);
        }
    }
    public void AddToInventory(Item.Data item)
    {
        FindItem(
            item.ID,
            (olditem)=>{
                int remainder=(olditem.count+item.count)%(int)olditem.stacking;
                olditem.count=(olditem.count+item.count)-remainder;
                Item.Data newitem=new Item.Data(item);
                newitem.count=remainder;
                Items.Add(newitem);
            },
            ()=>{
                Items.Add(item);
            }
        );
        // 
    }
    public void RemoveFromInventory(string ID,int count=1)
    {
       FindItem(
           ID,
           (item)=>{
               if(item.stacking==Stacking.X1)
               {Items.Remove(item);  }
                else
                {
                    if(count<=(int)item.stacking)
                    {
                        if(item.count-count>0)
                        {
                            item.count-=count;
                            return;
                        }
                        Items.Remove(item);
                    }
                }
            },
            ()=>{
                Debug.LogError(ID+" not found");
            }
       );
    }
    private bool FindItem(string ID,System.Action<Item.Data> onFound=null,System.Action onNotFound=null)
    {
        foreach(Item.Data data in Items)
        {
            if(data.ID==ID)
            {
                if(onFound!=null)
                    onFound(data);

                return true;
            }
        }
        onNotFound();
        return false;
    }

}
}