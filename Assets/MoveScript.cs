using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public bool xAxisControll = true,
        yAxisControl = true;
    public float moveDistance = 0.0f,
        newValue = 0.0f,
        timeFrame = 0.0f;

    void Start() {

    }

    void Update()
    {
        newValue += Time.deltaTime;
        moveDistance = newValue > timeFrame? 1.0f : 0.0f;
        newValue = moveDistance == 1.0f ? 0.0f : newValue;
        HandleXAxisControll();
        HandleYAxisControll();
    }

    void HandleXAxisControll()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveDistance, 0.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveDistance, 0.0f, 0.0f);
        }
    }

    void HandleYAxisControll()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, moveDistance, 0.0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, -moveDistance, 0.0f);
        }
    }

}
