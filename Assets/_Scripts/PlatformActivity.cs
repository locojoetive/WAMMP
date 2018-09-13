﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivity : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Postman")
        {
            transform.parent.GetComponent<Move>().setUnactive();
        }
    }
}