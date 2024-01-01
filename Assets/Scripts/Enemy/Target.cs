using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public int PickUpChance;
    public GameObject[] pickUps;


    private Outline outline;

    public bool isHeaderEnemy;
    public bool isTurretEnemy;
    public GameObject HeaderParent;

    public GameObject Organs;


    public ScoreManager scoreManager;
    public int TargetPoint;
    public float MinigunFillerPoints;



    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        outline = gameObject.GetComponentInChildren<Outline>();


        if (isHeaderEnemy)
        {
            HeaderParent = this.transform.parent.gameObject;
        }

        if (isTurretEnemy)
        {
            HeaderParent = this.transform.parent.parent.gameObject;
        }

    }
    public void TakeDamage(float amount)
    {
        StartCoroutine(TakingDamageEffect());
        health -= amount;
        if (health <= 0f)
        {
            scoreManager.AddToScore(TargetPoint);
            scoreManager.MinigunSliderFiller(MinigunFillerPoints);
            Instantiate(Organs, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < PickUpChance)
            {
                GameObject randomPickUp = pickUps[Random.Range(0, pickUps.Length)];
                Instantiate(randomPickUp, transform.position, transform.rotation);
            }

            if (!isHeaderEnemy && !isTurretEnemy)
            {
                Die();
            }
            else if (isHeaderEnemy || isTurretEnemy)
            {
                HeaderDie();
            }
            
        }
    }

    IEnumerator TakingDamageEffect()
    {
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = outline.OutlineWidth + 2f;
        yield return new WaitForSeconds(0.3f);
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = outline.OutlineWidth - 2f;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void HeaderDie()
    {
        Destroy(HeaderParent);
    }
}
