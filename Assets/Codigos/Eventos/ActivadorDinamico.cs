using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDinamico : MonoBehaviour
{
    public PanelSolar PS;
    public Funciones FuncionActivador;

    public enum Funciones
    {
        DesOrientarPaneles = 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PS != null)
        {
            if (FuncionActivador.Equals(Funciones.DesOrientarPaneles))
            {
                PS.DesOrientarPaneles();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (PS != null)
        {
            if (FuncionActivador.Equals(Funciones.DesOrientarPaneles))
            {
                if (!other.name.StartsWith("Personaje"))
                    PS.CambiarDireccionPaneles();
            }
        }
    }
}
