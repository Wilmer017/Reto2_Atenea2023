using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonesControlador : MonoBehaviour
{
    public ControlNiveles ControlNiveles;

    public Vista_Niveles vistaNivel;
    public Vista_Menu vistaMenu;
    public ControlMenu ControlMenu;
    public Button[] Botones;

    public void Inicializar()
    {
        if(vistaNivel != null) ControlNiveles = vistaNivel.ControlNivel;
        if(vistaMenu != null) ControlNiveles = vistaMenu.ControlNivel;

        for (int i = 0; i < Botones.Length; i++)
        {
            DefinirEvento(i);
        }
    }

    public void CambiarBotonReanudar(string Original, string Nuevo)
    {
        for (int i = 0; i < Botones.Length; i++)
        {
            if (Botones[i].name.Equals(Original))
            {
                Botones[i].transform.name = Nuevo;
                Botones[i].targetGraphic.GetComponent<Text>().text = Nuevo;
                DefinirEvento(i);
                return;
            }
        }
    }
    void DefinirEvento(int ID)
    {
        bool EventoMenuDefinido = true;
        bool EventoNivelesDefinido = true;

        if (ControlMenu != null)
        {
            if (Botones[ID].gameObject.name.Equals("BotonMenu Anterior"))
                Botones[ID].onClick.AddListener(ControlMenu.GirarAnterior);
            else if (Botones[ID].gameObject.name.Equals("BotonMenu Siguiente"))
                Botones[ID].onClick.AddListener(ControlMenu.GirarSiguiente);
            else if (Botones[ID].gameObject.name.Equals("BotonMenu Jugar"))
                Botones[ID].onClick.AddListener(ControlMenu.Jugar);
            else if (Botones[ID].gameObject.name.Equals("BotonMenu Salir"))
                Botones[ID].onClick.AddListener(ControlMenu.PedirSalir);
            else EventoMenuDefinido = false;
        }

        if (ControlNiveles != null)
        {
            if (Botones[ID].gameObject.name.Equals("Reanudar"))
                Botones[ID].onClick.AddListener(ControlNiveles.DesPausarJuego);
            else if (Botones[ID].gameObject.name.Equals("Revivir"))
                Botones[ID].onClick.AddListener(ControlNiveles.Revivir);
            else if (Botones[ID].gameObject.name.Equals("Reiniciar"))
                Botones[ID].onClick.AddListener(ControlNiveles.ReiniciarJuego);
            else if (Botones[ID].gameObject.name.Equals("Opciones"))
                Botones[ID].onClick.AddListener(ControlNiveles.OpcionesJuego);
            else if (Botones[ID].gameObject.name.Equals("Menu Principal"))
                Botones[ID].onClick.AddListener(ControlNiveles.MenuPrincipal);
            else if (Botones[ID].gameObject.name.Equals("Salir del Juego"))
                Botones[ID].onClick.AddListener(ControlNiveles.CerrarExe);
            else EventoNivelesDefinido = false;
        }

        if(!EventoMenuDefinido && !EventoNivelesDefinido) Debug.LogWarning("Controlador no existe", gameObject);
        else if(EventoMenuDefinido && EventoNivelesDefinido) Debug.LogWarning("Boton con dos Eventos", gameObject);
    }
}
