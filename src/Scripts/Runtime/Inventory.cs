using System;
using System.Collections.Generic;
using UnityEngine;
namespace DokkaebiBag.Generic{
    [System.Serializable]
public class Inventory :MonoBehaviour
{
    public List<Item.Data> Items=new List<Item.Data>();

    System.Action<string> onAdd;
    Item.Data temp1,temp2;
    public void Start()
    {
        Utils.Utils.getAllLeafTypesof<Item.Data>();
        temp1=new Item.Consumable("01","","Hi",Stacking.X16,MainTag.Weapon,SubTag.Gun,4);
        temp2=new Item.Consumable("02","","Hello",Stacking.X1,MainTag.Weapon,SubTag.Gun,1);
        // AddToInventory(temp2);
    }
    public void Init(System.Action<string> _onAdd)
    {
        this.onAdd=_onAdd;
    }
    public void Click()
    {/* TEMP */
        if(Input.GetMouseButtonDown(0))
        Debug.Log((Items[1] as Item.Consumable ).count); 
    }
    public void RightClick()
    {
        if(Input.GetMouseButtonDown(1))
        {
            // AddToInventory(temp1);
        }
    }
    public void AddToInventory(Item.Data item)
    {
        FindItem(
            item,
            (search,data)=>{
                if(search is Item.Stackable && data is Item.Stackable)
                {
                    Item.Stackable tempStack=data as Item.Stackable;
                    if(search.RID==data.RID && tempStack.count!=(int)tempStack.stacking)
                    {
                        // Debug.Log(tempStack.count+" "+(int)tempStack.stacking);
                        return true;
                    }
                    // Debug.LogError("DEFINE EVALUATOR FOR ADD");
                }
                return false;
            },
            (olditem)=>{
                
                onAdd(olditem.RID);
                
                if(olditem is Item.Stackable)
                { 
                    Item.Stackable temp1=item as Item.Stackable;
                    Item.Stackable temp2=olditem as Item.Stackable;
                    //  Debug.Log(temp1.count);
                    int remainder=0;
                    if(temp1.count+temp2.count>(int)temp2.stacking)
                        remainder=(temp2.count+temp1.count)%(int)temp2.stacking;
                    temp2.count=(temp2.count+  temp1.count)-remainder;
                    // Debug.Log(temp2.count);

                    // Debug.Log(remainder);

                    if(remainder>0)
                    {
                        Item.Data newitem=null;
                        if(item is Item.Consumable)
                        {
                            
                            newitem=new Item.Consumable(item as Item.Consumable);
                            newitem.assignUID(Items.Count.ToString("0000"));
                            Items.Add(newitem);
                        }
                    }
                }
                else
                {
                    Item.Data newitem=null;
                    if(item is Item.Consumable)
                    {
                        newitem=new Item.Consumable(item as Item.Consumable);
                        newitem.assignUID(Items.Count.ToString("0000"));
                        Items.Add(newitem);
                    }

                }
            },
            ()=>{

                onAdd(item.RID);

                Item.Data newitem=null;
                    if(item is Item.Consumable)
                    {
                        newitem=new Item.Consumable(item as Item.Consumable);
                        newitem.assignUID(Items.Count.ToString("0000"));
                        Items.Add(newitem);
                    }
                    if(!(item is Item.Consumable))
                    {
                        Debug.LogError("Add code for other Item.data types as well");
                    }
            }
        );
        // 
    }
    public void RemoveFromInventory(Item.Data item,int count=1,System.Action<string,int,Vector3> onRemove=null)
    {
       FindItem(
           item,
             (search,data)=>{
                if(search is Item.Stackable)
                {
                    if(search.UID==data.UID)
                    {
                        return true;
                    }
                    return false;
                    // Debug.LogError("DEFINE EVALUATOR FOR REMOVE");
                }
                return false;
             }
            ,
           (olditem)=>{
               if(olditem is Item.NonStackable)
               {
                   Items.Remove(olditem);
                   onRemove(olditem.RID,1,transform.position);  
               }
                else
                {
                    Item.Stackable temp=olditem as Item.Stackable;
                    if(count<=(int)temp.stacking)
                    {
                        if(temp.count-count>0)
                        {
                            temp.count-=count;

                            Debug.Log(temp.count);
                            onRemove(temp.RID,count,transform.position);  
                            return;
                        }
                        
                        onRemove(temp.RID,count,transform.position);  
                        Items.Remove(temp);
                    }

                }
            },
            ()=>{
                Debug.LogError(item.RID+" not found");
            }
       );
    }
    private bool FindItem(Item.Data search,System.Func<Item.Data,Item.Data,bool> evaluator,System.Action<Item.Data> onFound=null,System.Action onNotFound=null)
    {
        foreach(Item.Data data in Items)
        {
            if(evaluator(search,data))
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