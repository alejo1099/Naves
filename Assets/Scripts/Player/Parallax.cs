using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform[] ciudades;
    public Transform[] nubes;

    public Transform[] ciudadesFondo;

    //Aqui se inicia la velocidad del parallax de la ciudad y las nubes, y si la plataforma es windows, entonces se activan mas ciudades para cubrir mas espacio en pantalla
    void Start()
    {
        for (int i = 0; i < ciudades.Length; i++)
        {
            ciudades[i].GetComponent<Rigidbody>().velocity = Vector3.forward * -1.5f;
        }
        for (int i = 0; i < nubes.Length; i++)
        {
            nubes[i].GetComponent<Rigidbody>().velocity = Vector3.right * -3.5f;
        }
#if UNITY_STANDALONE_WIN
        for (int i = 0; i < ciudadesFondo.Length; i++)
        {
            ciudadesFondo[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < ciudadesFondo.Length; i++)
        {
            ciudadesFondo[i].GetComponent<Rigidbody>().velocity = Vector3.forward * -1.5f;
        }
#endif        
    }

    //Si la plataforma es Windows entonces se verificaran las ciudades de fondo, sino solo se hara con las nubes y ciudades centrales
    void Update()
    {
        VerificarNubes();
        VerificarCiudades();
#if UNITY_STANDALONE_WIN
        VerificarFondo();
#endif
    }

    //Verificacion de ciudades donde si llega a una posicion determinada, entonces se cambiara su posicion para reutilizarse
    private void VerificarCiudades()
    {
        for (int i = 0; i < ciudades.Length; i++)
        {
            if (ciudades[i].position.z <= -15f)
            {
                ciudades[i].position += Vector3.forward * 30;
            }
        }
    }

    //Verificacion de nubes donde si llega a una posicion determinada, entonces se cambiara su posicion para reutilizarse
    private void VerificarNubes()
    {
        for (int i = 0; i < nubes.Length; i++)
        {
            if (nubes[i].position.x <= -10)
            {
                nubes[i].position += Vector3.right * 20;
            }
        }
    }

    //Verificacion de ciudades donde si llega a una posicion determinada, entonces se cambiara su posicion para reutilizarse
    private void VerificarFondo()
    {
        for (int i = 0; i < ciudadesFondo.Length; i++)
        {
            if (ciudadesFondo[i].position.z <= -15f)
            {
                ciudadesFondo[i].position += Vector3.forward * 30;
            }
        }
    }
}
