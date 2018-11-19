using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public float shopRadius;
    public GameObject shopExit;

    List<RandomWalk> walkers = new List<RandomWalk>();
    public List<Transform> queuePositions = new List<Transform>();

    public float maxTimer = 1;
    float currentTimer;

    public int queueIteration;

    private void Start()
    {
        currentTimer = maxTimer;
    }

    private void Update()
    {
        if(walkers.Count > 0)
        {
            currentTimer -= Time.deltaTime;
            if(currentTimer <= 0)
            {
                walkers[0].Leave();
                walkers.RemoveAt(0);
                queueIteration--;
                for (int i = 0; i < walkers.Count; i++)
                {
                    walkers[i].SetQueuePositions(queuePositions[i]);
                }
                currentTimer = maxTimer;
            }
        }
    }

    public Vector3 GetNewLocationInShop()
    {
        Vector3 newLocation = Random.insideUnitSphere * shopRadius;
        newLocation.y = 0;
        return newLocation;
    }

    public Vector3 GetShopExit()
    {
        return new Vector3(shopExit.transform.position.x, 0, shopExit.transform.position.z);
    }

    public void AddToQueue(RandomWalk walker)
    {
        walker.SetQueuePositions(queuePositions[queueIteration]);
        walkers.Add(walker);
        queueIteration++;
    }
}
