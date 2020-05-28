﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject Player;

    public float offsetX = 0;
    public float offsetY = 20.0f;
    public float offsetZ = -10.0f;

    Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition.x = Player.transform.position.x + offsetX;
    }
    void LateUpdate()
    {
        //cameraPosition.x = Player.transform.position.x + offsetX;


        cameraPosition.y = Player.transform.position.y + offsetY;
        cameraPosition.z = Player.transform.position.z + offsetZ;

        transform.position = cameraPosition;
    }
}
