using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

public class GeneradorTerreno : MonoBehaviour
{
    public int Distancia = 1;
    public int Largo = 1;
    public int Ancho = 1;
    public float Escala = 1;
    public float Altura = 0;
    public float TamanoTextura = 1;


    public float OffSetPerlinNoiseX = 0;
    public float OffSetPerlinNoiseY = 0;
    public float OffSetLargo = 0;
    public float OffSetAncho = 0;

    public bool Isla;

    public MeshRenderer TerrenoRender;
    public MeshFilter TerrenoMesh;
    public MeshCollider TerrenoCollider;

    public void Iniciar(int L, int A, int D, int H)
    {
        Distancia = D;
        Largo = L / Distancia;
        Ancho = A / Distancia;

        TerrenoRender = GetComponent<MeshRenderer>();
        TerrenoMesh = GetComponent<MeshFilter>();
        TerrenoCollider = GetComponent<MeshCollider>();

        DefinirAltura(name, H);

        CrearTriangulos();
        EditarAlturas();
        GenerarColiciones();

        DefinirMateriales();
    }

    public void DefinirOffSet(int NoiseX, int NoiseZ, int X = 0, int Z = 0)
    {
        OffSetPerlinNoiseX = NoiseX;
        OffSetPerlinNoiseY = NoiseZ;
        OffSetLargo = X;
        OffSetAncho = Z;
    }
    void DefinirAltura(string TipoTerreno, int AlturaPorDefecto)
    {
        if (AlturaPorDefecto == 0)
        {
            Altura = 2;
            Escala = 2;
            Distancia = 2;

            TamanoTextura = 0.2f;
        }
        else
        {
            if (TipoTerreno.Equals("ArenaDuna"))
            {
                Escala = 2;
                Altura = 25;

                TamanoTextura = 0.2f;
            }
            else if (TipoTerreno.Equals("ArenaDesierto"))
            {
                Escala = 5;
                Altura = 10;

                TamanoTextura = 0.2f;
            }
            else if (TipoTerreno.Equals("Pasto"))
            {
                Escala = 3;
                Altura = 15;

                TamanoTextura = 0.2f;
            }
            else if (TipoTerreno.Equals("Nieve"))
            {
                Escala = 1;
                Altura = 50;

                TamanoTextura = 0.2f;
            }
            else
            {
                Debug.LogWarning(TipoTerreno + ", Tipo de terreno Desconocido - ni Escala ni Altura Definida");
            }
        }
    }
    void CrearTriangulos()
    {
        List<Vector3> VerticesN = new List<Vector3>();
        List<Vector2> uvN = new List<Vector2>();
        List<int> triangulosN = new List<int>();

        for (int L = 0; L < Largo + 1; L++)
        {
            for (int A = 0; A < Ancho + 1; A++)
            {
                VerticesN.Add(Vector3.right * L * Distancia + Vector3.forward * A * Distancia);
                uvN.Add(Vector2.right * L * TamanoTextura + Vector2.up * A * TamanoTextura);
            }
        }

        for (int L = 0; L < Largo; L++)
        {
            for (int A = 0; A < Ancho; A++)
            {
                triangulosN.Add((L * (Ancho + 1)) + A);
                triangulosN.Add((L * (Ancho + 1)) + A + 1);
                triangulosN.Add((L * (Ancho + 1)) + A + (Ancho + 1));

                triangulosN.Add((L * (Ancho + 1)) + A + (Ancho + 1) + 1);
                triangulosN.Add((L * (Ancho + 1)) + A + (Ancho + 1));
                triangulosN.Add((L * (Ancho + 1)) + A + 1);
            }
        }

        TerrenoMesh.mesh.vertices = VerticesN.ToArray();
        TerrenoMesh.mesh.uv = uvN.ToArray();
        TerrenoMesh.mesh.triangles = triangulosN.ToArray();
    }
    void EditarAlturas()
    {
        Vector3[] Vertices = TerrenoMesh.mesh.vertices;
        for (int i = 0; i < Vertices.Length; i++)
        {
            float CalculoX = (Vertices[i].x + OffSetLargo + OffSetPerlinNoiseX) / Largo * Escala;
            float CalculoZ = (Vertices[i].z + OffSetAncho + OffSetPerlinNoiseY) / Ancho * Escala;
            float AlturaGenerada = Mathf.PerlinNoise(CalculoX, CalculoZ) * Altura;

            if (Isla)
            {
                AlturaGenerada += 1;
                Vector3 PuntoVerticeActual = Vector3.right * (Vertices[i].x + OffSetLargo) + Vector3.forward * (Vertices[i].z + OffSetAncho);
                Vector3 PuntoElevacion = Vector3.right * Largo * 2 + Vector3.forward * Ancho * 2;
                float AlturaMontana = Vector3.Distance (PuntoVerticeActual , PuntoElevacion ) / ((Largo + Ancho) / 2);
                AlturaGenerada -= (AlturaMontana * AlturaMontana);
            }

            Vertices[i].y = AlturaGenerada;
        }

        TerrenoMesh.mesh.vertices = Vertices;
        TerrenoMesh.mesh.RecalculateNormals();
    }
    void GenerarColiciones()
    {
        TerrenoCollider.sharedMesh = TerrenoMesh.mesh;
    }

    void DefinirMateriales()
    {
        TerrenoRender.material = Resources.Load<Material>("Materiales/Terreno/Terreno_" + name);

        if (TerrenoRender.material == null)
            Debug.LogWarning(name + ", Material no Disponible");
    }
}
