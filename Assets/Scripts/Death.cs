using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Scene scene;
    private GamePlayManager gamePlayManager;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void Restart()
    {
        SceneManager.LoadScene(scene.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Restart();
        }
    }

}
