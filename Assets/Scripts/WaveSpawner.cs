using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public int EnemiesAlive = 0;

    public Wave[] waves;


    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        else if(countdown<=0)
        {
            StartCoroutine(SpawnWave());
        }

        if (waveIndex == waves.Length)
        {
            waveIndex = waves.Length - 1;
        }

        if (EnemiesAlive>0 && countdown <= 0)
        {
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        EnemiesAlive=1;
        foreach(Members k in wave.enemy)
        {
            for (int i = 0; i < k.count; i++)
            {
                SpawnEnemy(k.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
                EnemiesAlive++;
            }
        }
        EnemiesAlive -= 1;
        Debug.Log("Done");
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, new Vector3(Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10, 1, Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10), Quaternion.identity);
    }

}


[System.Serializable]
public struct Wave
{

    public Members[] enemy;
    public float rate;

}

[System.Serializable]
public struct Members
{
    public GameObject enemy;
    public int count;
}