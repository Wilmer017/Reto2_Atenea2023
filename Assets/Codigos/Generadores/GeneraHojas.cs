using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(CapsuleCollider))]
public class GeneraHojas : MonoBehaviour
{
    public ControlMenu ControlMenu;
    public ControlNiveles ControlNivel;
    public GameObject HojasPref;
    public GameObject Sombra;

    Mesh Arbol;
    Material TipoHoja;
    int HojasCreadas = 0;
    int Hojas = 25;
    int AlturaHojas = 4;

    public void Iniciar()
    {
        Arbol = GetComponent<MeshFilter>().sharedMesh;
        AlturaHojas = (int) GetComponent<CapsuleCollider>().height;
        TipoHoja = GetComponent<MeshRenderer>().material;

        CargarHojas();
        Destroy(this);
    }

    void CargarHojas()
    {
        Sombra.SetActive(true);

        if (Hojas == 0)
            Hojas = 1;

        float X = 0.25f * Random.Range(0, 5);
        float Y = 0.125f * Random.Range(0, 9);
        TipoHoja.mainTextureOffset = new Vector2(X, Y);
        GetComponent<MeshRenderer>().material = Instantiate<Material>(TipoHoja);

        int Intentos = Hojas * 3;

        while (Intentos > 0 && Hojas > HojasCreadas)
        {
            int R = Random.Range(0, Arbol.vertexCount);

            if (Arbol.vertices[R].y > AlturaHojas)
            {
                GameObject HojaNueva = Instantiate(HojasPref, transform.localPosition + Arbol.vertices[R], transform.localRotation, transform);
                Vector3 VectorAleatorio = new Vector3(Random.Range(0, 15), Random.Range(0, 15), Random.Range(0, 15));
                if (ControlMenu != null)
                    HojaNueva.AddComponent<HojaApuntaCamara>().Iniciar(ControlMenu.Camara, VectorAleatorio);
                else if (ControlNivel != null)
                    HojaNueva.AddComponent<HojaApuntaCamara>().Iniciar(ControlNivel.Personaje.CamaraPersonaje.transform, VectorAleatorio);

                HojasCreadas++;
            }
            else
            {
                Intentos--;
            }
        }
    }
}
