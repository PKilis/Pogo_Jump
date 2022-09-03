using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // THIS SCRIPT IS NOT USED FOR NOW.

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
        newPos.x = 0;
        newPos.y = 4.7f;
        gameObject.transform.position = newPos;
    }

}
