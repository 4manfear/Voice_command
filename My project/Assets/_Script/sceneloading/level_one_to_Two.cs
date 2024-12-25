using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_one_to_Two : MonoBehaviour
{
    public float unloadDelay; // Time delay before unloading the scene
    public GameObject Player; // Reference to the player object

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Start the coroutine to handle the scene transition
            StartCoroutine(TransitionScene());
            Scene newScene = SceneManager.GetSceneByBuildIndex(2);
            SceneManager.MoveGameObjectToScene(Player, newScene);
        }
    }

    private IEnumerator TransitionScene()
    {
        // Load the new scene additively
        SceneManager.LoadScene(2, LoadSceneMode.Additive);

        // Wait for the scene to be loaded and delay before unloading
        yield return new WaitForSeconds(unloadDelay);

        SceneManager.UnloadSceneAsync(1);
    }
}
