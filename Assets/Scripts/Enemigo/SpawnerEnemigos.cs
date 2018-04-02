using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public static SpawnerEnemigos ControladorEnemigos;

    public GameObject enemigoBasico;
    public GameObject enemigoFuerte;
    public GameObject boss;
	public GameObject disparo;
	public GameObject ataqueFuerte;
    public GameObject explosion;

    private GameObject[] enemigosBasicos;
    private GameObject[] enemigosFuertes;
    private GameObject[] jefes;

    public AudioSource[] controladorSonidos;

    [HideInInspector]
    public Vector3 posicionInstancia;
    private Vector3 posicionRelativa;

    //Singleton de este script
    void Awake()
    {
        if (!ControladorEnemigos)
            ControladorEnemigos = this;
        else
            Destroy(gameObject);
    }

    //Obtencion de variables como la posicion global de los limites de pantalla, ademas de llamada a metodos necesarios para el juego
    void Start()
    {
        posicionInstancia = MovimientoPlayer.ReferenciaMovimientoPlayer.limitesPlayer;
        posicionInstancia.x += 0.5f;

        posicionRelativa.z = 6;
        posicionRelativa.y = 4.95f;

        EnemigoBasico();
        EnemigoFuerte();
        Boss();

        InvokeRepeating("ActivarEnemigoBasico", 5, 5);
        InvokeRepeating("ActivarEnemigoFuerte", 20, 15);
        InvokeRepeating("ActivarBoss", 30, 45);
    }

    //Metodo que instancia objetos y los desactiva
    private void InstanciarEnemigos(GameObject enemigoInstanciar,ref GameObject[] arrayAlmacenaje)
    {
        arrayAlmacenaje = new GameObject[30];
        for (int i = 0; i < arrayAlmacenaje.Length; i++)
        {
            arrayAlmacenaje[i] = Instantiate(enemigoInstanciar, Vector3.zero, enemigoInstanciar.transform.rotation);
            arrayAlmacenaje[i].transform.SetParent(transform.GetChild(0));
            arrayAlmacenaje[i].SetActive(false);
        }
        
    }

    //Este metodo invoka otro que instanciara enemigos normales
    private void EnemigoBasico()
    {
        InstanciarEnemigos(enemigoBasico,ref enemigosBasicos);
    }
    
    //Este metodo invoka otro que instanciara enemigos fuertes
    private void EnemigoFuerte()
    {
        InstanciarEnemigos(enemigoFuerte,ref enemigosFuertes);
    }

    //Este metodo invoka otro que instanciara jefes
    private void Boss()
    {
        InstanciarEnemigos(boss, ref jefes);
    }

    //Metodo para activar un enemigo normal
    private void ActivarEnemigoBasico()
    {
        ActivarObjeto(enemigosBasicos);
    }

    //Metodo para activar un enemigo fuerte
    private void ActivarEnemigoFuerte()
    {
        ActivarObjeto(enemigosFuertes);
    }

    //Metodo para activar un jefe
    private void ActivarBoss()
    {
        ActivarObjeto(jefes);
    }

    //Metodo que busca si un objeto esta activado, y si no lo esta lo activa en la escena, en una posicion dada
    private void ActivarObjeto(GameObject[] almacenajeMostrar)
    {
        for (int i = 0; i < almacenajeMostrar.Length; i++)
        {
            if(!almacenajeMostrar[i].activeInHierarchy)
            {
                posicionRelativa.x = Random.Range(posicionInstancia.x, -posicionInstancia.x);
                almacenajeMostrar[i].transform.position = posicionRelativa;
                almacenajeMostrar[i].AddComponent<DisparoEnemigo>();
                almacenajeMostrar[i].SetActive(true);
                return;
            }
        }
    }
}