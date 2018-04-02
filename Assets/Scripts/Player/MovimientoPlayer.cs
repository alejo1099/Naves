using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MovimientoPlayer : MonoBehaviour
{
    public static MovimientoPlayer ReferenciaMovimientoPlayer;

    private Transform transformPlayer;

    private Rigidbody rigidbodyPlayer;

    public GameObject particulas;

    private Camera camaraPrincipal;

    [HideInInspector]
    public Vector3 limitesPlayer;

    public Collider[] colliders;

    public Image[] imagenesVidas;

    public float velocidadPlayer;

    public float cantidadSalud = 300;
    private float cantidadVidas = 3;
    private int imagenActual;

    //Referencias principales tales como la camara. rigidbody, los limites de la pantalla actual, y un singleton de este script
    private void Awake()
    {
        if (!ReferenciaMovimientoPlayer)
            ReferenciaMovimientoPlayer = this;
        else
            Destroy(gameObject);

        camaraPrincipal = Camera.main;
        transformPlayer = transform;
        rigidbodyPlayer = GetComponent<Rigidbody>();

        limitesPlayer = camaraPrincipal.ViewportToWorldPoint(new Vector3(0, 0, transformPlayer.position.y));

        limitesPlayer.x += 0.5f;
        limitesPlayer.z += 0.5f;
    }

    //Constante actualizacion de metodos
    void Update()
    {
        MoverPlayer();
        VerificarLimitesPlayer();
        Verificarvida();
    }

    //Limita la posicion del jugador para que no salga de la pantalla
    private void VerificarLimitesPlayer()
    {
        rigidbodyPlayer.position = new Vector3(Mathf.Clamp(rigidbodyPlayer.position.x,
            limitesPlayer.x, -limitesPlayer.x), rigidbodyPlayer.position.y, Mathf.Clamp(
                rigidbodyPlayer.position.z, limitesPlayer.z, -limitesPlayer.z));
    }

    //Movmiento de la nave dependiendo de la plataform, si es windows lo hara con las flechas, y si es android lo hara con un Joystick
    private void MoverPlayer()
    {
#if UNITY_STANDALONE_WIN
        rigidbodyPlayer.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * velocidadPlayer;
#elif UNITY_ANDROID
        rigidbodyPlayer.velocity = new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0, CrossPlatformInputManager.GetAxisRaw("Vertical")).normalized * velocidadPlayer;
#endif
    }

    //Constante verificacion de la vida y salud del player, si la salud del player llega a 0 entonces se le rebaja una vida, y si las vidas son 0 entonces se destruye el gameObject
    private void Verificarvida()
    {
        VerificarImagenes();
        if (cantidadSalud <= 0)
        {
            cantidadVidas--;
            if (cantidadVidas <= 0)
            {
                Destroy(Instantiate(SpawnerEnemigos.ControladorEnemigos.explosion,transform.position,Quaternion.identity),2);
                GetComponent<DisparoPlayer>().controladorSonidos[2].Play();
                Destroy(gameObject);
            }
            else
            {
                imagenesVidas[imagenActual].enabled = false;
                imagenActual++;
                cantidadSalud = 300;
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = false;
                }
                StartCoroutine(DesactivarColliders());
            }
        }
    }

    //se verifican las imagenes y determina el fillamoun que es la cantidad que se dibuja esta en la escena, parecido a un slider
    private void VerificarImagenes()
    {
        imagenesVidas[imagenActual].fillAmount = cantidadSalud / 300;
    }

    //Cuando se pierde una vida se desactivan los colliders para que sea invencible por 3 segundos
    private IEnumerator DesactivarColliders()
    {
        Destroy(Instantiate(particulas, transform.position, Quaternion.identity), 3);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }

    //se detectan los disparos que golpearon al jugador, y si es una nave enemiga inmediatamente pierde una vida
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Disparo/Normal"))
        {
            cantidadSalud -= 5;
            GetComponent<DisparoPlayer>().controladorSonidos[3].Play();
        }
        else if (other.transform.CompareTag("Disparo/Fuerte"))
        {
            cantidadSalud -= 10;
            GetComponent<DisparoPlayer>().controladorSonidos[3].Play();
        }
        else
        {
            cantidadSalud = 0;
            GetComponent<DisparoPlayer>().controladorSonidos[3].Play();
        }
    }

    //Al perder las vidas se carga otra escena
    void OnDisable()
    {
        SceneManager.LoadScene(3);
    }
}