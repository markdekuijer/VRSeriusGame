﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private Shop shop;

    private bool stealing;
    private bool willBuySomething;
    private bool willDressCloths;
    private int durationInShop;

    private UnitVisuals visuals;
    private NavMeshAgent agent;
    private bool leaving;

    private float maxShopIterationTime = 8f;
    private float currentShopIterationTime;

    private void Update()
    {
        if (durationInShop <= 0)
            return;

        HandleShopDuration();
    }

    #region Init And Randomize
    private void Awake()
    {
        visuals = GetComponent<UnitVisuals>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void Init(Shop shop)
    {
        this.shop = shop;

        RandomizeLook();
        RandomizeStats();

        durationInShop = Random.Range(3, 7);
        agent.SetDestination(GetNewLocationInShop());
    }
    public void RandomizeLook()
    {
        visuals.RandomizeLook();
	}
    public void RandomizeStats()
    {
        willDressCloths = (RandomizedStats.dressChange < Random.Range(0, 100));
        stealing = false;// (RandomizedStats.stealingChange < Random.Range(0, 100));
                         //if(!stealing)
        willBuySomething = true; //(RandomizedStats.buyingChange < Random.Range(0, 100));
    }
    #endregion

    #region Handle Positions In Shop
    public void HandleShopDuration()
    {
        currentShopIterationTime -= Time.deltaTime;
        if (currentShopIterationTime <= 0)
        {
            agent.SetDestination(GetNewLocationInShop());
            durationInShop--;
            currentShopIterationTime = Random.Range(5,maxShopIterationTime);
            if (durationInShop <= 0)
            {
                if (willBuySomething)
                {
                    shop.AddToDressing(this);
                }
                else if (stealing)
                {
                    shop.AddToDressing(this);
                }
                else
                {
                    Leave();
                }
            }

        }
    }
    public void CheckForStealOrBuy()
    {
        if (stealing)
            Leave();
        else
            shop.AddToCounterQueue(this);
    }

    public Vector3 GetNewLocationInShop()
    {
        return shop.GetNewLocationInShop();
    }
    public void SetQueuePositions(Transform t)
    {
        agent.SetDestination(t.position);
    }
    #endregion

    public void Leave()
    {
        agent.SetDestination(shop.GetShopExit());
        leaving = true;
    }

    public void CheckStolenItems()
    {
        if (leaving && stealing)
        {
            Debug.LogError("Game Over");
        }
    }
}

public static class RandomizedStats
{
    public static float stealingChange = 0f;
    public static float buyingChange = 80f;
    public static float dressChange = 100;
}