using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

public class GeneradorLimites : MonoBehaviour
{
    public int AlturaPared;
    public int VerticesConteo;

    public MeshRenderer LimiteRender;
    public MeshFilter LimiteMesh;
    public MeshCollider LimiteCollider;

    List<Vector3> VerticesN = new List<Vector3>();
    List<int> triangulosN = new List<int>();

    public void Iniciar(int Bordes, int Largo, int Ancho, int Alto)
    {
        AlturaPared = Alto;


        LimiteRender = GetComponent<MeshRenderer>();
        LimiteMesh = GetComponent<MeshFilter>();
        LimiteCollider = GetComponent<MeshCollider>();

        int zero_limite = Bordes;
        int Ly_limite = Largo - Bordes;
        int Ay_limite = Ancho - Bordes;

        Vector3 LxAy = Vector3.right * zero_limite + Vector3.forward * zero_limite;
        Vector3 LxAx = Vector3.right * Ly_limite + Vector3.forward * zero_limite;
        Vector3 LyAy = Vector3.right * zero_limite + Vector3.forward * Ay_limite;
        Vector3 LyAx = Vector3.right * Ly_limite + Vector3.forward * Ay_limite;

        GenerarPared(LxAx, LxAy);
        GenerarPared(LyAx, LxAx);
        GenerarPared(LyAy, LyAx);
        GenerarPared(LxAy, LyAy);

        GenerarTecho(LxAy, LxAx, LyAy, LyAx);

        GenerarTriangulos();
        GenerarColiciones();
        DefinirMateriales();
    }

    void GenerarPared(Vector3 V1, Vector3 V2)
    {
        VerticesN.Add(V1);
        VerticesN.Add(V1 + Vector3.up * AlturaPared);
        VerticesN.Add(V2);
        VerticesN.Add(V2 + Vector3.up * AlturaPared);

        triangulosN.Add(VerticesConteo);
        triangulosN.Add(VerticesConteo + 1);
        triangulosN.Add(VerticesConteo + 2);

        triangulosN.Add(VerticesConteo + 1);
        triangulosN.Add(VerticesConteo + 3);
        triangulosN.Add(VerticesConteo + 2);

        VerticesConteo = VerticesN.Count;
    }
    void GenerarTecho(Vector3 V1, Vector3 V2, Vector3 V3, Vector3 V4)
    {
        VerticesN.Add(V1 + Vector3.up * AlturaPared);
        VerticesN.Add(V2 + Vector3.up * AlturaPared);
        VerticesN.Add(V3 + Vector3.up * AlturaPared);
        VerticesN.Add(V4 + Vector3.up * AlturaPared);

        triangulosN.Add(VerticesConteo);
        triangulosN.Add(VerticesConteo + 1);
        triangulosN.Add(VerticesConteo + 2);

        triangulosN.Add(VerticesConteo + 1);
        triangulosN.Add(VerticesConteo + 3);
        triangulosN.Add(VerticesConteo + 2);

        VerticesConteo = VerticesN.Count;
    }

    void GenerarTriangulos()
    {
        LimiteMesh.mesh.vertices = VerticesN.ToArray();
        LimiteMesh.mesh.triangles = triangulosN.ToArray();

        LimiteMesh.mesh.RecalculateBounds();
        LimiteMesh.mesh.RecalculateNormals();
        LimiteMesh.mesh.RecalculateTangents();
    }
    public void GenerarColiciones()
    {
        LimiteCollider.sharedMesh = LimiteMesh.mesh;
    }

    void DefinirMateriales()
    {
        LimiteRender.material = Resources.Load<Material>("Materiales/Terreno/Terreno_" + name);

        if (LimiteRender.material == null)
            Debug.LogWarning(name + ", Material no Disponible");
    }
}
