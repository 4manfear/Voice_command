using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ai_move_towards_sound : MonoBehaviour
{
    
    [SerializeField]
    private float hearing_range;

    [SerializeField]
    private Voice_limit_checker vlc;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private List<NavMeshSurface> navmeshsurface = new List<NavMeshSurface>();

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private List<AudioSource> audiosources = new List<AudioSource>();


    private AudioSource currentSoundSource;
    Transform playertransform;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playertransform = Player.transform;

        audiosources.AddRange(FindObjectsOfType<AudioSource>());
        FindAllNavMeshSurface();

        

        // Set the stopping distance to 5 units
        agent.stoppingDistance = 4f;

        

    }

    private void Update()
    {
        // Constantly check for sound playing and move enemy accordingly
        DetectSoundAndMoveTowardsSource();

        if(vlc.targetObject.transform.localScale.y>7f)
        {
            playerposition();
        }
    }

    void playerposition()
    {
        float distanceToSound = Vector3.Distance(transform.position, playertransform.transform.position);
        if (distanceToSound < hearing_range)
        {
            MoveTowardsSoundSource(playertransform.position);
        }
        
    }

    void FindAllNavMeshSurface()
    {
        // finding all the navmeshsurface componenet and added it in the list
        navmeshsurface.AddRange(FindObjectsOfType<NavMeshSurface>());
    }
    
    void BakeAllNavMeshes()
    {
        if (navmeshsurface.Count > 0)
        {
            //looping throught all the surface and back the navemesh
            foreach(NavMeshSurface surface in navmeshsurface)
            {
                surface.BuildNavMesh();
                //Debug.Log($"Baked NavMesh for {surface.gameObject.name}");
            }

           // Debug.Log("All NavMeshes baked successfully.");
        }
        else
        {
            //Debug.LogWarning("No NavMeshSurface found in the scene!");
        }
    }

    void DetectSoundAndMoveTowardsSource()
    {
        // Loop through all audio sources to find if any is playing
        foreach (AudioSource audioSource in audiosources)
        {
            if (audioSource.isPlaying)
            {
                // Check if the sound source is within the hearing range
                float distanceToSound = Vector3.Distance(transform.position, audioSource.transform.position);
                if (distanceToSound <= hearing_range)
                {
                    currentSoundSource = audioSource; // Track the active sound source
                    BakeAllNavMeshes();

                    // Move the NavMeshAgent towards the sound source
                    MoveTowardsSoundSource(currentSoundSource.transform.position);
                }
                break;  // Exit the loop once we find the first playing sound
            }
        }
    }

    // Function to move the NavMeshAgent towards the sound's position
    void MoveTowardsSoundSource(Vector3 targetPosition)
    {
        if (agent != null)
        {
            float distance = Vector3.Distance(agent.transform.position, targetPosition);

            // Move towards the sound source only if it is more than 2 units away
            if (distance > agent.stoppingDistance)
            {
                agent.SetDestination(targetPosition); // Set the destination for the agent to move
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set the color of the sphere to red
        Gizmos.DrawWireSphere(transform.position, hearing_range); // Draw the hearing range as a wireframe sphere
    }


}
