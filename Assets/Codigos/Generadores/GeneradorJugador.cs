using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorJugador : MonoBehaviour
{
    public ControlNiveles ControlNivel;

    public int DistanciaBorde = 10;
    public string[] ApareceEn;

    GameObject JugadoraCreada;
    public GameObject[] JugadoresAGenerar;
    public Vector3 Coordenada;
    public Vector3 PosicionSpaw;
    public Vector3 DesplazamientoSpaw;
    public Quaternion RotacionSpaw;
    RaycastHit Rayo;

    public void Iniciar()
    {
        ApareceEn = new string[] { "ArenaDuna", "ArenaDesierto", "Pasto", "Nieve" };
        int Intentos = ControlNivel.intentos;

        while (JugadoraCreada == null && Intentos > 0)
        {
            Intentos--;
            Crearjugador();
        };

        if(Intentos < 0)
        {
            Debug.LogWarning("mas de " + ControlNivel.intentos + " intentos para Crear Personaje");
        }
    }

    public void Crearjugador()
    {
        int ObjetoID = Random.Range(0, JugadoresAGenerar.Length);
        CoordenadaAleatoria();
        RotacionAleatoria();

        if (Physics.Raycast(Coordenada, Vector3.down, out Rayo, 99))
        {
            //Debug.Log(Rayo.collider.name);
            for (int i = 0; i < ApareceEn.Length; i++)
            {
                if(Rayo.collider != null)
                {
                    if (Rayo.collider.name.Equals(ApareceEn[i]))
                    {
                        JugadoraCreada = Instantiate(JugadoresAGenerar[ObjetoID], Rayo.point + DesplazamientoSpaw, Quaternion.identity, transform);

                        ControlPersonaje CP = JugadoraCreada.GetComponent<ControlPersonaje>();
                        ControlNivel.Personaje = CP;
                        ControlNivel.Personaje.controladorCuerpo.transform.rotation = RotacionSpaw;
                        CP.IniciarPersonaje();
                        CP.controlNivel = ControlNivel;
                    }
                }
                else
                {
                    Debug.LogWarning("Rayo no tiene Collider");
                }
            }
        }
    }

    void CoordenadaAleatoria()
    {
        float X;
        float Z;

        X = Random.Range(ControlNivel.bordes + DistanciaBorde, ControlNivel.Largo - ControlNivel.bordes - DistanciaBorde);
        Z = Random.Range(ControlNivel.bordes + DistanciaBorde, ControlNivel.Ancho - ControlNivel.bordes - DistanciaBorde);

        Coordenada = new Vector3(X, 100, Z);
    }
    void RotacionAleatoria()
    {
        float X = Random.Range(0, 1f);
        RotacionSpaw = new Quaternion(0, X, 0, 1);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(Coordenada, Vector3.down * 99, Color.cyan);
        Gizmos.DrawSphere(PosicionSpaw, 0.2f);
    }
}
