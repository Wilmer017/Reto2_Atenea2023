using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorUnaVez : MonoBehaviour
{
    public PanelSolar PS;
    public RecogerBasura RB;
    public RecogerLlamas RL;
    public Funciones FuncionActivador;

    public enum Funciones
    {
        RecojeCristal = 0,
        OrientarPaneles = 1,
        RecogerBasura = 2,
        RecogerLlamas = 3
    }

    private void OnTriggerEnter(Collider other)
    {
        if(PS != null)
        {
            if (FuncionActivador.Equals(Funciones.OrientarPaneles))
            {
                if (other.name.StartsWith("Personaje"))
                {
                    PS.OrientarPaneles();
                }
            }
            else if (FuncionActivador.Equals(Funciones.RecojeCristal))
            {
                if (other.name.StartsWith("Personaje"))
                {
                    PS.RecogerCristal();
                    Destroy(gameObject);
                }
            }
        }
        if (RB != null)
        {
            if (FuncionActivador.Equals(Funciones.RecogerBasura))
            {
                if (other.name.StartsWith("Personaje"))
                {
                    RB.RecogeBasura();
                }
            }
            else if (FuncionActivador.Equals(Funciones.RecojeCristal))
            {
                if (other.name.StartsWith("Personaje"))
                {
                    RB.RecogerCristal();
                    Destroy(gameObject);
                }
            }
        }
        if (RL != null)
        {
            if (FuncionActivador.Equals(Funciones.RecogerLlamas))
            {
                if (other.name.StartsWith("Personaje"))
                {
                    RL.RecogeLlamas();
                }
            }
            else if (FuncionActivador.Equals(Funciones.RecojeCristal))
            {
                if (other.name.StartsWith("Personaje"))
                {
                    RL.RecogerCristal();
                    Destroy(gameObject);
                }
            }
        }
    }
}
