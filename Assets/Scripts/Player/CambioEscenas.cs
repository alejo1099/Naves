using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioEscenas : MonoBehaviour
{
	public Image imagenCargar;

	public Text texto;

	private AudioSource audioBoton;

	//Metodo que determina si la refrencia de texto existe, y si es asi entonces cambia el texto de acuerdo a la pltaforma Windows o Android
	void Start()
	{
		audioBoton = GetComponent<AudioSource>();
		if(!texto)
		{
			return;
		}
#if UNITY_STANDALONE_WIN
        texto.text = "Destruye las naves que se \ninterpongan en tu camino, \nno importa el costo, lo unico \nimportante que debes hacer es \nderrotar al jefe.\n \nMuevete en todas direcciones \ncon las flechas, y dispara con \nlos botones del mouse.";
#elif UNITY_ANDROID
        texto.text = "Destruye las naves que se \ninterpongan en tu camino, \nno importa el costo, lo unico \nimportante que debes hacer es \nderrotar al jefe.\n \nMuevete en todas direcciones \ncon el Joystick, y dispara con \nlos botones.";
#endif		
	}

	//Activa una imagen y carga la escena del menu principal
	public void CargarMenuPrincipal()
	{
		audioBoton.Play();
		imagenCargar.enabled = true;
		//SceneManager.LoadScene(0);
		StartCoroutine(CargarEscena(0));
	}

	//Activa una imagen y carga la escena del juego
	public void CargarEscenaJuego()
	{
		audioBoton.Play();
		imagenCargar.enabled = true;
		//SceneManager.LoadScene(1);
		StartCoroutine(CargarEscena(1));
	}

	//Activa una imagen y carga la escena de las instrucciones
	public void CargarInstrucciones()
	{
		audioBoton.Play();
		imagenCargar.enabled = true;
		StartCoroutine(CargarEscena(2));
		//SceneManager.LoadScene(2);
	}

	//Metodo que cierra la aplicaion
	public void QuitarAplicacion()
	{
		audioBoton.Play();
		Application.Quit();
	}

	//Corutina que invoka una escena despues de 1 segundo
	private IEnumerator CargarEscena(int numeroEscena)
	{
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(numeroEscena);
	}
}
