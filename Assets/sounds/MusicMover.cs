using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMover : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("GameMusic");
        string activeScene = SceneManager.GetActiveScene().name;
        if(musicObjs.Length > 1 || activeScene == "Level1")
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    private void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if ( activeScene == "Level1")
        {
            Destroy(this.gameObject);
        }
    }
}
