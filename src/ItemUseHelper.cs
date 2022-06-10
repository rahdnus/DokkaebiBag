using UnityEditor;
using UnityEngine;

public class ItemUseHelper 
{
    private static ItemUseHelper instance;
    public static ItemUseHelper Instance
    {
        get
        {
            if (instance == null)
                instance = new ItemUseHelper();
            return instance;
        }
    }
}
