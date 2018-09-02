using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public double currentStage = 0;
    public PostmanStateHandler postman;
    public bool nextStage = false;
    private void Start()
    {
        currentStage = char.GetNumericValue(SceneManager.GetActiveScene().name.Substring(5,1)[0]);
        postman = GameObject.FindGameObjectWithTag("Postman").GetComponent<PostmanStateHandler>();
    }
    private void Update()
    {
        if (postman.isMissionComplete())
        {
            nextStage = true;
        } else if (nextStage && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("The next scene is under construction!");
        }
        if (Input.GetKeyDown(KeyCode.R) || postman.isDying())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
