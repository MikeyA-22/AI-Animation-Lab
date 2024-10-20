using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetcontroller : MonoBehaviour
{
    
    private CharacterController controller;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
            controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            controller.Move(Vector3.left);
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            controller.Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            controller.Move(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            controller.Move(Vector3.back);
        }
        
        
        
        
    }
}
