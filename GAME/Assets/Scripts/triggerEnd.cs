using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerEnd : MonoBehaviour
{
    // This method is called when another collider enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
