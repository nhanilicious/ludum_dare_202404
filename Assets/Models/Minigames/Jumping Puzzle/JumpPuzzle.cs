using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpPuzzle : FrogSpawner
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");

        if (other.tag == "Player")
        {
            SpawnFrog();
            Destroy(gameObject);
        }
    }
}
