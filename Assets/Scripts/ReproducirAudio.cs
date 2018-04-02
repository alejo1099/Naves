using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirAudio : MonoBehaviour
{
    public static ReproducirAudio ReproductorAudio;

    //Singleton del gameobject, y Donstroy Onload, para que el audio que tiene este gameObject no sea interrumpido
    void Awake()
    {
        if (!ReproductorAudio)
        {
            ReproductorAudio = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
