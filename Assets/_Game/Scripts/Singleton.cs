using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T ins;
    
    public static T Ins
    {
        get
        {
            if (ins == null)
            {
                //find singleton
                ins = Object.FindFirstObjectByType<T>();

                //Create new instance if one doesn't already exist
                if(ins == null)
                {
                    //Need to create a new GameObject to attach the singleton to.
                    ins = new GameObject(nameof(T)).AddComponent<T>();
                }
            }
            return ins;
        }
    }
}
