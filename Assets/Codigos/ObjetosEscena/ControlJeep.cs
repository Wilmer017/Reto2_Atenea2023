using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJeep : MonoBehaviour
{
    public Rigidbody RB;
    public WheelCollider[] Llantas;

    public Vector2 InputJugador;
    public float Velocidad;

    public float Dead;
    public int Torque;
    public int Freno;
    public int Angulo;

    public bool NoAvanzar;
    public bool Acelerando;
    public bool Reversa;
    public bool Frenando;
    public bool Girando;

    void Start()
    {
        Dead = 0.2f;
        RB.centerOfMass = Vector3.down * 2;
    }

    private void FixedUpdate()
    {
        Velocidad = RB.velocity.magnitude;

        InputJugador = Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical");

        if (InputJugador.x > 0 || 0 > InputJugador.x)
        {
            Girar();
        }
        else
            Girando = false;

        if (InputJugador.y > 0)
        {
            if (Velocidad > 1 && Reversa) NoAvanzar = true;
            if (NoAvanzar) Frenar();
            else Acelerar();
            Reversa = false;
        }
        else if (InputJugador.y < 0)
        {
            if (Velocidad > 1 && !Reversa) NoAvanzar = true;
            if (NoAvanzar) Frenar();
            else Acelerar();
            Reversa = true;
        }
        else if(InputJugador.y == 0)
        {
            NoAvanzar = false;
        }
    }

    public void Acelerar()
    {
        Acelerando = true;
        if (Frenando)
        {
            Frenando = false;
            Llantas[0].brakeTorque = 0;
            Llantas[1].brakeTorque = 0;
            Llantas[2].brakeTorque = 0;
            Llantas[3].brakeTorque = 0;
        }

        Llantas[0].motorTorque = -Torque * InputJugador.y;
        Llantas[1].motorTorque = -Torque * InputJugador.y;
        Llantas[2].motorTorque = -Torque * InputJugador.y;
        Llantas[3].motorTorque = -Torque * InputJugador.y;
    }
    public void Frenar()
    {
        Frenando = true;
        if (Acelerando)
        {
            Acelerando = false;
            Llantas[0].motorTorque = 0;
            Llantas[1].motorTorque = 0;
            Llantas[2].motorTorque = 0;
            Llantas[3].motorTorque = 0;
        }

        Llantas[0].brakeTorque = Freno; // * InputJugador.y;
        Llantas[1].brakeTorque = Freno;
        Llantas[2].brakeTorque = Freno;
        Llantas[3].brakeTorque = Freno;
    }
    public void Girar()
    {
        Girando = true;
        Llantas[0].steerAngle = Angulo * InputJugador.x;
        Llantas[1].steerAngle = Angulo * InputJugador.x;
    }
}
