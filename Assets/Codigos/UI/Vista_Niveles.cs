using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Niveles : MonoBehaviour
{
    public ControlNiveles ControlNivel;
    public BotonesControlador BotonesMenu;

    public GameObject Prismaticos;
    public Text ZoomOptico;
    public Text DistanciaOptica;

    public Image FlechaHuracan;
    public Text MesajeBrujula1;
    public Text MesajeBrujula2;
    public Text MesajeBrujula3;
    public Text MesajeBrujula4;
    public Text ObjetivosTexto;
    public Text InformacionCentro;

    public void IniciarVista()
    {
        if (BotonesMenu.gameObject != null)
        {
            if (ControlNivel.ID_Nivel > 0)
                BotonesMenu.gameObject.SetActive(false);
        }

            
        if(Prismaticos != null)
            Prismaticos.SetActive(false);

        BotonesMenu.ControlNiveles = ControlNivel;
        BotonesMenu.Inicializar();
    }
    public void MostrarCausaMuerte(string Causa)
    {
        InformacionCentro.gameObject.SetActive(true);
        InformacionCentro.text = Causa;
    }
    public void MostrarVictoria()
    {
        InformacionCentro.gameObject.SetActive(true);
        InformacionCentro.text = "Haz Ganado";
    }

    public void MensajePrimerObjetivo(string O1)
    {
        ObjetivosTexto.text = O1;
    }
    public void MensajeObjetivos(string O1, int obtenido = 0, int total = 0)
    {
        if (obtenido == 0 && total == 0)
            ObjetivosTexto.text += "\n " + O1 ;
        else
            ObjetivosTexto.text += "\n - " + O1 + " " + obtenido + "/" + total;
    }
    public void MensajeBrujula(int id, string M, Color colorTexto)
    {
        if(id == 0)
        {
            if(M.Equals("Ocultar"))
            {
                MesajeBrujula1.gameObject.SetActive(false);
            }
            else
            {
                MesajeBrujula1.gameObject.SetActive(true);
                MesajeBrujula1.text = M;
                MesajeBrujula1.color = colorTexto;
            }
        }
        else if (id == 1)
        {
            if (M.Equals("Ocultar"))
            {
                MesajeBrujula2.gameObject.SetActive(false);
            }
            else
            {
                MesajeBrujula2.gameObject.SetActive(true);
                MesajeBrujula2.text = M;
                MesajeBrujula2.color = colorTexto;
            }
        }
        else if (id == 2)
        {
            if (M.Equals("Ocultar"))
            {
                MesajeBrujula3.gameObject.SetActive(false);
            }
            else
            {
                MesajeBrujula3.gameObject.SetActive(true);
                MesajeBrujula3.text = M;
                MesajeBrujula3.color = colorTexto;
            }
        }
        else if (id == 3)
        {
            if (M.Equals("Ocultar"))
            {
                MesajeBrujula4.gameObject.SetActive(false);
            }
            else
            {
                MesajeBrujula4.gameObject.SetActive(true);
                MesajeBrujula4.text = M;
                MesajeBrujula4.color = colorTexto;
            }
        }
    }

    public void MostarEnPrismaticos(int Zoom, int Distancia)
    {
        ZoomOptico.text = "x" + Zoom;
        DistanciaOptica.text = Distancia + "m";
    }
    public void MostarPausa()
    {
        InformacionCentro.gameObject.SetActive(false);
        BotonesMenu.gameObject.SetActive(true);
    }

    public void OcultarPausa()
    {
        InformacionCentro.gameObject.SetActive(false);
        BotonesMenu.gameObject.SetActive(false);
    }
}
