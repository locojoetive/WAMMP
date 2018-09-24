using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlatforms : MonoBehaviour {
    Move[] platforms;
    public int indexOfActive = 0;
    void Start () {
        referencePlatforms();
        platforms[indexOfActive].setActive();
	}
	
	void Update () {
        if (indexOfActive != -1 && !platforms[indexOfActive].isActive())
        {
            indexOfActive++;
            if (indexOfActive < platforms.Length)
                platforms[indexOfActive].setActive();
            else
            {
                indexOfActive = -1;
            }
        }
	}

    void referencePlatforms()
    {
        GameObject[] unorderedPlatforms = GameObject.FindGameObjectsWithTag("Platform");
        platforms = new Move[unorderedPlatforms.Length];
        foreach (GameObject platform in unorderedPlatforms)
        {
            int i = (int) char.GetNumericValue(platform.name.Substring(9, 1)[0]);
            platforms[i] = platform.GetComponent<Move>();
        }
    }
}
