using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private float spawnInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, EnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        PhotonNetwork.Instantiate(EnemyPrefab.name, new Vector3(Random.Range(-15f, 24f), Random.Range(4f, 12f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, EnemyPrefab));
    }
}