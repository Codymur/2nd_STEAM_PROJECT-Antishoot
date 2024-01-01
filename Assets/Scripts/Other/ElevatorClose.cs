using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorClose : MonoBehaviour
{

    public Animator ElevatorCloseAnimator;
    public GameObject Collider;

    public void Start()
    {
        Collider.SetActive(false);
    }

    IEnumerator ToLevels()
    {
        Collider.SetActive(true);
        ElevatorCloseAnimator.Play("ElevatorClosed");
        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ToLevels());
        }
    }
}
