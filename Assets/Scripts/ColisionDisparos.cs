using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionDisparos : MonoBehaviour {

	//Si este disparo colisiona con un objeto entonces se destruye
	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}
}