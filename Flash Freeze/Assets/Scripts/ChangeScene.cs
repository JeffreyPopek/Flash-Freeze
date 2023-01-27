using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] int sceneToLoad;
    void Start()
    {
        Debug.Log("Changing Scenes Script");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Loaded Scene");
        SceneManager.LoadScene(sceneToLoad);
    }
}
