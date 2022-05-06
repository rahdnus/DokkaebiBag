using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DokkaebiBag.Generic{
public enum Stackable{X0, X16, X32, X64}
public enum ItemType{Weapon,Accessory,Armor,Consumable}
public class Item : MonoBehaviour
{
   [SerializeField] private string id;
    public string _ID{
        get
        {
            if(id==null)
            {
                Debug.LogError("No ID assigned to "+ gameObject.name);
                return null;
            }
            return id;
        }
        set{}
    }
    [SerializeField] private string description;
    public string _Description{
        get
        {
            if(description==null)
            {
                Debug.LogError("No description assigned to "+ gameObject.name);
                return null;
            }
            return description;
        }
        set{}
    }
    [SerializeField] private Stackable stack;
    public Stackable _Stack{
        get
        {
            return stack;
        }
        set{}
    }
    [SerializeField] private ItemType itype;
    public ItemType _ItemType{
        get
        {
            return itype;
        }
        set{}
    }
    public int _Count=1;

    public void OnTriggerEnter2D(Collider2D other)
    {
    }
    public void OnTriggerEnter(Collider other)
    {
    }
}
}
