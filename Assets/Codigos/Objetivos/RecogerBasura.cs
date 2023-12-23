using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerBasura : MonoBehaviour
{
    public ControlNiveles Control1;

    public GameObject PadreBasura;
    public RecolocarObjeto Cristal;

    [Range(1 , 10)] public int BasuraNoRecogida;
    [Range(1 , 25)] public int Velocidad;

    public int BasuraRecogida;
    float X;


    public void IniciarRecogerBasura()
    {
        Cristal.IniciarRecolocar();

        for (int i = 0; i < PadreBasura.transform.childCount ; i++)
        {
            RecolocarObjeto Basura_Recolocar;
            if (PadreBasura.transform.GetChild(i).TryGetComponent<RecolocarObjeto>(out Basura_Recolocar))
            {
                Basura_Recolocar.IniciarRecolocar();
            }
        }
    }
    void Update()
    {
        if ((int) X > 1)
        {
            X = 0;

            if (PadreBasura.transform.childCount > BasuraRecogida)
                RecogeBasura(BasuraRecogida);
            BasuraRecogida++;
        }
        else if (BasuraRecogida < BasuraNoRecogida)
        {
            X += Time.deltaTime * Velocidad;
        }
        else if(BasuraRecogida == BasuraNoRecogida)
        {
            AreaLimpia();
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
    public void RecogeBasura()
    {
        enabled = true;
    }
    public void RecogeBasura(int B)
    {
        PadreBasura.transform.GetChild(B).gameObject.SetActive(false);
        Velocidad = Random.Range(2, 25);
    }
    public void AreaLimpia()
    {
        print("Area limpia");
        transform.SetParent(Control1.Realizados);
        Cristal.gameObject.SetActive(true);
        Control1.ActualizaObjetivos();
        enabled = false;
    }
}
