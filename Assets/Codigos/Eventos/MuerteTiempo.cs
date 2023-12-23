using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteTiempo : MonoBehaviour
{
    public GameObject Enemigo_A_Destruir;
    public string Name;
    public float TiempoMinimo;
    public float TiempoMaximo;

    public float Tiempo;
    public float TiempoTranscurrido;


    private void OnTriggerEnter(Collider other)
    {
        Tiempo = Random.Range(TiempoMinimo, TiempoMaximo);
    }
    private void OnTriggerStay(Collider other)
    {
        if (Tiempo != 0)
        {
            TiempoTranscurrido += Time.deltaTime;
            if (TiempoTranscurrido > Tiempo)
                if (other != null)
                    if (other.name.StartsWith("Personaje"))
                    {
                        ControlPersonaje MP;
                        if (other.transform.parent.TryGetComponent<ControlPersonaje>(out MP))
                        {
                            MP.controlNivel.Muerto(Name, Enemigo_A_Destruir);
                        }
                    }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Tiempo = 0;
    }
}
