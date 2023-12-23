using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Menu : MonoBehaviour
{
    public ControlNiveles ControlNivel;
    public BotonesControlador BotonesMenu;

    public Text ObjetivosTexto;

    public void IniciarVista()
    {
        if (BotonesMenu.gameObject != null)
        {
            if (ControlNivel.ID_Nivel > 0)
                BotonesMenu.gameObject.SetActive(false);
        }

        BotonesMenu.ControlNiveles = ControlNivel;
        BotonesMenu.Inicializar();
    }

    public void MensajePrimerObjetivo(string O1)
    {
        ObjetivosTexto.text = O1;
    }
    public void MensajeObjetivos(string O1, int obtenido = 0, int total = 0)
    {
        if (obtenido == 0 && total == 0)
            ObjetivosTexto.text += "\n" + O1;
        else
            ObjetivosTexto.text += "\n - " + O1 + " " + obtenido + "/" + total;
    }
}
