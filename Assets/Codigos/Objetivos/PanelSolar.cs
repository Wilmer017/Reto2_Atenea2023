using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSolar : MonoBehaviour
{
    public ControlNiveles Control1;
    public GameObject Rayo;
    public GameObject Cristal;
    public GameObject Mandos;
    public GameObject Paneles;
    public GameObject Poste;
    public MeshRenderer MaterialPanel;
    public Vector3 OrientacionCorrecta;
    public Vector3 DireccionFinal;
    [Range(0.1f , 1)] public float Limpieza;
    [Range(10 , 100)] public int Velocidad;

    public float Suciedad;
    float X, Y;

    public RecolocarObjeto RecolocarPanelRayo;
    public RecolocarObjeto RecolocarMando;

    public void IniciarPanelSolar()
    {
        DireccionFinal = new Vector3(Random.Range(0, 80), Random.Range(-90, 90), 0);

        MaterialPanel.material = new Material(MaterialPanel.material);

        RecolocarPanelRayo.IniciarRecolocar();
        RecolocarMando.IniciarRecolocar();
    }
    void Update()
    {
        if (!DireccionFinal.Equals(OrientacionCorrecta))
        {
            if (Rayo != null)
                Rayo.gameObject.SetActive(false);
            if (Cristal != null)
                Cristal.gameObject.SetActive(false);
        }
        if (Suciedad > 0 && DireccionFinal.Equals(OrientacionCorrecta))
        {
            Suciedad -= Time.deltaTime * Limpieza;
        }
        else if (X - 1 > DireccionFinal.x)
        {
            X -= Time.deltaTime * Velocidad;
        }
        else if (X + 1 < DireccionFinal.x)
        {
            X += Time.deltaTime * Velocidad;
        }
        else if (Y - 1 > DireccionFinal.y)
        {
            Y -= Time.deltaTime * Velocidad;
        }
        else if (Y + 1 < DireccionFinal.y)
        {
            Y += Time.deltaTime * Velocidad;
        }
        else if(DireccionFinal.Equals(OrientacionCorrecta))
        {
            if (Rayo != null)
                Rayo.gameObject.SetActive(true);
            if(Cristal!= null)
                Cristal.gameObject.SetActive(true);
            enabled = false;
        }
        else
        {
            enabled = false;
        }

        MaterialPanel.material.color = new Color(0.8f , 0.6f , 0.2f , Suciedad);
        Poste.transform.eulerAngles = new Vector3(0, Y, 0);
        Paneles.transform.eulerAngles = new Vector3(X, Y, 0);
    }

    public void RecogerCristal()
    {
        if (Rayo != null)
            Rayo.gameObject.SetActive(false);
        if (Cristal != null)
            Destroy(Cristal.gameObject);
        
        Control1.TareasRealizadas++;
        Control1.ActualizaObjetivos();
    }
    public void OrientarPaneles()
    {
        DireccionFinal = OrientacionCorrecta;
        Velocidad = Random.Range(10, 20);
        transform.SetParent(Control1.Realizados);
        enabled = true;
        Control1.ActualizaObjetivos();
    }
    public void DesOrientarPaneles()
    {
        DireccionFinal = new Vector3(Random.Range(0, 80), Random.Range(-90, 90), 0);
        Velocidad = Random.Range(60, 100);
        if (Rayo != null)
            Rayo.gameObject.SetActive(false);
        if (Cristal != null)
            Cristal.gameObject.SetActive(false);

        enabled = true;
    }
    public void CambiarDireccionPaneles()
    {
        if (!enabled)
        {
            DireccionFinal = new Vector3(Random.Range(0, 80), Random.Range(-90, 90), 0);
            enabled = true;
        }
    }
}
