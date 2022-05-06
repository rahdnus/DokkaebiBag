using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DokkaebiBag.Generic{
public enum Stacking{X1=1, X16=16, X32=32, X64=64}
public enum MainTag{Weapon,Armor,Accessory,Consumable}
public enum SubTag
{
    Sword,Shield,Gun,Bow,
    Helmet,Torso,Grieves,Gloves,
    Ring,Pendant,
    Potions,Ammo
}
public class Item : MonoBehaviour
{
    [SerializeField] Data data;
/*    [SerializeField] private string id;
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
    [SerializeField] private Stacking stack;
    public Stacking _Stack{
        get
        {
            return stack;
        }
        set{}
    }
    [SerializeField] private MainTag maintag;
    public MainTag _MainTag{
        get
        {
            return maintag;
        }
        set{}
    }
    [SerializeField] private SubTag subtag;
    public SubTag _SubTag{
        get
        {
            return subtag;
        }
        set{}
    } */
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.GetComponent<IPick>()!=null)
        {
            other.gameObject.GetComponent<IPick>().PickUp(this.data);
            Destroy(gameObject);
        }
        else if(other.gameObject.GetComponentInParent<IPick>()!=null)
        {
            other.gameObject.GetComponentInParent<IPick>().PickUp(this.data);
            Destroy(gameObject);
        }
        else if(other.gameObject.GetComponentInChildren<IPick>()!=null)
        {
            other.gameObject.GetComponentInChildren<IPick>().PickUp(this.data);
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
         Debug.Log(other.gameObject.name);
        if(other.gameObject.GetComponent<IPick>()!=null)
        {
            other.gameObject.GetComponent<IPick>().PickUp(this.data);
            Destroy(gameObject);
        }
        else if(other.gameObject.GetComponentInParent<IPick>()!=null)
        {
            other.gameObject.GetComponentInParent<IPick>().PickUp(this.data);
            Destroy(gameObject);
        }
        else if(other.gameObject.GetComponentInChildren<IPick>()!=null)
        {
            other.gameObject.GetComponentInChildren<IPick>().PickUp(this.data);
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class Data
    {
        public string ID,description;
        public Stacking stacking;
        public MainTag mainTag;
        public SubTag subTag;
        public int count=1;
        public Data(string ID,string description,Stacking stacking,MainTag mainTag,SubTag subTag,int count)
        {
            this.description=description;
            this.stacking=stacking;
            this.mainTag=mainTag;
            this.subTag=subTag;
            this.count=count;
            this.ID=ID;
        }
        public Data(Item.Data _data)
        {
            description=_data.description;
            stacking=_data.stacking;
            mainTag=_data.mainTag;
            subTag=_data.subTag;
            count=_data.count;
            ID=_data.ID;
        }
      
    }
}

public interface IPick
{
    public void PickUp(Item.Data data);
}
}
