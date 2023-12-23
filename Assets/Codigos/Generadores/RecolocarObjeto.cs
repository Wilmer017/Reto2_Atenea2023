using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolocarObjeto : MonoBehaviour
{
    public bool NoRotar;
    public bool DefinirAltura;
    public int Altura;
    public string[] ApareceEn;

    public Vector3 Coordenada;
    public Vector3 PosicionSpaw;
    public Vector3 DesplazamientoSpaw;
    public Quaternion RotacionSpaw;

    public Transform Referencia;
    public Vector2 Offset_X;
    public Vector2 Offset_Z;

    RaycastHit Rayo;


    public void IniciarRecolocar()
    {
        if (Offset_X.x == Offset_X.y)
        {
            Offset_X.x -= 1;
            Offset_X.y = 1;
        }
        if (Offset_Z.x == Offset_Z.y)
        {
            Offset_Z.x -= 1;
            Offset_Z.y = 1;
        }

        int Intentos = 10;

        while (Intentos > 0)
        {
            Intentos--;
            Intentar();
        };
    }

    void Intentar()
    {
        CoordenadaAleatoria();

        if (!NoRotar)
            RotacionAleatoria();

        if (Physics.Raycast(Coordenada, Vector3.down, out Rayo, 99))
        {
            if (Rayo.collider != null)
            {
                for (int i = 0; i < ApareceEn.Length; i++)
                {
                    if (Rayo.collider.name.Equals(ApareceEn[i]))
                    {
                        if (!DefinirAltura)
                            transform.position = Rayo.point + DesplazamientoSpaw;
                        else
                            transform.position = Coordenada + Vector3.up * (Altura - 100);

                        if (!NoRotar)
                            transform.rotation = RotacionSpaw;

                        gameObject.name = "MandoRecolocado";
                    }
                }
            }
        }
    }

    void CoordenadaAleatoria()
    {
        float X;
        float Z;

        X = Random.Range(Referencia.position.x + Offset_X.x, Referencia.position.x + Offset_X.y);
        Z = Random.Range(Referencia.position.z + Offset_Z.x, Referencia.position.z + Offset_Z.y);

        if (Random.Range(0f, 1f) < 0.5f)
            X = -X;
        if (Random.Range(0f, 1f) < 0.5f)
            Z = -Z;

        Coordenada = new Vector3(X, 100, Z);
    }
    void RotacionAleatoria()
    {
        float X = Random.Range(0, 1f);
        RotacionSpaw = new Quaternion(0, X, 0, 1);
    }
}
