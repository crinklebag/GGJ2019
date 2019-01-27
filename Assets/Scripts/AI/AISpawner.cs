using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] GameObject[] aiPrefabsArray;

    [Header("Parameters")]
    [SerializeField] float minSpawnDelay;
    [SerializeField] float maxSpawnDelay;
    [SerializeField] float spawnDelayModifier;
    [SerializeField] float spawnDelayModifierTime;


    List<GameObject> aiMasterList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnNPC());
        StartCoroutine(ModifySpawnDelays());
    }
    
    private IEnumerator SpawnNPC ()
    {
        GameObject newNPC = Instantiate(aiPrefabsArray[Random.Range(0, aiPrefabsArray.Length)], this.transform.position, Quaternion.identity) as GameObject;
        newNPC.GetComponent<AIController>().SetSpawnerReference(this);
        aiMasterList.Add(newNPC);

        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

        StartCoroutine(SpawnNPC());
    }

    private IEnumerator ModifySpawnDelays()
    {
        yield return new WaitForSeconds(spawnDelayModifierTime);

        minSpawnDelay *= spawnDelayModifier;
        maxSpawnDelay *= spawnDelayModifier;

        StartCoroutine(ModifySpawnDelays());
    }

    public void RemoveNPC (GameObject npc)
    {
        aiMasterList.Remove(npc);
    }

    public GameObject GetRandomNPC ()
    {
        if (aiMasterList.Count > 0)
        {
            GameObject[] npcList = new GameObject[aiMasterList.Count];
            npcList = aiMasterList.ToArray();
            return npcList[Random.Range(0, npcList.Length)];
        }

        return null;
    }
}
