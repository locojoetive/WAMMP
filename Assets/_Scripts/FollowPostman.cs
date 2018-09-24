using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPostman : MonoBehaviour {
    private float interpolation = 1.0f;
    public float minX = 0.0f, 
        minY = 0.0f, 
        maxX = 0.0f, 
        maxY = 0.0f,
        maxSpeed = 20F,
        offsetX = 5F,
        offsetY = -5F;
    public Transform target;
    public bool grounded = false, waitForIt = false, rooted = false, followYAxis = false;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Postman").transform;
	}
	void Update () {
        grounded = target.GetComponent<PostmanStateHandler>().isGrounded();
        Vector3 position = target.position;
        position.x = Mathf.Lerp(transform.position.x, position.x + offsetX, interpolation);
        position.x = position.x < minX ? minX :
            position.x > maxX ? maxX : position.x;
        if (grounded)
        {
            position.y = Mathf.Lerp(transform.position.y, position.y + offsetY, 0.25f * interpolation);
            position.y = position.y < minY ? minY :
                        position.y > maxY ? maxY : position.y;

        }
        else
            position.y = transform.position.y;  
        position.z = transform.position.z;
        transform.position = position;
    }   
}
