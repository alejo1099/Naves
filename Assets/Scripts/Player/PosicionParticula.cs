using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionParticula : MonoBehaviour
{

    //se actualiza la posicion de la particula que envuelve al jugador mientras es invencible por perder una vida
    void Update()
    {
        transform.position = MovimientoPlayer.ReferenciaMovimientoPlayer.transform.position;
    }
}
