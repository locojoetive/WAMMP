using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public double currentStage = 1;
    public PostmanStateHandler postman;
    public bool nextStage = false;
    public string activeName;
    private void Start()
    {
        activeName = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        activeName = SceneManager.GetActiveScene().name;
        if (Input.GetKey(KeyCode.Space) && 
            (activeName == "Title" || activeName == "Controls"))
        {
            if(activeName.Equals("Title") )
            {
                SceneManager.LoadScene("Controls", LoadSceneMode.Single);
            } else if (activeName.Equals("Controls"))
            {
                SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
            }
        }
        if (activeName[0] == 'S')
        {
            postman = GameObject.FindGameObjectWithTag("Postman").GetComponent<PostmanStateHandler>();
            currentStage = char.GetNumericValue(SceneManager.GetActiveScene().name.Substring(5, 1)[0]);
            if (postman.isMissionComplete() && Input.GetKeyDown(KeyCode.Space))
            {
                currentStage++;
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
