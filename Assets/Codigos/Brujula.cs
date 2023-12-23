using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brujula : MonoBehaviour
{
    public enum Apuntadores
    {
        Objetivos,
        Jeep,
        Animales,
        Rodadoras,
        Tormentas,
        Niebla,
        Huracanes,
        Cactus
    }

    public int BrujulaID;
    public Apuntadores Apuntado;

    public ControlPersonaje Personaje;
    public Transform BaseCamara;
    public ControlNiveles Control1;
    public Transform PadreObjetivos;
    public Transform ObjetoApuntado;
    public float DistanciaMostrar;

    private void Update()
    {
        if (Apuntado.Equals(Apuntadores.Objetivos))
            Apuntar(30, "Estas cerca del objetivo", 200, "Objetivo cerca", "No hay objetivos cerca");
        else if (Apuntado.Equals(Apuntadores.Jeep))
            Apuntar(20, "Aqui esta la Jeep", 150, "Jeep cerca", "Acercate a la Jeep para ganar");
        else if (Apuntado.Equals(Apuntadores.Animales))
            Apuntar(15, "Cuidado!!\n Corre", 50, "Mira tus alrededores", "No hay bestias cerca");
        else if (Apuntado.Equals(Apuntadores.Rodadoras))
            Apuntar(20, "Bola Rodadora!!", 60, "Se Aproxima Rodadora", "No hay Rodadoras cerca");
        else if (Apuntado.Equals(Apuntadores.Tormentas))
            Apuntar(15, "Estas en la tormenta", 80, "Se Aproxima una Tormenta", "No hay Tormentas cerca");
        else if (Apuntado.Equals(Apuntadores.Niebla))
            Apuntar(10, "Estas en la Neblina", 60, "Se Aproxima una Neblina", "No hay Neblinas cerca");
        else if (Apuntado.Equals(Apuntadores.Huracanes))
            Apuntar(20, "Estas en el Huracan", 150, "Se Aproxima un Huracan", "No hay Huracanes cerca");
        else if (Apuntado.Equals(Apuntadores.Cactus))
            Apuntar(15, "Cuidado con las espinas", 30, "Cactus cerca", "No hay Cactus cerca");
    }
    public void SetearBrujula()
    {
        Control1 = Personaje.controlNivel;
        gameObject.SetActive(true);

        if (Apuntado.Equals(Apuntadores.Objetivos))
            PadreObjetivos = Control1.PorRealizar;
        else if (Apuntado.Equals(Apuntadores.Jeep))
            PadreObjetivos = Control1.GeneraJeep.transform;
        else if (Apuntado.Equals(Apuntadores.Animales))
            PadreObjetivos = Control1.GeneraAnimales.transform;
        else if (Apuntado.Equals(Apuntadores.Rodadoras))
            PadreObjetivos = Control1.GeneraRodadores.transform;
        else if (Apuntado.Equals(Apuntadores.Tormentas))
            PadreObjetivos = Control1.GeneraTormentas.transform;
        else if (Apuntado.Equals(Apuntadores.Niebla))
            PadreObjetivos = Control1.GeneraNieblas.transform;
        else if (Apuntado.Equals(Apuntadores.Huracanes))
            PadreObjetivos = Control1.GeneraHuracanes.transform;
        else if (Apuntado.Equals(Apuntadores.Cactus))
            PadreObjetivos = Control1.GeneraCactus.transform;

    }

    void Apuntar(int Distan1, string Mensaje1, int Distan2, string Mensaje2, string Mensaje3)
    {
        if (PadreObjetivos != null)
        {
            if (PadreObjetivos.childCount != 0)
            {
                Control1.vista_Nivel.gameObject.SetActive(true);
                gameObject.SetActive(true);

                ObjetoApuntado = PadreObjetivos.GetChild(0);

                for (int i = 1; i < PadreObjetivos.childCount; i++)
                {
                    float Distancia1, Distancia2;
                    Distancia1 = Vector3.Distance(ObjetoApuntado.position, transform.position);
                    Distancia2 = Vector3.Distance(PadreObjetivos.GetChild(i).position, transform.position);

                    if (Distancia1 > Distancia2)
                    {
                        ObjetoApuntado = PadreObjetivos.GetChild(i);
                    }
                }

                DistanciaMostrar = Vector3.Distance(Personaje.controladorCuerpo.transform.position, ObjetoApuntado.position);

                Vector3 targetDir = Personaje.controladorCuerpo.transform.position - ObjetoApuntado.position;
                float angle = Vector3.SignedAngle(-BaseCamara.forward, targetDir, Vector3.up);

                transform.eulerAngles = new Vector3(0, angle, 0);
                if (DistanciaMostrar < Distan1)
                    Control1.vista_Nivel.MensajeBrujula(BrujulaID, Mensaje1, Color.red);
                else if (DistanciaMostrar < Distan2)
                    Control1.vista_Nivel.MensajeBrujula(BrujulaID, Mensaje2 + " " + (int) DistanciaMostrar + "M", Color.yellow);
                else
                    Control1.vista_Nivel.MensajeBrujula(BrujulaID, Mensaje3, Color.green);
            }
            else
            {
                Control1.vista_Nivel.MensajeBrujula(BrujulaID, "Ocultar", default);
                gameObject.SetActive(false);
            }
        }
    }
}
