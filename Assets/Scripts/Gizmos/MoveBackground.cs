using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///背景移动
///</summary>
public class MoveBackground : MonoBehaviour
{
    public GameObject Cam;
    public Transform cameraTransform;
    public Vector2 startPoint;
    public float moveRate = 0.2f;
    public bool lockY = false;

    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = Cam.transform;
        startPoint = transform.position;
    }
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector3(startPoint.x + cameraTransform.position.x * moveRate, transform.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(startPoint.x + cameraTransform.position.x * moveRate, startPoint.y + cameraTransform.position.y * moveRate, 0);
        }
    }
}
