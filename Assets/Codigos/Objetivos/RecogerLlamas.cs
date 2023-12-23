using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerLlamas : MonoBehaviour
{
    public ControlNiveles Control1;

    public GameObject PadreLlamas;
    public RecolocarObjeto Cristal;

    [Range(1 , 10)] public int LlamasNoRecogida;
    [Range(1 , 25)] public int Velocidad;

    public int LlamasRecogida;
    float X;


    public void IniciarRecogerLlamas()
    {
        Cristal.IniciarRecolocar();

        for (int i = 0; i < PadreLlamas.transform.childCount; i++)
        {
            RecolocarObjeto Llamas_Recolocar;
            if (PadreLlamas.transform.GetChild(i).TryGetComponent<RecolocarObjeto>(out Llamas_Recolocar))
            {
                Llamas_Recolocar.IniciarRecolocar();
            }

            HojaApuntaCamara Llamas_Apuntan;
            if (PadreLlamas.transform.GetChild(i).TryGetComponent<HojaApuntaCamara>(out Llamas_Apuntan))
            {
                Llamas_Apuntan.Iniciar(Control1.Personaje.CamaraPersonaje.transform, Vector3.zero);
            }
        }
    }
    void Update()
    {
        if ((int) X > 1)
        {
            X = 0;

            if (PadreLlamas.transform.childCount > LlamasRecogida)
                RecogeLlamas(LlamasRecogida);
            LlamasRecogida++;
        }
        else if (LlamasRecogida < LlamasNoRecogida)
        {
            X += Time.deltaTime * Velocidad;
        }
        else if(LlamasRecogida == LlamasNoRecogida)
        {
            LlamasApagadas();
        }
        else
        {
            enabled = false;
        }
    }

    public void RecogerCristal()
    {
        if (Cristal != null)
            Destroy(Cristal.gameObject);
        
        Control1.TareasRealizadas++;
        Control1.ActualizaObjetivos();
    }
    public void RecogeLlamas()
    {
        enabled = true;
    }
    public void RecogeLlamas(int B)
    {
        PadreLlamas.transform.GetChild(B).gameObject.SetActive(false);
        Velocidad = Random.Range(2, 25);
    }
    public void LlamasApagadas()
    {
        print("Area limpia");
        transform.SetParent(Control1.Realizados);
        Cristal.gameObject.SetActive(true);
        Control1.ActualizaObjetivos();
        enabled = false;
    }
}
