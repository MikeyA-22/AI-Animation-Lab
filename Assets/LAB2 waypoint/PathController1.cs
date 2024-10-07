using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController1 : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    private List<waypoint> thePath;
    waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    bool isSprinting;

    void Start()
    {
        isSprinting = false;
        animator.SetBool("IsSprinting", isSprinting);
        
        thePath = pathManager.GetPath();
        if (thePath.Count != null && thePath.Count > 0)
        {
            target = thePath[0];
        }
        
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            isSprinting = !isSprinting;
            animator.SetBool("IsSprinting", isSprinting);
        }

        if (isSprinting)
        {
            rotateTowardsTarget();
            moveForward();
            
        }
        
            
    }

    private void OnTriggerEnter(Collider other)
    {    
        
        target = pathManager.GetNextTarget();
    }

    private void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;
        
        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void moveForward()
    {
        float stepSize = MoveSpeed * Time.deltaTime;
        float distancetoTarget = Vector3.Distance(transform.position, target.pos);
        if (distancetoTarget < stepSize)
        {
            return;
        }
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

  
}
