using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneratorScript : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject endPoint;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        Prewarm();
        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnCloud(Vector3 startPos) {
        int randomIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        float startY = UnityEngine.Random.Range(startPos.y - 1.5f, startPos.y + 1.5f);
        cloud.transform.position = new Vector3(startPos.x, startY, startPos.z);

        float scale = UnityEngine.Random.Range(0.8f, 1.2f);
        cloud.transform.localScale = new Vector2(scale, scale);

        //cloud.transform.position = startPos;
        float randomSpeed = UnityEngine.Random.Range(1.5f, 4f);
        cloud.GetComponent<CloudScript>().StartFloating(randomSpeed, endPoint.transform.position.x);
    }

    void AttemptSpawn() {

        SpawnCloud(startPos);
        Invoke("AttemptSpawn", spawnInterval);
    }
    
    void Prewarm() {
        for (int i = 0; i < 15; i++)
        {
            Vector3 spawnPos = startPos + Vector3.right * (i * 5);
            SpawnCloud(spawnPos);
        }
    }

}
