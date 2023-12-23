using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HojaApuntaCamara : MonoBehaviour
{
    public Transform Objetivo;
    public Vector3 OffSet;

    public void Iniciar(Transform O, Vector3 V)
    {
        CambioHoja();
        Objetivo = O;
        OffSet = V;
}

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Objetivo.transform.position - transform.position + OffSet);
    }

    public void CambioHoja()
    {
        float X = 0.25f * Random.Range(0, 5);
        float Y = 0.125f * Random.Range(0, 9);
        GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(X, Y);
    }
}
