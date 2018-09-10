using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public bool axisControll = true,
        rotationControll = true;
    public float moveDistance = 0.0f,
        rotateDistance = 1.0f,
        newValue = 0.0f,
        timeFrame = 0.1f;
    public bool active = false;

    void Start() {
    }

    void Update()
    {
        if (active)
        {
            HandleControlls();
            CalculateMoveStep();
            if (axisControll) {
                HandleXAxisControll();
                HandleYAxisControll();
            }
            if(rotationControll)
                HandleRotation();
        } else
        {

        }
    }

    void HandleControlls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rotationControll = true;
            axisControll = false;
        }
        else
        {
            rotationControll = false;
            axisControll = true;
        }
    }

    void CalculateMoveStep()
    {
        newValue += Time.deltaTime;
        moveDistance = newValue > timeFrame ? 1.0f : 0.0f;
        newValue = moveDistance == 1.0f ? 0.0f : newValue;
    }

    void HandleXAxisControll()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-moveDistance, 0.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(moveDistance, 0.0f, 0.0f);
        }
    }

    void HandleYAxisControll()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0.0f, moveDistance, 0.0f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0.0f, -moveDistance, 0.0f);
        }
    }

    void HandleRotation()
    {
        float rotateAngle = moveDistance * rotateDistance;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward, rotateAngle);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.forward, -rotateAngle);
        }
    }

    public bool isActive()
    {
        return active;
    }

    public void setActive()
    {
        active = true;
    }
    public void setUnactive()
    {
        active = false;
    }
}
