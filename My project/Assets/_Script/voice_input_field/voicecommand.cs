using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class VoiceCommand : MonoBehaviour
{
    public float yup;


    public TwoBoneIKConstraint killing_hand;
    public Transform targethand;
    public Transform closestKillPoint; // Add a reference for the closest kill point

    public GameObject Knife;

    public Animator anim;
    public bool wavis, kill;
    public bool letsdance;

    public GameObject[] enemies; // Array to hold all enemies
    private NavMeshAgent agent; // NavMeshAgent for moving the player

    // Dictionary to store voice commands (keywords) and associated actions
    private Dictionary<string, Action> KeywordAction = new Dictionary<string, Action>();
    private KeywordRecognizer keyrecognizer; // Object to recognize spoken keywords

    public GameObject closestEnemy = null;
    public Transform[] killPoints; // Array to hold all possible kill points

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Add voice commands
        KeywordAction.Add("Hellow", Hellow);
        KeywordAction.Add("dance", danceing);
        KeywordAction.Add("Take out Knife", meeliswitcher);
        KeywordAction.Add("kill", attackmelli);
        KeywordAction.Add("Go to the enemy", towardsenemy);

        // Initialize KeywordRecognizer
        keyrecognizer = new KeywordRecognizer(KeywordAction.Keys.ToArray());
        keyrecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keyrecognizer.Start();

        
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Recognized phrase: " + args.text);
        if (KeywordAction.ContainsKey(args.text))
        {
            KeywordAction[args.text].Invoke();
        }
    }

    private void Update()
    {
        agent.stoppingDistance = yup;

        if (kill)
        {
            MoveKillingHandTowardsKillPoint(); // Move hand towards the closest kill point while killing
            killing_hand.weight = 1f;
        }
        if (!kill)
        {
            //killing_hand.weight = 0f;
        }

        if (wavis)
        {
            StartCoroutine(HELLOWANIMATIONFALSE());
        }
        if (kill)
        {
            StartCoroutine(knimeattackmakingfalse());
        }
        if (letsdance)
        {
            StartCoroutine(hiphopdansce());
        }
    }

    void Hellow()
    {
        anim.SetBool("hellow", true);
        wavis = true;
        Debug.Log("Hello recognized!");
    }

    void danceing()
    {
        anim.SetBool("dance", true);
        letsdance = true;
    }

    IEnumerator HELLOWANIMATIONFALSE()
    {
        yield return new WaitForSeconds(5);
        anim.SetBool("hellow", false);
        wavis = false;
    }

    IEnumerator hiphopdansce()
    {
        yield return new WaitForSeconds(4);
        letsdance = false;
        anim.SetBool("dance", false);
    }

    void meeliswitcher()
    {
        Knife.SetActive(true);
    }

    void attackmelli()
    {
        anim.SetBool("Knife_Kill", true);
        kill = true;
    }

    IEnumerator knimeattackmakingfalse()
    {
        yield return new WaitForSeconds(4);
        anim.SetBool("Knife_Kill", false);
        kill = false;
        
        closestKillPoint = null; // Reset closest kill point
    }

    void towardsenemy()
    {
        enemyFinder(); // Ensure closestEnemy is updated
        if (closestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);

            // Check if the distance to the closest enemy is greater than 5 units
            if (distanceToEnemy > agent.stoppingDistance)
            {
                // Set the NavMeshAgent's destination to the closest enemy
                agent.SetDestination(closestEnemy.transform.position);
            }
            else
            {
                // Stop the NavMeshAgent if within 5 units
                agent.ResetPath();
            }
        }
    }

    void enemyFinder()
    {
        float closestDistance = Mathf.Infinity;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceCalculator = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceCalculator < closestDistance)
            {
                closestDistance = distanceCalculator;
                closestEnemy = enemy;
            }
        }
    }

    void MoveKillingHandTowardsKillPoint()
    {
        if (closestKillPoint != null)
        {
            // Move the killing hand towards the closest kill point while the kill animation is playing
            killing_hand.data.target.position = Vector3.MoveTowards(targethand.position, closestKillPoint.position, 5f );
            
        }
    }

    // New method to find the closest kill point
    void FindClosestKillPoint()
    {
        if (killPoints.Length == 0) return;

        float closestDistance = Mathf.Infinity;

        foreach (Transform killPoint in killPoints)
        {
            float distance = Vector3.Distance(transform.position, killPoint.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestKillPoint = killPoint;
            }
        }
    }

    // Call this method before starting the kill animation to find the closest kill point
    void PrepareForKill()
    {
        FindClosestKillPoint();
        if (closestKillPoint != null)
        {
            // Set the target position of the TwoBoneIKConstraint to the closest kill point
            killing_hand.data.target.position = closestKillPoint.position;
        }
    }
}
