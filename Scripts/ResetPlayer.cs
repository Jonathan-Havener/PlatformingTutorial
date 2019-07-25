using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    Vector3 startPostition;

    // Start is called before the first frame update
    void Start()
    {
        startPostition = GameObject.Find("Player").transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = startPostition;
        }
    }
}
