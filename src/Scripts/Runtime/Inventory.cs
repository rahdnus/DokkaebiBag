using System;
using System.Collections.Generic;
using UnityEngine;
namespace DokkaebiBag.Generic{
    [System.Serializable]
public class Inventory :MonoBehaviour
{
    public List<Item.Data> Items=new List<Item.Data>();

    System.Action<string> onAdd;
    public void Start()
    {
        Utils.Utils.getAllLeafTypesof<Item.Data>();
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
                        return true;
                    }
                }
                return false;
            },
            (olditem)=>{
                
                onAdd(olditem.RID);
                
                if(olditem is Item.Stackable)
                { 
                    Item.Stackable stack_item=item as Item.Stackable;
                    Item.Stackable stack_olditem=olditem as Item.Stackable;

                    int remainder=0;
                    
                    if(stack_item.count+stack_olditem.count>(int)stack_olditem.stacking)
                        remainder=(stack_olditem.count+stack_item.count)%(int)stack_olditem.stacking;
                    stack_olditem.count=(stack_olditem.count+  stack_item.count)-remainder;
                    stack_item.count = remainder;
                    if(remainder>0)
                    {
                        Item.Data newitem= item;
                        Debug.Log(newitem.GetType());
                        newitem.assignUID(Items.Count.ToString("0000"));
                        Items.Add(newitem);
                        #region manualassignment
                        /*
                        if (item is Item.Consumable)
                        {
                            //newitem=new Item.Consumable(item as Item.Consumable);
                        }
                        */
                        #endregion
                    }
                }
                else
                {
                    Item.Data newitem= item;
                    Debug.Log(newitem.GetType());
                    newitem.assignUID(Items.Count.ToString("0000"));
                    Items.Add(newitem);
                    #region manualassignment
                    /*    if (item is Item.Consumable)
                        {
                            //newitem=new Item.Consumable(item as Item.Consumable
                        }
                    */
                    #endregion
                }
            },
            ()=>{

                onAdd(item.RID);

                Item.Data newitem=null;
                newitem = item;
                Debug.Log(newitem.GetType());
                newitem.assignUID(Items.Count.ToString("0000"));
                Items.Add(newitem);

                #region manualassingment
                /*
                if (item is Item.Consumable)
                    {
                    //    newitem=new Item.Consumable(item as Item.Consumable);
                       
                    }
                    if(!(item is Item.Consumable))
                    {
                        Debug.LogError("Add code for other Item.data types as well");
                    }
                */
                #endregion
            }
        );
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
                    Item.Stackable stack_olditem=olditem as Item.Stackable;
                    if(count<=(int)stack_olditem.stacking)
                    {
                        if(stack_olditem.count-count>0)
                        {
                            stack_olditem.count-=count;

                            Debug.Log(stack_olditem.count);
                            onRemove(stack_olditem.RID,count,transform.position);  
                            return;
                        }
                        
                        onRemove(stack_olditem.RID,count,transform.position);  
                        Items.Remove(stack_olditem);
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