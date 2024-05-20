using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerEnd : MonoBehaviour
{
// Name of the scene to load
    public string nextSceneName;

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
