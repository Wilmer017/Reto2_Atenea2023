using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLanta : MonoBehaviour
{
    public WheelCollider LlantaColicionador;
    public Transform LlantaMesh;

    // Update is called once per frame
    void Update()
    {
        Quaternion Rot;
        Vector3 Pos;
        LlantaColicionador.GetWorldPose(out Pos, out Rot);
        LlantaMesh.SetPositionAndRotation(Pos, Rot);
    }
}
