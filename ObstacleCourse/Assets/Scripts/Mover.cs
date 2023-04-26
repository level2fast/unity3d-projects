using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // SerializeFields makes these variables available
    // in the Unity editor.
    // [SerializeField] float x= 0.00f; 
    // [SerializeField] float y = 0.01f;
    // [SerializeField] float z = 0.0f;
    [SerializeField] float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        PrintInstructions();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    void PrintInstructions()
    {
        Debug.Log("Welcome to the game");
        Debug.Log("Use the arrow keys to move around");
        Debug.Log("Avoid hitting the walls");
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(x, 0, z);
    }
}
