using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HojaApuntaCamara : MonoBehaviour
{
    public Transform Objetivo;
    public Vector3 OffSet;

    public void Iniciar(Transform O, Vector3 V)
    {
        Objetivo = O;
        OffSet = V;
}

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Objetivo.transform.position - transform.position + OffSet);
    }
}
