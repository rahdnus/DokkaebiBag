using UnityEngine;
using DokkaebiBag.Generic;

namespace DokkaebiBag.UI{
public class ItemView : MonoBehaviour
{
    public int GUID;
    public IVector2 position;
    private Item item;
    public void Init(Item _item)
    {
        item=_item;
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
