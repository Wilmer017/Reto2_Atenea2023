using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Huracan : MonoBehaviour
{
    public ControlNiveles ControlNivel;

    public ParticleSystem Particulas;
    public int Velocidad;
    public int Distancia;

    public Vector3 VectorDireccion;
    public float Recorrido;

    public void IniciarHuracan()
    {
        Particulas = transform.GetComponent<ParticleSystem>();
        Velocidad = 5;
        Distancia = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Recorrido < Distancia)
        {
            Recorrido += Velocidad * Time.deltaTime;
            transform.position += VectorDireccion * Velocidad * Time.deltaTime;
        }
        else
        {
            int X, Y;

            Recorrido = 0;

            if (transform.position.x > ControlNivel.Largo)
                X = Random.Range(-10, -1);
            else if (transform.position.x < 0)
                X = Random.Range(1, 10);
            else
                X = Random.Range(-10, 11);

            if (transform.position.z > ControlNivel.Ancho)
                Y = Random.Range(-10, -1);
            else if (transform.position.z < 0)
                Y = Random.Range(1, 10);
            else
                Y = Random.Range(-10, 11);

            Velocidad = Random.Range(5, 25);
            Distancia = Random.Range(10, 50);

            VectorDireccion = (Vector3.right * X + Vector3.forward * Y).normalized;
        }

    }
}
