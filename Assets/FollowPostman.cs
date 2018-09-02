﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPostman : MonoBehaviour {
    public float minX = 0.0f, 
        minY = 0.0f, 
        maxX = 0.0f, 
        maxY = 0.0f,
        interpolation = 1.0f,
        maxSpeed = 20F;
    public Transform target;
    public bool grounded = false, rootCamera = false, rooted = false;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Postman").transform;    	
	}
	void Update () {
        rooted = grounded;
        grounded = target.GetComponent<PostmanStateHandler>().isGrounded();
        if(!rootCamera && !rooted && grounded)
        {
            rootCamera = true;
        }
        Vector3 position = target.position;
        float left = minX;
        float right = maxX;
        float bottom = minY;
        float up = maxY;
        interpolation = maxSpeed * Time.deltaTime;

        position.x = Mathf.Lerp(transform.position.x, position.x, interpolation);
        if (rootCamera)
        {
            position.y = Mathf.Lerp(transform.position.y, position.y, 0.5f * interpolation);
        } else position.y = transform.position.y;
        if (Mathf.Abs(transform.position.y - position.y) < 0.1f)
            rootCamera = false;

        position.z = transform.position.z;

        position.x = position.x < minX ? minX :
            position.x > maxX ? maxX : position.x;
        position.y = position.y < minY ? minY :
                    position.y > maxY ? maxY : position.y;

        transform.position = position;
    }
}