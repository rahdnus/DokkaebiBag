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
    [SerializeField] Data data=new Item.Consumable("01","Hi",Stacking.X16,MainTag.Weapon,SubTag.Gun,4);

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
    void Awake()
    {
        data.UID=data.generateUID();
    }
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
        public string RID,UID,description;
        public MainTag mainTag;
        public SubTag subTag;
        public Data(string ID,string description,MainTag mainTag,SubTag subTag)
        {
            this.description=description;
            this.mainTag=mainTag;
            this.subTag=subTag;
            this.RID=ID;
        }
        public Data(Item.Data _data)
        {
            description=_data.description;
            mainTag=_data.mainTag;
            subTag=_data.subTag;
            UID=_data.UID;
            RID=_data.RID;
        }
        public void AssignRID(string ID)
        {
            this.RID=ID;
            
        }
        public string generateUID()
        {
            Random.InitState(10101);
            return Random.Range(0,1000000).ToString();/* TEMP */
        }
      
    }
    [System.Serializable]
    public class Stackable:Data
    {
        public Stacking stacking;
        public int count=1;


        public Stackable(string ID,string description,Stacking stacking,MainTag mainTag,SubTag subTag,int count):
            base(ID,description,mainTag,subTag)
        {
            this.stacking=stacking;
            this.count=count;
        }
         public Stackable(Item.Stackable _data):base(_data)
        {
            stacking=_data.stacking;
            count=_data.count;
        }
    }
     [System.Serializable]
    public class NonStackable:Data
    {
        public NonStackable(string ID,string description,MainTag mainTag,SubTag subTag):
            base(ID,description,mainTag,subTag)
        {     }
         public NonStackable(Item.NonStackable _data):base(_data)
        {     }
    }
         [System.Serializable]
    public class Weapon:NonStackable
    {
        public int damage;
        public int lvl;
        public Weapon(string ID,string description,MainTag mainTag,SubTag subTag,int damage,int lvl):
            base(ID,description,mainTag,subTag)
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
        public Armor(string ID,string description,MainTag mainTag,SubTag subTag,int defense,int lvl):
            base(ID,description,mainTag,subTag)
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
        public Consumable(string ID,string description,Stacking stacking,MainTag mainTag,SubTag subTag,int count):
            base(ID,description,stacking,mainTag,subTag,count)
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
