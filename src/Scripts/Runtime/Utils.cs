using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

namespace DokkaebiBag.Utils{
public class Utils 
{
    // public static Utils instance;
    // public static Utils Instance{
    //     get{
    //         if(instance==null)
    //             instance=new Utils();
    //         return instance;
    //     }
    // }
    public static System.Type[] getAllLeafTypesof<T>()
    {
        List<System.Type> leafClasses=new List<System.Type>();
        foreach(System.Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(mytype=>mytype.IsSubclassOf(typeof(T))))
        {
            bool flag=false;
            foreach(System.Type subtype in Assembly.GetAssembly(typeof(T)).GetTypes().Where(mytype=>mytype.IsSubclassOf(type)))
            {
                flag=true;
                break;
            }
            if(flag==false)
                leafClasses.Add(type);
        }
        
        return leafClasses.ToArray();
    }
    public static FieldInfo[] getAllFieldsof(System.Type type)
    {
        return type.GetFields();
    }
    public static IEnumerator delay(float waittime,System.Action<bool> delayedRepsone){
        yield return new WaitForSeconds(waittime);
        delayedRepsone(true);
    }
    public static IEnumerator delay(float waittime,System.Action<int> delayedRepsone){
        yield return new WaitForSeconds(waittime);
        delayedRepsone(0);
    }
}
}