using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Voice_limit_checker : MonoBehaviour
{
    public Image img;
    
    public GameObject targetObject; // The object whose height will be changed
    [SerializeField]
    private float sensitivity ; // Sensitivity to control loudness threshold
    [SerializeField]
    private float maxHeight ; // Maximum height of the object

    private AudioClip microphoneClip;
    private bool micInitialized;

    void Start()
    {
        // Check if there is at least one microphone available
        if (Microphone.devices.Length > 0)
        {
            microphoneClip = Microphone.Start(null, true, 1, 44100);
            micInitialized = true;
           
        }
        else
        {
            Debug.LogError("No microphone detected. Please connect a microphone to play.");
            
        }
    }

    void Update()
    {
        Renderer targetRenderer = targetObject.GetComponent<Renderer>();

        if (micInitialized)
        {
            // Get the current loudness of the audio input
            float loudness = GetMicLoudness();

            // Adjust height of the object based on loudness
            float newHeight = Mathf.Clamp(loudness * sensitivity, 0, maxHeight);
            targetObject.transform.localScale = new Vector3(1, newHeight, 1);
        }
        if (targetObject.transform.localScale.y >= 7)
        {
            Debug.Log("enemy's are coming RUNNNNNNN");
            img.color = Color.red;
            
        }
        if(targetObject.transform.localScale.y <=7 && targetObject.transform.localScale.y >= 5 )
        {
            img.color = Color.yellow;
        }
        if (targetObject.transform.localScale.y <= 5 && targetObject.transform.localScale.y >= 0)
        {
            img.color = Color.green;
        }

    }

    // Function to calculate microphone loudness
    float GetMicLoudness()
    {
        //Defines the number of audio samples (256 in this case) to analyze for calculating loudness.
        int sampleSize = 256;

        //Creates an array to store the audio samples.
        float[] samples = new float[sampleSize];

        //Finds the current recording position of the microphone, then subtracts sampleSize + 1 to get the starting position for audio samples.
        int micPosition = Microphone.GetPosition(null) - (sampleSize + 1);
        
        //Ensures the microphone has recorded enough data.If not, returns 0(indicating no sound).
        if (micPosition < 0) return 0;

        //Fills the samples array with audio data from the microphone starting at micPosition.
        microphoneClip.GetData(samples, micPosition);

        //Calculating the Loudness:
        float sum = 0;
        for (int i = 0; i < sampleSize; i++)
        {
            sum += samples[i] * samples[i];
        }

        //alculating the Root Mean Square (RMS) Value
        return Mathf.Sqrt(sum / sampleSize);
    }
}