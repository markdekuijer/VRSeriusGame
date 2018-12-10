using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour
{
    NavMeshAgent agent;
    float maxTime = 8f;
    float currentTime;

    public Shop shop;

    public int iterationsInShop;
    bool leaving;
    [SerializeField] bool willBuySomething;

	void Start ()
    {
        print("randomWalkerSomewhere");
        iterationsInShop = Random.Range(3, 7);
        agent = GetComponent<NavMeshAgent>();
        currentTime = maxTime;
        agent.SetDestination(GetNewLocationInShop());
        int i = Random.Range(0, 2);
        if (i == 0)
            willBuySomething = false;
        else
            willBuySomething = true;
    }

    void Update ()
    {
        if (iterationsInShop <= 0)
            return;

        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            agent.SetDestination(GetNewLocationInShop());
            iterationsInShop--;
            currentTime = maxTime;
            if (iterationsInShop <= 0)
            {
                if (willBuySomething)
                {
                    //shop.AddToQueue(this);
                }
                else
                {
                    Leave();
                }
            }

        }
    }

    public void Leave()
    {
        agent.SetDestination(shop.GetShopExit());
        leaving = true;
    }

    public void SetQueuePositions(Transform t)
    {
        agent.SetDestination(t.position);
    }

    public Vector3 GetNewLocationInShop()
    {
        return shop.GetNewLocationInShop();
    }
}
