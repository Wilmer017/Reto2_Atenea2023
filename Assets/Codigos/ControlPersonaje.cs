using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    public ControlNiveles controlNivel;
    public CharacterController controladorCuerpo;
    public Animator controlAnimacion;
    public Camera CamaraPersonaje;
    public Brujula Brujul1;
    public Brujula Brujul2;
    public Brujula Brujul3;
    public Brujula Brujul4;
    public Binoculares Binocular;

    public bool Resguardado;
    public bool Vivo;
    public float velocidad = 6;
    public float tiempoRotacion = 0.1f;
 
    public float fuerzaSalto = 1.0f;
    public float gravedad = 9.81f;

    private float velocidadGiro;
    private float fuerzaGravedad = 0;

    public float Aire = 10;

    float horizontal = 0;
    float vertical = 0;


    public void IniciarPersonaje()
    {
        print("Personaje Inicia");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vivo)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        if (controladorCuerpo.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                fuerzaGravedad = fuerzaSalto;
                controlAnimacion.SetTrigger("Salto");
            }
        }
        else
        {
            fuerzaGravedad -= gravedad * Time.deltaTime;
        }

        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;
        if (direccion.magnitude >= 0.1f && !Binocular.Activados)
        {
            float anguloDeseado = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + CamaraPersonaje.gameObject.transform.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(controladorCuerpo.transform.eulerAngles.y, anguloDeseado, ref velocidadGiro, tiempoRotacion);
            controladorCuerpo.transform.rotation = Quaternion.Euler(0, angulo, 0);

            Vector3 direccionMovimiento = Quaternion.Euler(0f, anguloDeseado, 0f) * Vector3.forward;
            controladorCuerpo.Move(direccionMovimiento.normalized * velocidad * Time.deltaTime);

            if (!Input.GetButton("Fire3"))
            {
                velocidad = 6;
                controlAnimacion.SetBool("Camina", true);
                controlAnimacion.SetBool("Corre", false);
            }
            else
            {
                velocidad = 12;
                controlAnimacion.SetBool("Camina", false);
                controlAnimacion.SetBool("Corre", true);
            }
        }
        else
        {
            controlAnimacion.SetBool("Camina", false);
            controlAnimacion.SetBool("Corre", false);
        }
        
        controladorCuerpo.Move(new Vector3(0, fuerzaGravedad, 0) * Time.deltaTime);


        if(controlNivel.GeneraAgua != null)
        {
            if (controladorCuerpo.transform.position.y + 1.58f < controlNivel.GeneraAgua.Altura && Aire > 0)
            {
                Aire -= Time.deltaTime;
            }
            else if (Aire <= 0)
            {
                controlNivel.Muerto("Sin Aire, Agua");
            }
            else if (controladorCuerpo.transform.position.y + 1.58f > controlNivel.GeneraAgua.Altura && Aire < 10)
            {
                Aire += Time.deltaTime;
            }
        }
    }

    public void Muere()
    {
        horizontal = 0;
        vertical = 0;
        controlAnimacion.SetBool("Vivo", false);
        Vivo = false;
    }
    public void Revive()
    {
        Aire = 10;
        controlAnimacion.SetBool("Vivo", true);
        Vivo = true;
    }
}
