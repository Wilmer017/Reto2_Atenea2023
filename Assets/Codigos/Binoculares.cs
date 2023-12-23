using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binoculares : MonoBehaviour
{
    public ControlPersonaje ControlPerson;
    public Cinemachine.CinemachineFreeLook CamaraBase;
    public Transform CamaraPosicion;
    public bool Activados;
    public float Zoom = 60;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Activados)
            {
                CamaraBase.m_SplineCurvature = 0.5f;

                CamaraBase.m_Orbits[0].m_Height = 5f;
                CamaraBase.m_Orbits[1].m_Height = 3f;
                CamaraBase.m_Orbits[2].m_Height = 1f;

                CamaraBase.m_Orbits[0].m_Radius = 2f;
                CamaraBase.m_Orbits[1].m_Radius = 3f;
                CamaraBase.m_Orbits[2].m_Radius = 1f;

                CamaraBase.m_Lens.FieldOfView = 60;
                CamaraBase.m_YAxis.Value = 0.4f;
                CamaraBase.m_YAxis.m_MaxSpeed = 2;
                CamaraBase.m_XAxis.m_MaxSpeed = 200;

                Zoom = 40;
                Activados = false;

                ControlPerson.controlNivel.vista_Nivel.Prismaticos.SetActive(false);
            }
            else
            {
                CamaraBase.m_SplineCurvature = 0;

                CamaraBase.m_Orbits[0].m_Height = 1.9f;
                CamaraBase.m_Orbits[1].m_Height = 1.8f;
                CamaraBase.m_Orbits[2].m_Height = 1.7f;

                CamaraBase.m_Orbits[0].m_Radius = 0.1f;
                CamaraBase.m_Orbits[1].m_Radius = 0.1f;
                CamaraBase.m_Orbits[2].m_Radius = 0.1f;

                CamaraBase.m_YAxis.Value = 0.85f;

                Activados = true;

                ControlPerson.controlNivel.vista_Nivel.Prismaticos.SetActive(true);
            }
        }
        if (Activados)
        {
            if (Zoom > 42)
                Zoom = 42;
            else if (Zoom < 3)
                Zoom = 3;
            else
            {
                Zoom -= Input.mouseScrollDelta.y;
                CamaraBase.m_Lens.FieldOfView = Zoom;
                CamaraBase.m_YAxis.m_MaxSpeed = Zoom / 20;
                CamaraBase.m_XAxis.m_MaxSpeed = Zoom * 10;

               
                RaycastHit Rayo;
                if(Physics.Raycast(CamaraPosicion.position, CamaraPosicion.forward , out Rayo, 400))
                {
                    ControlPerson.controlNivel.vista_Nivel.MostarEnPrismaticos( 42 / (int)Zoom, (int) Rayo.distance);
                }
                else
                {
                    ControlPerson.controlNivel.vista_Nivel.MostarEnPrismaticos(42 / (int)Zoom, 0);
                }
            }
        }
    }
}
