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
        Debug.Log(currentStage);
        postman = GameObject.FindGameObjectWithTag("Postman").GetComponent<PostmanStateHandler>();
    }
    private void Update()
    {
        if (!nextStage && postman.isMissionComplete())
        {
            nextStage = true;
            currentStage++;
        } else if (nextStage && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Stage" + currentStage, LoadSceneMode.Single);
        }
        if (Input.GetKey(KeyCode.R) || postman.isDying())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
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
