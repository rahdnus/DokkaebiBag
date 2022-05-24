using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DokkaebiBag.Utils{
public class Utils 
{
    public static Utils instance;

    public static Utils Instance{
        get{
            if(instance==null)
                instance=new Utils();
            return instance;
        }
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