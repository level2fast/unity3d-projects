using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropped : MonoBehaviour
{
    [SerializeField] float TimeToWait = 3.0f;
    // Start is called before the first frame update
    MeshRenderer render;
    Rigidbody body;
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        render.enabled = false;

        body = GetComponent<Rigidbody>();
        body.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > TimeToWait)
        {
            Debug.Log("3 secs has elapsed, drop.");
            render.enabled = true;
            body.useGravity = true;
        }
    }
}
