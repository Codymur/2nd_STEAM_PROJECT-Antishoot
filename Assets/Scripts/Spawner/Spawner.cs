using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
	public float startTimeBtwSpawn;
	private float timeBtwSpawn;

	public GameObject[] enemies;
	public Transform spawnPos;

	private PlayerController player;

    public int WaitSecondBeforeStart;

	private void Update()
	{

        StartCoroutine(StartingSpawn());
    }

    IEnumerator StartingSpawn()
    {
        yield return new WaitForSeconds(WaitSecondBeforeStart);
        if (timeBtwSpawn <= 0)
        {
            int randEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randEnemy], spawnPos.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    // && player.isDead == false add to 22 string
}
