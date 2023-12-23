using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(BoxCollider))]

public class GeneradorAgua : MonoBehaviour
{
    public int Largo;
    public int Ancho;
    public float Altura;

    public MeshRenderer AguaRender;
    public MeshFilter AguaMesh;
    public BoxCollider AguaCollider;

    public void Iniciar(int L, int A, int H)
    {
        AguaRender = GetComponent<MeshRenderer>();
        AguaMesh = GetComponent<MeshFilter>();
        AguaCollider = GetComponent<BoxCollider>();

        Largo = L;
        Ancho = A;
        Altura = H;

        GenerarAgua();
        DefinirMateriales();
    }

    void GenerarAgua()
    {
        List<Vector3> VerticesN = new List<Vector3>();
        List<int> triangulosN = new List<int>(); ;

        VerticesN.Add(Vector3.up * Altura);
        VerticesN.Add(Vector3.forward * Ancho + Vector3.up * Altura);
        VerticesN.Add(Vector3.right * Largo + Vector3.up * Altura);
        VerticesN.Add(Vector3.right * Largo + Vector3.forward * Ancho + Vector3.up * Altura);

        triangulosN.Add(0);
        triangulosN.Add(1);
        triangulosN.Add(2);

        triangulosN.Add(1);
        triangulosN.Add(3);
        triangulosN.Add(2);

        triangulosN.Add(2);
        triangulosN.Add(1);
        triangulosN.Add(0);

        triangulosN.Add(2);
        triangulosN.Add(3);
        triangulosN.Add(1);

        AguaMesh.mesh.vertices = VerticesN.ToArray();
        AguaMesh.mesh.triangles = triangulosN.ToArray();

        AguaMesh.mesh.RecalculateNormals();

        if (Altura == 0)
            Altura = 0.2f;

        AguaCollider.isTrigger = true;
        AguaCollider.size = Vector3.right * Largo + Vector3.forward * Ancho + Vector3.up * Altura;
        AguaCollider.center = (Vector3.right * Largo + Vector3.forward * Ancho + Vector3.up * Altura) / 2;
    }

    void DefinirMateriales()
    {
        AguaRender.material = Resources.Load<Material>("Materiales/Terreno/Terreno_" + name);

        if (AguaRender.material == null)
            Debug.LogWarning(name + ", Material no Disponible");
    }
}
