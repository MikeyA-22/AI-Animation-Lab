using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navscript : MonoBehaviour
{
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int Attacking1 = Animator.StringToHash("attacking");
    private static readonly int Walking = Animator.StringToHash("walking");
    public GameObject theTarget;
    private NavMeshAgent agent;
    
    
    bool isWalking = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.speed = agent.speed/4;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            agent.destination = theTarget.transform.position;    
        }
        else
        {
            agent.destination = transform.position;
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.name == "Dragon")
        {   
            isWalking = false;
            animator.SetTrigger(Attacking1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name== "Dragon")
        {
            isWalking = true;
            animator.SetTrigger(Walking);
        }
    }
}
