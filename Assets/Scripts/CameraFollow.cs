using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance = null;

    [HideInInspector]
    public Vector3 distance;

    private Vector3 newPos;
    void Start()
    {
        distance = gameObject.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
    }


    private void FixedUpdate()
    {
        newPos = GameObject.FindGameObjectWithTag("Player").transform.position + distance;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPos, 0.125f);
        gameObject.transform.position = smoothedPosition;
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}