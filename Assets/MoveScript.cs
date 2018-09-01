using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    private new MeshRenderer renderer;
    public Material materialActive,
        materialInactive;
    public bool xAxisControll = true,
        yAxisControl = true;
    public float moveDistance = 0.0f,
        newValue = 0.0f,
        timeFrame = 0.0f;
    public bool active = false;

    void Start() {
        renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (active)
        {
            newValue += Time.deltaTime;
            moveDistance = newValue > timeFrame ? 1.0f : 0.0f;
            newValue = moveDistance == 1.0f ? 0.0f : newValue;
            HandleXAxisControll();
            HandleYAxisControll();
            renderer.material = materialActive;
        } else
        {
            renderer.material = materialInactive;
        }
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

    void HandleYAxisRotation()
    {

    }

    public bool isActive()
    {
        return active;
    }

    public void setActive()
    {
        active = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Postman")
        {
            active = false;
        }
    }
}
