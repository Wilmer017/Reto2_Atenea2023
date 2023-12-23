using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetosDispersos : MonoBehaviour
{
    public ControlNiveles ControlNivel;
    public ControlMenu ControlMenu;

    public int DistanciaPersonaje;
    public bool AplicarBordes;
    public int LimiteBorde = 25;

    public int ObjetosCreados;
    public int NumeroMaximo;

    public bool NoRotar;
    public bool DefinirAltura;
    public int Altura;
    public string[] ApareceEn;

    public GameObject[] ObjetosAGenerar;
    public GameObject ObjetoPreCargado;
    public Vector3 Coordenada;
    public Vector3 PosicionSpaw;
    public Vector3 DesplazamientoSpaw;
    public Quaternion RotacionSpaw;
    RaycastHit Rayo;

    public void Iniciar()
    {
        for (int i = 0; i < ObjetosAGenerar.Length; i++)
        {
            MeshFilter MF;
            if (ObjetosAGenerar[i].TryGetComponent<MeshFilter>(out MF))
            {
                print(ObjetosAGenerar[i].name + " Tiene " + MF.sharedMesh.vertexCount + " Vertices");
            }
        }
        if (NumeroMaximo < 1)
        {
            gameObject.name += " (Sin Objetos Creados)";
            gameObject.SetActive(false);
            return;
        }

        if (ApareceEn == null)
            Debug.LogWarning("Aparece En _ No definido por defecto");


        if (name.StartsWith("GeneradorObjetivos")) DistanciaPersonaje = 30;
        else if (name.StartsWith("GeneradorJeep")) DistanciaPersonaje = 20;
        else if (name.StartsWith("GeneradorAnimales")) DistanciaPersonaje = 45;
        else if (name.StartsWith("GeneradorRodadores")) DistanciaPersonaje = 30;
        else if (name.StartsWith("GeneradorTormentas")) DistanciaPersonaje = 50;
        else if (name.StartsWith("GeneradorNieblas")) DistanciaPersonaje = 50;
        else if (name.StartsWith("GeneradorHuracanes")) DistanciaPersonaje = 70;
        else if (name.StartsWith("GeneradorCactus")) DistanciaPersonaje = 10;
        else if (name.StartsWith("GeneradorArboles")) DistanciaPersonaje = 10;
        else Debug.LogWarning(name + ", Generador no Tiene Distancia A Personaje");

        if (gameObject.activeSelf)
        {
            int Intentos = ControlNivel.intentos;
            ObjetosCreados = 0;

            while (ObjetosCreados < NumeroMaximo && Intentos > 0)
            {
                Intentos--;
                CrearObjeto();
            };
        }
    }

    public void CrearObjeto()
    {
        if (ObjetosCreados > NumeroMaximo)
            return;

        int ObjetoID = Random.Range(0, ObjetosAGenerar.Length);
        string NombrePosibleCargado = ObjetosAGenerar[ObjetoID].name;

        if (ApareceEn != null)
        {
            if (NombrePosibleCargado.StartsWith("Oso")) ApareceEn = new string[] { "Pasto" };
            else if (NombrePosibleCargado.StartsWith("Rodante")) ApareceEn = new string[] { "ArenaDuna", "ArenaDesierto" };
            else if (NombrePosibleCargado.StartsWith("Tormenta")) ApareceEn = new string[] { "ArenaDuna", "ArenaDesierto" };
            else if (NombrePosibleCargado.StartsWith("Niebla")) ApareceEn = new string[] { "Pasto", "Nieve" };
            else if (NombrePosibleCargado.StartsWith("Huracan")) ApareceEn = new string[] { "ArenaDuna" };
            else if (NombrePosibleCargado.StartsWith("Cactus")) ApareceEn = new string[] { "ArenaDuna", "ArenaDesierto" };
            else if (NombrePosibleCargado.StartsWith("Arbol")) ApareceEn = new string[] { "Pasto", "ArenaDesierto" };

            else if (NombrePosibleCargado.StartsWith("PanelSolar")) ApareceEn = new string[] { "ArenaDuna", "ArenaDesierto", "Pasto" };
            else if (NombrePosibleCargado.StartsWith("BasuraRio")) ApareceEn = new string[] { "Agua" };

            else ApareceEn = new string[] { "ArenaDuna", "ArenaDesierto", "Pasto", "Nieve" };
        }


        CoordenadaAleatoria();

        if (!NoRotar)
            RotacionAleatoria();

        if (Physics.Raycast(Coordenada, Vector3.down, out Rayo, 99))
        {
            Debug.Log(gameObject.name + " > " + Rayo.collider.name);
            if (Rayo.collider != null)
            {
                for (int i = 0; i < ApareceEn.Length; i++)
                {
                    if (Rayo.collider.name.Equals(ApareceEn[i]))
                    {
                        ObjetoPreCargado = Instantiate(ObjetosAGenerar[ObjetoID], transform);

                        if (!DefinirAltura)
                            ObjetoPreCargado.transform.SetLocalPositionAndRotation(Rayo.point + DesplazamientoSpaw, RotacionSpaw);
                        else
                            ObjetoPreCargado.transform.position = Coordenada + DesplazamientoSpaw + Vector3.down * 105;

                        ObjetosCreados++;

                        if (NombrePosibleCargado.StartsWith("Huracan"))
                        {
                            Huracan H = ObjetoPreCargado.AddComponent<Huracan>();
                            H.ControlNivel = ControlNivel;
                            H.IniciarHuracan();
                        }
                        else if (NombrePosibleCargado.StartsWith("Tormenta"))
                        {
                            Tormenta T = ObjetoPreCargado.AddComponent<Tormenta>();
                            T.ControlNivel = ControlNivel;
                            T.IniciarTormenta();
                        }
                        else if (NombrePosibleCargado.StartsWith("Niebla"))
                        {
                            Niebla N = ObjetoPreCargado.AddComponent<Niebla>();
                            N.ControlNivel = ControlNivel;
                            N.IniciarNiebla();
                        }
                        else if (NombrePosibleCargado.StartsWith("Rodante"))
                        {
                            BolaRodadora R = ObjetoPreCargado.AddComponent<BolaRodadora>();
                            R.ControlNivel = ControlNivel;
                            R.IniciarRodadora();
                        }
                        else if (NombrePosibleCargado.StartsWith("PanelSolar"))
                        {
                            PanelSolar PS;
                            if (ObjetoPreCargado.TryGetComponent<PanelSolar>(out PS))
                            {
                                PS.Control1 = ControlNivel;
                                PS.IniciarPanelSolar();
                            }

                            ControlNivel.TareasSinCompletar = ObjetosCreados;
                            ControlNivel.ActualizaObjetivos();
                        }
                        else if (NombrePosibleCargado.StartsWith("BasuraRio"))
                        {
                            RecogerBasura RB;
                            if (ObjetoPreCargado.TryGetComponent<RecogerBasura>(out RB))
                            {
                                RB.Control1 = ControlNivel;
                                RB.IniciarRecogerBasura();
                            }

                            ControlNivel.TareasSinCompletar = ObjetosCreados;
                            ControlNivel.ActualizaObjetivos();
                        }
                        else if (NombrePosibleCargado.StartsWith("LlamasFuego"))
                        {
                            RecogerLlamas RL;
                            if (ObjetoPreCargado.TryGetComponent<RecogerLlamas>(out RL))
                            {
                                RL.Control1 = ControlNivel;
                                RL.IniciarRecogerLlamas();
                            }

                            ControlNivel.TareasSinCompletar = ObjetosCreados;
                            ControlNivel.ActualizaObjetivos();
                        }
                        else if (NombrePosibleCargado.StartsWith("Oso"))
                        {
                            AnimalComportamiento AC;
                            if (ObjetoPreCargado.TryGetComponent<AnimalComportamiento>(out AC))
                            {
                                AC.JugadorTarjet = ControlNivel.Personaje.controladorCuerpo.gameObject;
                                AC.ControlNivel = ControlNivel;
                            }
                        }
                        else if (NombrePosibleCargado.StartsWith("Arbol"))
                        {
                            if (ApareceEn[i].Equals("Pasto"))
                            {
                                GeneraHojas GH;
                                if (ObjetoPreCargado.TryGetComponent<GeneraHojas>(out GH))
                                {
                                    if (ControlNivel != null)
                                        GH.ControlNivel = ControlNivel;
                                    if (ControlMenu != null)
                                        GH.ControlMenu = ControlMenu;
                                    GH.Iniciar();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void CoordenadaAleatoria()
    {
        float Intentos = ControlNivel.intentos;

        float PersonajeX = 0;
        float PersonajeZ = 0;
        float X;
        float Z;

        if(ControlNivel.Personaje != null)
        {
            PersonajeX = ControlNivel.Personaje.controladorCuerpo.transform.position.x;
            PersonajeZ = ControlNivel.Personaje.controladorCuerpo.transform.position.z;
        }

        do
        {
            Intentos --;
            if (AplicarBordes)
            {
                X = Random.Range(ControlNivel.bordes + LimiteBorde, ControlNivel.Largo - ControlNivel.bordes - LimiteBorde);
                Z = Random.Range(ControlNivel.bordes + LimiteBorde, ControlNivel.Ancho - ControlNivel.bordes - LimiteBorde);
            }
            else
            {
                X = Random.Range(0, ControlNivel.Largo);
                Z = Random.Range(0, ControlNivel.Ancho);
            }

        } while (   PersonajeX < X + DistanciaPersonaje &&
                    PersonajeX > X - DistanciaPersonaje &&
                    PersonajeZ < Z + DistanciaPersonaje &&
                    PersonajeZ > Z - DistanciaPersonaje &&
                    Intentos > 0);

        Coordenada = new Vector3(X, 100, Z);
    }
    void RotacionAleatoria()
    {
        float X = Random.Range(0, 1f);
        RotacionSpaw = new Quaternion( 0, X, 0, 1);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(Coordenada, Vector3.down * 99, Color.red);
        Gizmos.DrawSphere(PosicionSpaw, 0.2f);
    }
}
