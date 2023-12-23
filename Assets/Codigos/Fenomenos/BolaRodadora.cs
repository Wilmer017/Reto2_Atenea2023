using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BolaRodadora : MonoBehaviour
{
    public ControlNiveles ControlNivel;

    public bool Empujando;
    public Rigidbody RB;
    public int Fuerza;
    public void IniciarRodadora()
    {
        RB = transform.GetComponent<Rigidbody>();
        Fuerza = 50;
    }

    void Update()
    {
        if (   transform.position.x > ControlNivel.Largo - ControlNivel.bordes - 1
            || transform.position.x < 1
            || transform.position.z > ControlNivel.Ancho - ControlNivel.bordes - 1
            || transform.position.z < 1)
        {
            Destroy(gameObject);
            ControlNivel.GeneraRodadores.ObjetosCreados--;

            ControlNivel.GeneraRodadores.CrearObjeto();
        }

        else if (RB.angularVelocity.magnitude < 10)
        {
            RB.AddTorque(Vector3.right * Fuerza);
            Empujando = true;
        }
        else
        {
            Empujando = false;
        }
    }
}
