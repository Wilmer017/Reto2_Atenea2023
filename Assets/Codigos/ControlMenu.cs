using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public ControlNiveles ControlNivel;
    public Transform BaseCamara;
    public Transform Camara;
    public Vector3 VectorGiro;

    public bool Girando;
    public int Velocidad;
    public float GradosActuales;
    public float GradosPara;
    public float GradosGiro;

    string[] Biomas = new string[] { "Duna", "Desierto", "Bosque", "Glacial" };
    public string Bioma = "Duna";

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        int Radio = (ControlNivel.Largo + ControlNivel.Ancho) / 4;

        BaseCamara.transform.position = Vector3.right * Radio + Vector3.forward * Radio;
        Camara.transform.localPosition = Vector3.up * Radio / 5 - Vector3.forward * Radio / 1.5f;
    }
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetKeyDown(KeyCode.RightArrow))
            GirarSiguiente();

        else if (Input.GetAxis("Horizontal") < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
            GirarAnterior();

        else if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            Jugar();

        if (GradosActuales > GradosPara - 2 && GradosActuales < GradosPara + 2)
        {
            Girando = false;
        }
        else
        {
            GradosActuales = Vector3.SignedAngle(-BaseCamara.right, transform.right, Vector3.up) + 180;
            BaseCamara.transform.eulerAngles += VectorGiro * Velocidad * Time.deltaTime;
        }
    }

    public void GirarSiguiente()
    {
        if (!Girando)
        {
            Velocidad = -Mathf.Abs(Velocidad);

            GradosPara += GradosGiro;
            while (GradosPara > 360)
            {
                GradosPara -= 360;
            }

            ActualizarBioma();
            Girando = true;
        }
    }
    public void GirarAnterior()
    {
        if (!Girando)
        {
            Velocidad = Mathf.Abs(Velocidad);

            GradosPara -= GradosGiro;
            while (GradosPara < 0)
            {
                GradosPara += 360;
            }

            ActualizarBioma();
            Girando = true;
        }
    }
    public void Jugar()
    {
        if (!Girando)
        {
            if (Bioma.Equals("Duna"))
                SceneManager.LoadScene("Nivel1_CristalesDeSol");
            else if (Bioma.Equals("Desierto"))
                SceneManager.LoadScene("Nivel2_AguaPurificante");
            else if (Bioma.Equals("Bosque"))
                SceneManager.LoadScene("Nivel3_FuegoVigorizante");
            else if (Bioma.Equals("Glacial"))
                SceneManager.LoadScene("Nivel4_AireEnvolvente");
            else
                Debug.LogError(Bioma + ", Escena no Existe");
        }
    }

    void ActualizarBioma()
    {
        switch (GradosPara)
        {
            default:
                Bioma = Biomas[0];
                break;
            case 45:
                Bioma = Biomas[1];
                break;
            case 135:
                Bioma = Biomas[2];
                break;
            case 225:
                Bioma = Biomas[3];
                break;
        }
    }
    public void PedirSalir()
    {
        if(ControlNivel != null)
            ControlNivel.CerrarExe();
    }
}
