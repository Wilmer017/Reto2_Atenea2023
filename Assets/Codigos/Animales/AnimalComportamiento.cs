using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AnimalComportamiento : MonoBehaviour
{
    public ControlNiveles ControlNivel;

    public int Rutina;
    public int ID_ani;
    public float cronometro;
    public int Espera;
    public Animator animacion;
    public Quaternion angulo;
    public float grado;

    public GameObject JugadorTarjet;

    [System.Serializable]
    public class AnimationEntry
    {
        public string animationName;
        public AnimationType type;
    }
    public enum AnimationType
    {
        Trigger,
        Bool
    }
    public enum NombreAnimaciones
    {
        CombatIdle = 0,
        Idle = 1,
        RunForward = 2,
        RunBackward = 3,
        WalkBackward = 4,
        WalkForward = 5,
        Jump = 6,
        Attack1 = 7,
        Attack2 = 8,
        Attack3 = 9,
        Attack5 = 10,
        Buff = 11,
        GetHitFront = 12,
        StunnedLoop = 13,
        Eat = 14,
        Sit = 15,
        Sleep = 16,
        Death = 17
    }


    public List<AnimationEntry> entries = new List<AnimationEntry>();

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }

    public void Comportamiento()
    {
        float Distancia = Vector3.Distance(transform.position, JugadorTarjet.transform.position);

        if (Distancia > 50 || ControlNivel.Personaje.Resguardado == true)
            SinRumbo();
        else if (Distancia > 36)
            Perseguir(1, 3, NombreAnimaciones.WalkForward);
        else if (Distancia > 35)
            Perseguir(0.5f, 3,NombreAnimaciones.Buff);
        else if (Distancia > 3)
            Perseguir(5, 5, NombreAnimaciones.RunForward);
        else if (Distancia > 2 && Distancia < 4)
            switch (Random.Range(0, 5))
            {
                default:
                    Perseguir(1, 7, NombreAnimaciones.Attack5);
                    break;
                case 1:
                    Perseguir(1, 7, NombreAnimaciones.Attack1);
                    break;
                case 2:
                    Perseguir(1, 7, NombreAnimaciones.Attack2);
                    break;
                case 3:
                    Perseguir(1, 7, NombreAnimaciones.Attack3);
                    break;
            }
        else
        {
            SinRumbo();
        }
    }

    void Perseguir(float Velocidad = 1, int giro = 1, NombreAnimaciones AnimacionActiva = NombreAnimaciones.WalkForward)
    {
        Vector3 LookPosicion = JugadorTarjet.transform.position - transform.position;
        LookPosicion.y = 0;

        Quaternion Rotacion = Quaternion.LookRotation(LookPosicion);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotacion, giro);
        transform.Translate(Vector3.forward * Time.deltaTime * Velocidad);

        ReproduceAnimacion(AnimacionActiva);
    }
    void SinRumbo()
    {
        cronometro += Time.deltaTime;

        if (cronometro > Espera)
        {
            Rutina = Random.Range(0, 5);
            cronometro = 0;
        }
        switch (Rutina)
        {
            default:
                ReproduceAnimacion(NombreAnimaciones.Idle);
                Espera = 7;
                break;
            case 1:
                ReproduceAnimacion(NombreAnimaciones.Eat);
                Espera = 5;
                break;
            case 2:
                ReproduceAnimacion(NombreAnimaciones.Sit);
                Espera = 8;
                break;
            case 3:
                ReproduceAnimacion(NombreAnimaciones.Sleep);
                Espera = 15;
                break;
            case 4:
                ReiniciarParametros();
                Rutina++;
                break;
            case 5:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * Time.deltaTime);

                ReproduceAnimacion(NombreAnimaciones.WalkForward);
                Espera = Random.Range(2, 6);
                break;
        }
    }
    void ReiniciarParametros()
    {
        grado = Random.Range(0, 360);
        angulo = Quaternion.Euler(0, grado, 0);
    }

    private void ReiniciarAnimaciones()
    {
        foreach (var entry in entries)
        {
            animacion.SetBool(entry.animationName, false);
        }
    }
    private void ReproduceAnimacion(NombreAnimaciones AnimacionActiva)
    {
        ReiniciarAnimaciones();
        ID_ani = (int) AnimacionActiva;

        if (entries[ID_ani].type == AnimationType.Bool)
        {
            animacion.SetBool(entries[ID_ani].animationName, true);
        }
        else
        {
            animacion.SetTrigger(entries[ID_ani].animationName);
        }
    }
}
