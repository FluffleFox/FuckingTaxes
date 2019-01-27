using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Enemy;
    float Delay = 2;
    float Reload = 2;
   /* float WaveDruation=5;
    float SpawnDruation=1;
    int EnemyCount;
    int Wave=0;*/

	void Update () {
        if (Reload <= 0)
        {
            Instantiate(Enemy, new Vector3(Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10, 1, Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10), Quaternion.identity);
            if (Delay > -0.2f) { Delay /= 1.1f; }
            Reload = Delay;
        }
        else { Reload -= Time.deltaTime; }
	}
}
