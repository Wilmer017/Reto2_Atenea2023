using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlNiveles : MonoBehaviour
{
    public int ID_Nivel;
    public int bordes;
    public int Largo;
    public int Ancho;
    public int intentos = 50;

    public GameObject JuegoPadre;

    public string[] Objetivos;
    public int TareasSinCompletar;
    public int TareasRealizadas;
    public ControlPersonaje Personaje;
    public Transform PorRealizar;
    public Transform Realizados;

    public Transform TerrenosEstaticos;

    public GeneradorTerreno[] GeneraTerrenos;
    public GeneradorAgua GeneraAgua;
    public GeneradorLimites GeneraLimites;

    public GeneradorJugador GeneraPersonajes;
    public GeneradorObjetosDispersos GeneraObjetivos;
    public GeneradorObjetosDispersos GeneraJeep;
    public GeneradorObjetosDispersos GeneraAnimales;
    public GeneradorObjetosDispersos GeneraRodadores;
    public GeneradorObjetosDispersos GeneraTormentas;
    public GeneradorObjetosDispersos GeneraNieblas;
    public GeneradorObjetosDispersos GeneraHuracanes;
    public GeneradorObjetosDispersos GeneraCactus;
    public GeneradorObjetosDispersos GeneraArbolesBosque;
    public GeneradorObjetosDispersos GeneraArbolesDesierto;

    public Vista_Niveles vista_Nivel;
    public Vista_Menu vista_Menu;

    private void Start()
    {
        CrearGeneradoresEstaticos();
        if (GeneraTerrenos == null) ErrorCodigo("Error al generar el Mapa", gameObject);

        IniciarGeneradoresDinamicos();
        if (Personaje == null) {
            ErrorCodigo("Error al generar el Personaje", gameObject);
        } 
        else
        {
            Personaje.controlNivel = this;
            if (ID_Nivel == 1)
            {
                Personaje.Brujul1.Apuntado = Brujula.Apuntadores.Objetivos;
                Personaje.Brujul2.Apuntado = Brujula.Apuntadores.Rodadoras;
                Personaje.Brujul3.Apuntado = Brujula.Apuntadores.Huracanes;
                Personaje.Brujul4.Apuntado = Brujula.Apuntadores.Cactus;
            }
            if (ID_Nivel == 2)
            {
                Personaje.Brujul1.Apuntado = Brujula.Apuntadores.Objetivos;
                Personaje.Brujul2.Apuntado = Brujula.Apuntadores.Rodadoras;
                Personaje.Brujul3.Apuntado = Brujula.Apuntadores.Tormentas;
                Personaje.Brujul4.Apuntado = Brujula.Apuntadores.Cactus;
            }
            if (ID_Nivel == 3)
            {
                Personaje.Brujul1.Apuntado = Brujula.Apuntadores.Objetivos;
                Personaje.Brujul2.Apuntado = Brujula.Apuntadores.Animales;
                Personaje.Brujul3.Apuntado = Brujula.Apuntadores.Niebla;
                Personaje.Brujul4.Apuntado = Brujula.Apuntadores.Jeep;
            }
            if (ID_Nivel == 4)
            {
                Personaje.Brujul1.Apuntado = Brujula.Apuntadores.Objetivos;
                Personaje.Brujul2.Apuntado = Brujula.Apuntadores.Animales;
                Personaje.Brujul3.Apuntado = Brujula.Apuntadores.Niebla;
                Personaje.Brujul4.Apuntado = Brujula.Apuntadores.Jeep;
            }

            Personaje.Brujul1.SetearBrujula();
            Personaje.Brujul2.SetearBrujula();
            Personaje.Brujul3.SetearBrujula();
            Personaje.Brujul4.SetearBrujula();
        }

        if (vista_Nivel != null)
        {
            ActualizaObjetivos();
            vista_Nivel.ControlNivel = this;
            vista_Nivel.IniciarVista();
        }
        else Debug.LogWarning("Vista Nivel No Cargada");

        if (vista_Menu != null)
        {
            ActualizaObjetivos();
            vista_Menu.ControlNivel = this;
            vista_Menu.IniciarVista();
        }
        else Debug.LogWarning("Vista Menu No Cargada");

        if (ID_Nivel > 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        if (ID_Nivel > 0)
        {
            ComprobarVictoria();
            ComprobarPausa();
        }
    }


    void CrearGeneradoresEstaticos()
    {
        int CantidadTerrenos = 1;
        GeneraTerrenos = new GeneradorTerreno[CantidadTerrenos];

        GameObject GeneradorTerreno = new GameObject("Terreno Vacio");
        GameObject GeneradorAgua = new GameObject("Agua");
        GameObject generadorLimites = new GameObject("Limite");

        if (ID_Nivel == -1)
        {
            int RamdonOffsetX = Random.Range(0, 500);
            int RamdonOffsetY = Random.Range(0, 500);

            CantidadTerrenos = 4;
            GeneraTerrenos = new GeneradorTerreno[CantidadTerrenos];
            for (int i = 0; i < CantidadTerrenos; i++)
            {
                if (i > 0)
                    GeneradorTerreno = new GameObject("Terreno Vacio");

                GeneradorTerreno.transform.SetParent(TerrenosEstaticos);
                GeneraTerrenos[i] = GeneradorTerreno.AddComponent<GeneradorTerreno>();

                if (i == 0)
                {
                    GeneradorTerreno.name = "ArenaDuna";
                    GeneraTerrenos[i].Isla = true;
                    GeneraTerrenos[i].DefinirOffSet(RamdonOffsetX, RamdonOffsetY);
                    GeneraTerrenos[i].transform.position = Vector3.zero;
                    GeneraTerrenos[i].Iniciar(Largo / 2, Ancho / 2, 2, 0);
                }
                else if (i == 1)
                {
                    GeneradorTerreno.name = "ArenaDesierto";
                    GeneraTerrenos[i].Isla = true;
                    GeneraTerrenos[i].DefinirOffSet(RamdonOffsetX, RamdonOffsetY, Largo / 2);
                    GeneraTerrenos[i].transform.position = Vector3.right * Largo / 2;
                    GeneraTerrenos[i].Iniciar(Largo / 2, Ancho / 2, 2, 0);
                }
                else if (i == 2)
                {
                    GeneradorTerreno.name = "Pasto";
                    GeneraTerrenos[i].Isla = true;
                    GeneraTerrenos[i].DefinirOffSet(RamdonOffsetX, RamdonOffsetY, Largo / 2, Ancho / 2);
                    GeneraTerrenos[i].transform.position = (Vector3.right * Largo + Vector3.forward * Ancho) / 2;
                    GeneraTerrenos[i].Iniciar(Largo / 2, Ancho / 2, 2, 0);
                }
                else if (i == 3)
                {
                    GeneradorTerreno.name = "Nieve";
                    GeneraTerrenos[i].Isla = true;
                    GeneraTerrenos[i].DefinirOffSet(RamdonOffsetX, RamdonOffsetY, 0, Ancho / 2);
                    GeneraTerrenos[i].transform.position = Vector3.forward * Ancho / 2;
                    GeneraTerrenos[i].Iniciar(Largo / 2, Ancho / 2, 2, 0);
                }
            }

            GeneradorAgua.name = "Agua";
            GeneradorAgua.transform.SetParent(TerrenosEstaticos);
            GeneraAgua = GeneradorAgua.AddComponent<GeneradorAgua>();
            GeneraAgua.Iniciar(Largo, Ancho, 0);

            generadorLimites.name = "Limite";
            generadorLimites.transform.SetParent(TerrenosEstaticos);
            GeneraLimites = generadorLimites.AddComponent<GeneradorLimites>();
            GeneraLimites.Iniciar(bordes, Largo, Ancho, (Largo + Ancho / 2));
        }
        else if (ID_Nivel == 1)
        {
            for (int i = 0; i < CantidadTerrenos; i++)
            {
                GeneradorTerreno.name = "ArenaDuna";
                GeneradorTerreno.transform.SetParent(TerrenosEstaticos);

                GeneraTerrenos[i] = GeneradorTerreno.AddComponent<GeneradorTerreno>();
                GeneraTerrenos[i].Iniciar(Largo, Ancho, 2, 15);
            }

            GeneradorAgua.transform.SetParent(TerrenosEstaticos);
            GeneradorAgua.gameObject.SetActive(false);
            GeneradorAgua.gameObject.name = "Sin Agua";

            generadorLimites.name = "Limite";
            generadorLimites.transform.SetParent(TerrenosEstaticos);
            GeneraLimites = generadorLimites.AddComponent<GeneradorLimites>();
            GeneraLimites.Iniciar(bordes, Largo, Ancho, (Largo + Ancho / 2));
        }
        else if (ID_Nivel == 2)
        {
            for (int i = 0; i < CantidadTerrenos; i++)
            {
                GeneradorTerreno.name = "ArenaDesierto";
                GeneradorTerreno.transform.SetParent(TerrenosEstaticos);

                GeneraTerrenos[i] = GeneradorTerreno.AddComponent<GeneradorTerreno>();
                GeneraTerrenos[i].Iniciar(Largo, Ancho, 2, 5);
            }

            GeneradorAgua.name = "Agua";
            GeneradorAgua.transform.SetParent(TerrenosEstaticos);
            GeneraAgua = GeneradorAgua.AddComponent<GeneradorAgua>();
            GeneraAgua.Iniciar(Largo, Ancho, 4);

            generadorLimites.name = "Limite";
            generadorLimites.transform.SetParent(TerrenosEstaticos);
            GeneraLimites = generadorLimites.AddComponent<GeneradorLimites>();
            GeneraLimites.Iniciar(bordes, Largo, Ancho, (Largo + Ancho / 2));
        }
        else if (ID_Nivel == 3)
        {
            for (int i = 0; i < CantidadTerrenos; i++)
            {
                GeneradorTerreno.name = "Pasto";
                GeneradorTerreno.transform.SetParent(TerrenosEstaticos);

                GeneraTerrenos[i] = GeneradorTerreno.AddComponent<GeneradorTerreno>();
                GeneraTerrenos[i].Iniciar(Largo, Ancho, 2, 4);
            }

            GeneradorAgua.name = "Agua";
            GeneradorAgua.transform.SetParent(TerrenosEstaticos);
            GeneraAgua = GeneradorAgua.AddComponent<GeneradorAgua>();
            GeneraAgua.Iniciar(Largo, Ancho, 1);

            generadorLimites.name = "Limite";
            generadorLimites.transform.SetParent(TerrenosEstaticos);
            GeneraLimites = generadorLimites.AddComponent<GeneradorLimites>();
            GeneraLimites.Iniciar(bordes, Largo, Ancho, (Largo + Ancho / 2));
        }
        else if (ID_Nivel == 4)
        {
            for (int i = 0; i < CantidadTerrenos; i++)
            {
                GeneradorTerreno.name = "Nieve";
                GeneradorTerreno.transform.SetParent(TerrenosEstaticos);

                GeneraTerrenos[i] = GeneradorTerreno.AddComponent<GeneradorTerreno>();
                GeneraTerrenos[i].Iniciar(Largo, Ancho, 2, 40);
            }

            GeneradorAgua.name = "Agua";
            GeneradorAgua.transform.SetParent(TerrenosEstaticos);
            GeneraAgua = GeneradorAgua.AddComponent<GeneradorAgua>();
            GeneraAgua.Iniciar(Largo, Ancho, 5);

            generadorLimites.name = "Limite";
            generadorLimites.transform.SetParent(TerrenosEstaticos);
            GeneraLimites = generadorLimites.AddComponent<GeneradorLimites>();
            GeneraLimites.Iniciar(bordes, Largo, Ancho, (Largo + Ancho / 2));
        }
        else
        {
            GeneradorTerreno.transform.SetParent(TerrenosEstaticos);
            ErrorCodigo("Nivel " + ID_Nivel + ", No Existe", GeneradorTerreno);
        }
    }
    void IniciarGeneradoresDinamicos()
    {
        if (GeneraPersonajes != null)
            GeneraPersonajes.Iniciar();

        if (GeneraObjetivos != null)
        {
            GeneraObjetivos.NumeroMaximo = TareasSinCompletar;
            GeneraObjetivos.Iniciar();
        }

        if (GeneraJeep != null)
            GeneraJeep.Iniciar();
        if (GeneraAnimales != null)
            GeneraAnimales.Iniciar();

        if (GeneraRodadores != null)
            GeneraRodadores.Iniciar();
        if (GeneraTormentas != null)
            GeneraTormentas.Iniciar();
        if (GeneraNieblas != null)
            GeneraNieblas.Iniciar();
        if (GeneraHuracanes != null)
            GeneraHuracanes.Iniciar();
        if (GeneraCactus != null)
            GeneraCactus.Iniciar();
        if (GeneraArbolesBosque != null)
            GeneraArbolesBosque.Iniciar();
        if (GeneraArbolesDesierto != null)
            GeneraArbolesDesierto.Iniciar();
    }

    void ComprobarPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ID_Nivel > 0 && Personaje.Vivo)
        {
            if (JuegoPadre.activeSelf) PausarJuego();
            else DesPausarJuego();
        }
    }
    void ComprobarVictoria()
    {
        if (Personaje.Brujul1.Apuntado.Equals(Brujula.Apuntadores.Jeep))
        {
            if (Personaje.Brujul1.DistanciaMostrar < 5)
            {

                Victoria();
            }
        }
    }
    public void ActualizaObjetivos()
    {
        if (Personaje != null)
        {
            if (TareasRealizadas >= TareasSinCompletar)
            {
                if (Realizados.childCount >= PorRealizar.childCount)
                {
                    Personaje.Brujul1.DistanciaMostrar = 1000;
                    Personaje.Brujul1.Apuntado = Brujula.Apuntadores.Jeep;
                    Personaje.Brujul1.SetearBrujula();
                }
            }
        }
        else
        {
            Debug.LogWarning("Personaje No Detectado");
        }

        if (vista_Nivel != null)
        {
            if(ID_Nivel > 0)
            {
                vista_Nivel.MensajePrimerObjetivo("Objetivos");

                if (Objetivos.Length > 4)
                {
                    ErrorCodigo("Mas de 4 Objetivos", gameObject);
                    return;
                }
                if (Objetivos.Length > 0)
                    if (Objetivos[0] != null)
                    vista_Nivel.MensajeObjetivos(Objetivos[0], Realizados.childCount, TareasSinCompletar);
                if (Objetivos.Length > 1)
                    if (Objetivos[1] != null)
                    vista_Nivel.MensajeObjetivos(Objetivos[1], TareasRealizadas, TareasSinCompletar);
                if (Objetivos.Length > 2)
                    if (Objetivos[2] != null)
                    vista_Nivel.MensajeObjetivos(Objetivos[2], 0, 1);
                if(Objetivos.Length > 3)
                    if (Objetivos[3] != null)
                        vista_Nivel.MensajeObjetivos(Objetivos[3], 0, 1);
            }
        }
        else if (vista_Menu != null)
        {
            vista_Menu.MensajePrimerObjetivo(Objetivos[0]);

            for (int i = 1; i < Objetivos.Length; i++)
            {
                vista_Menu.MensajeObjetivos(Objetivos[i]);
            }
        }
    }

    public void PausarJuego()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        vista_Nivel.MostarPausa();
        vista_Nivel.BotonesMenu.Botones[0].Select();

        JuegoPadre.SetActive(false);
    }
    public void DesPausarJuego()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        vista_Nivel.OcultarPausa();

        JuegoPadre.SetActive(true);
    }
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpcionesJuego()
    {

    }
    public  void MenuPrincipal()
    {
        SceneManager.LoadScene("Niveles");
    }
    public void Muerto(string CausaMuerte, GameObject Enemigo = null)
    {
        if (CausaMuerte.Equals("Sin Aire, Agua")) CausaMuerte = "Te haz ahogado en el rio";
        else if (CausaMuerte.StartsWith("Oso")) CausaMuerte = "Eliminado por un Oso";
        else if (CausaMuerte.StartsWith("Rodante")) CausaMuerte = "Eliminado por una Rodadora";
        else if (CausaMuerte.StartsWith("Tormenta")) CausaMuerte = "Eliminado por una Tormenta";
        else if (CausaMuerte.StartsWith("Niebla")) CausaMuerte = "Eliminado por una Niebla";
        else if (CausaMuerte.StartsWith("Huracan")) CausaMuerte = "Eliminado por un Huracan";
        else if (CausaMuerte.StartsWith("Cactus")) CausaMuerte = "Eliminado por un Cactus";
        else CausaMuerte = " - Eliminado por " + CausaMuerte + " - ";


        Personaje.Muere();
        vista_Nivel.BotonesMenu.CambiarBotonReanudar("Reanudar", "Revivir");
        vista_Nivel.MostrarCausaMuerte(CausaMuerte);
        StartCoroutine(IrPausaMuerte(Enemigo));
    }
    public void Revivir()
    {
        Personaje.Revive();

        vista_Nivel.BotonesMenu.CambiarBotonReanudar("Revivir", "Reanudar");
        DesPausarJuego();
    }
    void Victoria()
    {
        Personaje.Resguardado = true;

        StartCoroutine(IrMenu());
        vista_Nivel.MostrarVictoria();
    }
    IEnumerator IrPausaMuerte(GameObject Enemigo)
    {
        yield return new WaitForSeconds(3);
        PausarJuego();

        if (Enemigo != null)
            Destroy(Enemigo);
        else
            Debug.LogWarning("Enemigo_A_Destruir No Existe");
    }
    IEnumerator IrMenu()
    {
        yield return new WaitForSeconds(3);
        MenuPrincipal();
    }

    void ErrorCodigo(string Descripcion, GameObject contex)
    {
        Debug.LogError(Descripcion, contex);
        gameObject.SetActive(false);
        JuegoPadre.SetActive(false);
        vista_Nivel.gameObject.SetActive(false);
    }
    public void CerrarExe()
    {
        Application.Quit();
    }
}
