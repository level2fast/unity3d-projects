using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    int count = 0;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Hit")
        {
            count++;
            string notification = string.Format("You hit an object {0} times!", count);
            Debug.Log(notification);
        }
    }
}
