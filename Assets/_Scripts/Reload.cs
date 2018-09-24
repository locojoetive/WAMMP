using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public double currentStage = 0;
    public PostmanStateHandler postman;
    public bool nextStage = false;
    public string activeName = "";
    private void Start()
    {
        activeName = SceneManager.GetActiveScene().name;
        if (activeName[0] == 'S')
        {
            postman = GameObject.FindGameObjectWithTag("Postman").GetComponent<PostmanStateHandler>();
            currentStage = char.GetNumericValue(SceneManager.GetActiveScene().name.Substring(5, 1)[0]);
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            Debug.Log("Let's tryta goo!");
            if (activeName[0] == 'S')
            {
                if (postman.isMissionComplete())
                {
                    currentStage++;
                    SceneManager.LoadScene("Stage" + currentStage, LoadSceneMode.Single);
                }
            }
            else if (activeName == "Title")
            {
                SceneManager.LoadScene("Controls", LoadSceneMode.Single);
            }
            else if (activeName == "Controls")
            {
                Debug.Log("Let'sa goo!");
                SceneManager.LoadScene("Stage1", LoadSceneMode.Single);
            }
        }
        if (currentStage != 0 && (Input.GetKey(KeyCode.R) || postman.isDying()))
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
