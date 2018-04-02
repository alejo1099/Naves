using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DisparoPlayer : MonoBehaviour
{

    public Transform[] posicionesDisparo;

    public GameObject bala;
    public GameObject balaFuerte;
    public GameObject joystick;

    public AudioSource[] controladorSonidos;

    public float velocidadBala;

    //Si la plataforma en android entonces se activaran los controles Joystick
    void Start()
    {
#if UNITY_ANDROID
        joystick.SetActive(true);
#endif
    }

    //Se verifican constantemente los Inputs del usuario
    void Update()
    {
        DispararBalaNormal();
        DispararBalaFuerte();
    }

    //Metodo para disparar un laser normal, dependiendo del dispositvo, si es Windows entonces es click izquierdo, y si es Android entonces es un boton
    //Ademas de su respectivo efecto sonoro
    private void DispararBalaNormal()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < posicionesDisparo.Length; i++)
            {
                controladorSonidos[0].Play();
                Rigidbody rb = Instantiate(bala, posicionesDisparo[i].position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.velocity = bala.transform.forward * velocidadBala;
                Destroy(rb.gameObject, 3);
            }
        }
#elif UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < posicionesDisparo.Length; i++)
            {
                controladorSonidos[0].Play();
                Rigidbody rb = Instantiate(bala, posicionesDisparo[i].position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.velocity = bala.transform.forward * velocidadBala;
                Destroy(rb.gameObject, 3);
            }
        }
#endif
    }

    //Metodo para disparar un laser fuerte, dependiendo del dispositvo, si es Windows entonces es click derecho, y si es Android entonces es un boton
    //Ademas de su respectivo efecto sonoro
    private void DispararBalaFuerte()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetButtonDown("Fire2"))
        {
            controladorSonidos[1].Play();
            Rigidbody rb = Instantiate(balaFuerte, posicionesDisparo[0].position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = bala.transform.forward * velocidadBala;
            Destroy(rb.gameObject, 3);
        }
#elif UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            controladorSonidos[1].Play();
            Rigidbody rb = Instantiate(balaFuerte, posicionesDisparo[0].position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = bala.transform.forward * velocidadBala;
            Destroy(rb.gameObject, 3);
        }
#endif
    }
}