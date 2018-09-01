using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public string currentStage = "";
    public PostmanStateHandler player;
    public bool nextStage = false;
    private void Start()
    {
        currentStage = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Postman").GetComponent<PostmanStateHandler>();
    }
    private void Update()
    {
        if (player.isMissionComplete())
        {
            nextStage = true;
        }
        if (nextStage && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("The next scene is under construction!");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Postman")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("You dead, bro!");
        }
    }
}
