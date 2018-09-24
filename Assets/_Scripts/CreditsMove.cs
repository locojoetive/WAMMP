using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMove : MonoBehaviour {
    public float time = 0.0f;
	void Update () {
        time += Time.deltaTime;
        transform.Translate(new Vector2(0.0f, 0.05f));
        if (time > 18F || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
    }
}
