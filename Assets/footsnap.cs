using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class footsnap : MonoBehaviour
{
    void FixedUpdate()
    {
        RaycastHit hit;
        float maxDist = 1.0f;

        if (Physics.Raycast(transform.position, transform.TransformDirection(-1.0f * Vector3.up), out hit, maxDist))
        {
            GameObject go = hit.collider.gameObject;
            if (go != null)
            {
                if (go != this.gameObject)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
