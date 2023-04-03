using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera cam;
    private Vector3 offset;
    private Transform target;
    void Start()
    {
        target = PlayerMotor.instance.transform;
        cam = GetComponent<Camera>();
        offset = transform.position;
    }


}
