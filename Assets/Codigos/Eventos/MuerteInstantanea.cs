using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteInstantanea : MonoBehaviour
{
    public GameObject Enemigo_A_Destruir;
    public string Name;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
            if (other.name.StartsWith("Personaje"))
            {
                ControlPersonaje MP;
                if(other.transform.parent.TryGetComponent<ControlPersonaje>(out MP))
                {
                    MP.controlNivel.Muerto(Name, Enemigo_A_Destruir);
                }
            }
    }
}
