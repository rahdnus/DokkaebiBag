using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DokkaebiBag.Generic{

public class ItemManager : MonoBehaviour
{
    void Start()
    {
       foreach(Item item in  FindObjectsOfType<Item>())
       {
           item.Init();
       }
    }
}
}
