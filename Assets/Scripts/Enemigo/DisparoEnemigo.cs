using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{

    private Rigidbody rigidbodyEnemigo;

    private Transform[] posicionesDisparo;

    private int cantidadSalud;


    //Se inicializan las variables obteniendo referencias
    void Awake()
    {
        rigidbodyEnemigo = GetComponent<Rigidbody>();
        posicionesDisparo = new Transform[transform.childCount];
        for (int i = 0; i < posicionesDisparo.Length; i++)
        {
            posicionesDisparo[i] = transform.GetChild(i);
        }
    }

    //Se identifica en que tipo de enemigo esta este script, y se inicializan sus variables propias, como salud, velocidad, y direccion
    void Start()
    {
        if (gameObject.tag == "Enemigo/Basico")
        {
            cantidadSalud = 150;
            rigidbodyEnemigo.velocity = -Vector3.forward * 1;
            InvokeRepeating("DisparoBasico", 1, 1.5f);
        }
        else if (gameObject.tag == "Enemigo/Fuerte")
        {
            cantidadSalud = 250;
            Vector3 posicionUno = SpawnerEnemigos.ControladorEnemigos.posicionInstancia;
            posicionUno.y = 4.95f;
            posicionUno.x = -posicionUno.x;
            Vector3 posicionDos = SpawnerEnemigos.ControladorEnemigos.posicionInstancia;
            posicionDos.y = 4.95f;
            Vector3 posicionRelativa = Random.Range(0,2) == 0 ? (posicionUno - transform.position).normalized : (posicionDos - transform.position).normalized;

            rigidbodyEnemigo.velocity = posicionRelativa * 2f;
            InvokeRepeating("DisparoFuerte", 3, 1);
        }
        else if (gameObject.tag == "Enemigo/Boss")
        {
            cantidadSalud = 800;
            rigidbodyEnemigo.velocity = -Vector3.forward * 0.25f;
            InvokeRepeating("DisparoFuerte", 3, 3.5f);
            InvokeRepeating("DisparoBasico", 3, 1.75f);
        }
    }

    //se verifica la salud constantemente
    void Update()
    {
        VerificarSalud();
    }

    //Verificador de salud, que al llegar a 0 activa los efectos sonoros y visuales
    private void VerificarSalud()
    {
        if (cantidadSalud <= 0)
        {
            Destroy(Instantiate(SpawnerEnemigos.ControladorEnemigos.explosion,transform.position,Quaternion.identity),2);
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[2].Play();
            gameObject.SetActive(false);
            Destroy(this);
        }
    }

    //Cuando el objeto ya no este visto por la camara se desactivara y se destruira este script para volver a usar la nave
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }

    //Disparo basico de lasers normales, con su respectivo efecto sonoro, y su velocidad
    private void DisparoBasico()
    {
        for (int i = 0; i < posicionesDisparo.Length; i++)
        {
            Rigidbody rb = Instantiate(SpawnerEnemigos.ControladorEnemigos.disparo, posicionesDisparo[i].position, posicionesDisparo[i].rotation).GetComponent<Rigidbody>();
            rb.velocity = rb.transform.forward * 5;
            Destroy(rb.gameObject, 3);
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[0].Play();
        }
    }

    //Disparo fuerte de lasers fuerte, con su respectivo efecto sonoro, y su velocidad
    private void DisparoFuerte()
    {
        for (int i = 0; i < posicionesDisparo.Length; i++)
        {
            Rigidbody rb = Instantiate(SpawnerEnemigos.ControladorEnemigos.ataqueFuerte, posicionesDisparo[i].position, posicionesDisparo[i].rotation).GetComponent<Rigidbody>();
            rb.velocity = rb.transform.forward * 3;
            Destroy(rb.gameObject, 5);
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[1].Play();
        }
    }

    //Detector de colisiones enemigas, como los lasers y su respectiva disminucion de salud
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Disparo/Normal")
        {
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[3].Play();
            cantidadSalud -= 5;
        }
        else if (other.transform.tag == "Disparo/Fuerte")
        {
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[3].Play();
            cantidadSalud -= 10;
        }
        else if (this.tag == "Enemigo/Boss" && other.transform.tag == "Player")
        {
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[3].Play();
            cantidadSalud -= 30;
            Destroy(other.gameObject);
        }
        else
        {
            SpawnerEnemigos.ControladorEnemigos.controladorSonidos[3].Play();
            cantidadSalud = 0;
        }
    }
}