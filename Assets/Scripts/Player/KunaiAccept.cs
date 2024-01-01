using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiAccept : MonoBehaviour
{
    public GameObject myAnim;
    public Animator ElevatorAnim;

    public GameObject Kunai;

    public GameObject ElevatorText;

    public GameObject ElevatorObg;

    public GameObject ElevatorCollider;

    private void Start()
    {
        
        myAnim.SetActive(false);
        Kunai.SetActive(true);
        ElevatorText.SetActive(false);
        ElevatorObg.SetActive(false);
        ElevatorCollider.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KunaiAccept")
        {
            myAnim.SetActive(true);
            StartCoroutine(KunaiAccepting());
        }
        if (other.tag == "InElevator")
        {
            ElevatorText.SetActive(false);
            ElevatorCollider.SetActive(true);
            ElevatorAnim.Play("ElevatorClosed");
        }
    }

    IEnumerator KunaiAccepting()
    {
        yield return new WaitForSeconds(0.3f);
        Kunai.SetActive(false);
        ElevatorText.SetActive(true);
        ElevatorObg.SetActive(true);
    }
}
