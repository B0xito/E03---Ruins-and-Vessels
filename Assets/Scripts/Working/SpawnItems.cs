using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    MeshRenderer riftMR;
    public GameObject[] prefabs;
    public Vector3 spawnAreaSize;
    public float[] SpawnProbabilities;
    public BoxCollider spawnCollider;
    private Coroutine spawnObj;

    private void Start()
    {
        riftMR = GetComponent<MeshRenderer>();
    }

    public void Mining()
    {
        riftMR.enabled = false;
        if (Input.GetKeyDown(KeyCode.F)) 
        { 
            StartCoroutine(InvokeSpawnRandom());
        }
        else
        {
            StopCoroutine(InvokeSpawnRandom());
        }
    }

    private IEnumerator InvokeSpawnRandom()
    {
        Invoke("SpawnRandom", 10);
        yield return new WaitForSeconds(.5f);
    }

    void SpawnRandom()
    {
        // Choose a random prefab using the spawn probabilities
        int prefabIndex = GetRandomPrefabIndex();

        // Get a random position within the spawn area
        Vector3 randomPosition = GetRandomPositionInSpawnArea();

        // Instantiate the prefab at the random position
        Instantiate(prefabs[prefabIndex], randomPosition, Quaternion.identity);

        Debug.Log("GameObject spawned!");
    }

    int GetRandomPrefabIndex()
    {
        // Calculate the total probability
        float totalProbability = 0;
        for (int i = 0; i < SpawnProbabilities.Length; i++)
        {
            totalProbability += SpawnProbabilities[i];
        }

        // Choose a random value between 0 and the total probability
        float randomValue = Random.value * totalProbability;

        // Determine the index of the prefab to spawn
        float currentProbability = 0;
        for (int i = 0; i < SpawnProbabilities.Length; i++)
        {
            currentProbability += SpawnProbabilities[i];
            if (randomValue <= currentProbability)
            {
                return i;
            }
        }

        // This should never happen
        return 0;
    }

    Vector3 GetRandomPositionInSpawnArea()
    {
        // Generate a random position inside the box collider
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x),
            Random.Range(spawnCollider.bounds.min.y, spawnCollider.bounds.max.y),
            Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z)
        );

        // Return the random position
        return randomPosition;
    }
}
