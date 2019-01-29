using System.Collections;
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

    private float maxShopIterationTime = 20f;
    private float currentShopIterationTime;

    [HideInInspector]
    public bool checkSpawnStuff;
    [HideInInspector]
    public Vector3 checkSpawnStuffPos;

    bool fittingCloths;
    float clothTimer = 0;
    float fittingDuration;
    int dressingNumber;

    private void Start()
    {
        fittingDuration = UnityEngine.Random.Range(10f, 30f);
    }

    private void Update()
    {
        if (checkSpawnStuff)
        {
            if(Vector2.Distance(new Vector2(transform.position.x,transform.position.z), new Vector2(checkSpawnStuffPos.x, checkSpawnStuffPos.z)) < 0.1f)
            { 
                shop.SpawnNewSet();
                checkSpawnStuff = false;
            }
        }

        if (Vector3.Distance(transform.position, endPos) < 1)
        {
            shop.SpawnNewCharacter();
            GameObject.Destroy(this.gameObject);
        }

        if (fittingCloths)
        {
            clothTimer += Time.deltaTime;
            if(clothTimer > fittingDuration)
            {
                shop.UpdateNextQueueDressing(dressingNumber);
                fittingCloths = false;
            }
        }

        if (durationInShop <= 0)
            return;

        HandleShopDuration();
    }

    public void InitDressing(int roomNumber)
    {
        fittingCloths = true;
        dressingNumber = roomNumber;
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

        durationInShop = Random.Range(4, 12);
        agent.SetDestination(GetNewLocationInShop());
    }
    public void RandomizeLook()
    {
        visuals.RandomizeLook();
	}
    public void RandomizeStats()
    {
        willDressCloths = (RandomizedStats.dressChange < Random.Range(0, 100));
        stealing = (RandomizedStats.stealingChange < Random.Range(0, 100));
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

    Vector3 endPos = new Vector3(10000,0,0);
    public void Leave()
    {
        Vector3 v = shop.GetShopExit();
        endPos = v;
        agent.SetDestination(v);
        leaving = true;
    }

    public void CheckStolenItems()
    {
        if (leaving && stealing)
        {
            Debug.LogError("Game Over");
        }
    }

    public void ChangeColor(bool on)
    {
        visuals.switchTag(on);
    }
}

public static class RandomizedStats
{
    public static float stealingChange = 100f;
    public static float buyingChange = 80f;
    public static float dressChange = 100;
}