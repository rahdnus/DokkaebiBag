using UnityEngine;
using UnityEngine.EventSystems;
using DokkaebiBag.Generic;

namespace DokkaebiBag.UI{
public class ItemView : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{
    public int GUID;
    public IVector2 position;
    Item.Data data;

    System.Action<Item.Data> onSelect=null;
    public void Init(Item.Data _data,System.Action<Item.Data> _onClick)
    {
       this.data=_data;
       this.onSelect=_onClick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelect(this.data);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);    
    }
        /*
void Move()
void Display()->display description and info
void Discard()->Inventory.Remove()
void Use()
*/
    }
}
namespace DokkaebiBag.Generic{
    public class IVector2{
    public int x;
    public int y;
    public IVector2(int _x=0,int _y=0)
    {
        this.x=_x;
        this.y=_y;
    }
    public static IVector2 operator+(IVector2 A,IVector2 B)
    {
        IVector2 vec=new IVector2();
        vec.x=A.x+B.x;
        vec.y=A.y+B.y;
        return vec;
    }
      public static IVector2 operator-(IVector2 A,IVector2 B)
    {
        IVector2 vec=new IVector2();
        vec.x=A.x-B.x;
        vec.y=A.y-B.y;
        return vec;
    }
    public static IVector2 operator*(IVector2 A,int mult)
    {
        IVector2 vec=new IVector2();
        vec.x=A.x*mult;
        vec.y=A.y*mult;
        return vec;
    }
     public static IVector2 operator/(IVector2 A,int div)
    {
        IVector2 vec=new IVector2();
        vec.x=A.x/div;
        vec.y=A.y/div;
        return vec;
    }
     public static IVector2 operator%(IVector2 A,int div)
    {
        IVector2 vec=new IVector2();
        vec.x=A.x%div;
        vec.y=A.y%div;
        return vec;
    }
}
}
