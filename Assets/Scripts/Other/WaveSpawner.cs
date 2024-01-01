using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public UnityEvent OnWaveEnd;
		public GameObject enemy;
		public long count;
		public float rate;
		public bool shouldWaitWaveClear = true;
	}

	public Wave[] waves;
	private int nextWave = 0;
	private int currentWave = 0;

	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public Transform[] spawnPoints;
	public float timeBetweenWaves = 5f;
	public UnityEvent OnWaveEnd;

	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}
		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{

		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive() || !waves[currentWave].shouldWaitWaveClear)
			{
				WaveCompleted(waves[currentWave]);
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0 || nextWave == 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted(Wave _wave)
	{
		Debug.Log("Wave Completed!");

		_wave.OnWaveEnd.Invoke();

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{

			OnWaveEnd.Invoke();
			Debug.Log("ALL WAVES COMPLETE! Looping...");
			this.enabled = false;
		}
		else
		{
			nextWave++;
			currentWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);

		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(GameObject _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);

		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

		GameObject enemyObject = Instantiate(_enemy, _sp.position, _sp.rotation);

		//Spawn attributes here
	}
}
