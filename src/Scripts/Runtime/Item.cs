using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DokkaebiBag.Generic{
public enum Stacking{X1=1,X16=16, X32=32, X64=64}
// public enum MainTag{Weapon,Armor,Accessory,Consumable}
public enum Tag
{
    Sword,Shield,Gun,Bow,
    Helmet,Torso,Grieves,Gloves,
    Ring,Pendant,
    Potions,Ammo
}
public class Item : MonoBehaviour
{
    public string RID;
    public int count=8;
    Data data;
    // new Item.Consumable("01","temp","Hi",Stacking.X16,MainTag.Weapon,SubTag.Gun,4);

    public Data myData{
        get{
            if(data==null)
                Debug.Log("Data is Null");
            return data;
        }
        
    }
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
    public void Init()
    {
        data=new Item.Consumable(RID,"temp","Hi",Stacking.X16/* ,MainTag.Weapon, */,Tag.Gun,count);
        // data.UID=data.generateUID();
    }
    public void setTrigger(bool state)
    {
        GetComponent<Collider2D>().enabled=state;
    }
    
    
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name);
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
        public string name;
        public string RID,UID,description;
        // public MainTag mainTag;
        public Tag tag;
        public Data(string ID,string name,string description,/* MainTag mainTag, */Tag tag)
        {
            this.description=description;
            // this.mainTag=mainTag;
            this.tag=tag;
            this.name=name;
            this.RID=ID;
        }
        public Data(Item.Data _data)
        {
            description=_data.description;
            // mainTag=_data.mainTag;
            tag=_data.tag;
            name=_data.name;
            UID=_data.UID;
            RID=_data.RID;
        }
        public void AssignRID(string ID)
        {
            this.RID=ID;
            
        }
        public void assignUID(string num)
        {
            this.UID=num;
        }
      
    }
    [System.Serializable]
    public class Stackable:Data
    {
        public Stacking stacking;
        public int count=1;


        public Stackable(string ID,string name,string description,Stacking stacking,/* MainTag mainTag, */Tag tag,int count):
            base(ID,name,description/*, mainTag */,tag)
        {
            this.stacking=stacking;
            this.count=count;
            // Debug.Log(count);

        }
         public Stackable(Item.Stackable _data):base(_data)
        {
            stacking=_data.stacking;
            count=_data.count;
                // Debug.Log(count);

        }
    }
     [System.Serializable]
    public class NonStackable:Data
    {
        public NonStackable(string ID,string name,string description,/* MainTag mainTag, */Tag tag):
            base(ID,name,description,/* mainTag, */tag)
        {     }
         public NonStackable(Item.NonStackable _data):base(_data)
        {     }
    }
         [System.Serializable]
    public class Weapon:NonStackable
    {
        public int damage;
        public int lvl;
        public Weapon(string ID,string name,string description,/* MainTag mainTag, */Tag tag,int damage,int lvl):
            base(ID,name,description,/* mainTag, */tag)
        {
            this.damage=damage;
            this.lvl=damage;
        }
         public Weapon(Item.Weapon _data):base(_data)
        {
            damage=_data.damage;
            lvl=_data.damage;
        }
    }
         [System.Serializable]
    public class Armor:NonStackable
    {
        public int defense;
        public int lvl;
        public Armor(string ID,string name,string description,/* MainTag mainTag, */Tag tag,int defense,int lvl):
            base(ID,name,description,/* mainTag, */tag)
        {
            this.defense=defense;
            this.lvl=lvl;
        }
         public Armor(Item.Armor _data):base(_data)
        {
            defense=_data.defense;
            lvl=_data.lvl;
        }
    }
    public class Consumable:Stackable
    {
        public Consumable(string ID,string name,string description,Stacking stacking,/* MainTag mainTag, */Tag tag,int count):
            base(ID,name,description,stacking,/* mainTag, */tag,count)
        {
        }
         public Consumable(Item.Consumable _data):base(_data)
        {
        }
    }
}

public interface IPick
{
    public void PickUp(Item.Data data);
}
}
