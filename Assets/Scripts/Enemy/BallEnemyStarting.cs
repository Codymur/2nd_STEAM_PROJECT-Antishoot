using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemyStarting : MonoBehaviour
{
    Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Spawner>();
        spawner.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 20)
        {
            spawner.enabled = true;
        }
    }
}
