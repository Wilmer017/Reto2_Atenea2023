using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PruebasCodigo : MonoBehaviour
{
    public float Tiempo;
    public int Generacion = 0;
    public int Generaciones = 0;

    public GameObject Objeto;
    public GameObject[] ObjetosCreados;
    public GameObject[] ObjetosGanadores;

    public int Cantidad = 0;
    public int Separacion;

    public VarEntera[] spring_P;
    public VarEntera[] damper_P;
    public VarFlotante[] targetPosition_P;

    [System.Serializable]
    public struct VarEntera
    {
        public int Actual;
        public int Minimo;
        public int Maximo;

        public VarEntera(int A, int L, int H)
        {
            Actual = Random.Range(L, H);
            Minimo = L;
            Maximo = H;
        }
    }
    [System.Serializable]
    public struct VarFlotante
    {
        public float Actual;
        public float Minimo;
        public float Maximo;

        public VarFlotante(float A, float L, float H)
        {
            Actual = Random.Range(L, H);
            Minimo = L;
            Maximo = H;
        }
    }

    private void Start()
    {
        NuevaGeneracion();
    }

    private void Update()
    {
        Tiempo += Time.deltaTime;

        if (Tiempo > 5)
        {
            if (Generacion > Generaciones)
                NuevaGeneracion();
            else
                Time.timeScale = 0;

            SetParetGanadores();

            Tiempo = 0;
        }
    }

    void SetParetGanadores()
    {
        ObjetosGanadores = ObjetosCreados.OrderBy(Objeto => Vector3.Distance(Objeto.transform.GetChild(0).transform.position, Objeto.transform.position)).ToArray();

        GameObject PadreGanadores = new GameObject("Generacion " + Generacion + " Orden");
        foreach (GameObject Item in ObjetosGanadores)
        {
            Item.transform.SetParent(PadreGanadores.transform);
        }
    }
    void NuevaGeneracion()
    {
        Generacion++;

        //if (ObjetosCreados.Length > 0) DestruyePrefab();
        if(Cantidad > 0) ImprimePrefab();
    }
    void DestruyePrefab()
    {
        foreach (GameObject ObjetoABorrar in ObjetosCreados)
        {
            Destroy(ObjetoABorrar);
        }
    }
    void ImprimePrefab()
    {
        ObjetosCreados = new GameObject[Cantidad];

        spring_P = new VarEntera[Cantidad];
        damper_P = new VarEntera[Cantidad];
        targetPosition_P = new VarFlotante[Cantidad];

        for (int i = 0; i < Cantidad; i++)
        {
            spring_P[i] = new VarEntera(0, 0, 5000);
            damper_P[i] = new VarEntera(0, 0, 5000);
            targetPosition_P[i] = new VarFlotante(0, 0, 1);

            ControlJeep CJ;
            if (Objeto.TryGetComponent<ControlJeep>(out CJ)) {
                for (int L = 0; L < CJ.Llantas.Length; L++)
                {
                    CJ.Llantas[L].suspensionSpring = new JointSpring
                    {
                        spring = spring_P[i].Actual,
                        damper = damper_P[i].Actual,
                        targetPosition = targetPosition_P[i].Actual
                    };
                }
            }
            GameObject NuevoObjeto = Instantiate(Objeto);
            NuevoObjeto.transform.SetPositionAndRotation(transform.position + Vector3.right * i * Separacion, transform.rotation);
            NuevoObjeto.transform.SetParent(transform);
            ObjetosCreados[i] = NuevoObjeto;
        }
    }
}
