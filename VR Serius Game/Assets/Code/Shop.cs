using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Initializers")]
    [SerializeField] private GameObject UnitPrefab;
    [SerializeField] private GameObject shopExit;
    [SerializeField] private float shopRadius;
    [SerializeField] private float maxTimer = 10;

    [Header("SpawnLocations")]
    [SerializeField] private List<Transform> outsideSpawns = new List<Transform>();
    [SerializeField] private List<Transform> queuePositions = new List<Transform>();

    [SerializeField] private List<Transform> dressingPositions = new List<Transform>();
    [SerializeField] private List<Transform> dressingWaitlist = new List<Transform>();
    public List<Unit> dressingWalkers = new List<Unit>();

    int currentDressing;

    public Transform spawnMoneyPos;
    public GameObject money;
    public Transform spawnClothPos;
    public GameObject[] Cloths;

    public Transform ai;

    private List<Unit> counterWalkers = new List<Unit>();
    private float counterCurrentTimer;
    private int counterQueueIteration;

    public int spawnAmount;

    public List<Transform> locations = new List<Transform>();
    bool crowdSound;
    float crowdTimer;
    public void Begin()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnNewCharacter();
            crowdSound = true;
        }

        counterCurrentTimer = maxTimer;
    }

    bool playing;
    private void Update()
    {
        if (playing || !crowdSound)
            return;

        if (crowdTimer > 5)
        {
            GetComponent<AudioSource>().Play();
            playing = true;
        }
        crowdTimer += Time.deltaTime;
    }

    public bool CheckCounterQueue()
    {
        return counterWalkers.Count > 0;
    }
    
    public void HandleCounterQueue()
    {
        if (!CheckCounterQueue())
            return;

        counterWalkers[0].Leave();
        counterWalkers.RemoveAt(0);
        counterQueueIteration--;
        for (int i = 0; i < counterWalkers.Count; i++)
        {
            counterWalkers[i].SetQueuePositions(queuePositions[i]);
        }
    }

    public void SpawnNewSet()
    {
        int iterationns = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < iterationns; i++)
        {
            GameObject g = Instantiate(money, spawnMoneyPos.position + (Vector3.up * 0.05f), Quaternion.identity);
            GameObject c = Instantiate(Cloths[UnityEngine.Random.Range(0, 1)], spawnClothPos.position +( (Vector3.up * 0.1f) * i),Quaternion.identity);
        }
    }

    //public void HandleDressingQueue()
    //{
    //    dressingCurrentTimer -= Time.deltaTime;
    //    if (dressingCurrentTimer <= 0 && dressingWalkers.Count > 0)
    //    {
    //        if (dressingWalkers.Count > 2)
    //            SetNextPosIterations(3);
    //        else if (dressingWalkers.Count > 1)
    //            SetNextPosIterations(2);
    //        else if (dressingWalkers.Count > 0)
    //            SetNextPosIterations(1);

    //        dressingCurrentTimer = maxTimer;
    //    }
    //}

    //public void SetNextPosIterations(int iter)
    //{
    //    for (int j = 0; j < iter; j++)
    //    {
    //        dressingWalkers[0].CheckForStealOrBuy();
    //        dressingWalkers.RemoveAt(0);
    //        dressingQueueIteration--;

    //        for (int i = 0; i < dressingWalkers.Count; i++)
    //        {
    //            dressingWalkers[i].SetQueuePositions(dressingPositions[i]);
    //        }
    //    }
    //}

    //public void RemoveDresser(int room)
    //{
    //        dressingWalkers[room].CheckForStealOrBuy();
    //        dressingWalkers.RemoveAt(room);
    //        dressingQueueIteration--;

    //        for (int i = 0; i < dressingWalkers.Count; i++)
    //        {
    //            dressingWalkers[i].SetQueuePositions(dressingPositions[i]);
    //        }

    //}

    

    public Vector3 GetNewLocationInShop()
    {
        Vector3 v = Vector3.zero;
        v = UnityEngine.Random.insideUnitSphere * shopRadius + locations[UnityEngine.Random.Range(0, locations.Count - 1)].position;
        v.y = 0;

        return v;
    }
    public Vector3 GetShopExit()
    {
        return new Vector3(shopExit.transform.localPosition.x, 0, shopExit.transform.localPosition.z);
    }

    public void AddToCounterQueue(Unit unit)
    {
        unit.checkSpawnStuff = true;
        unit.checkSpawnStuffPos = queuePositions[0].position;
        unit.SetQueuePositions(queuePositions[counterQueueIteration]);
        counterWalkers.Add(unit);
        counterQueueIteration++;
    }
    public void AddToDressing(Unit unit)
    {
        AddQueue1(unit);
    }

    private void AddQueue1(Unit unit)
    {
        if(currentDressing <= 2)
        {
            int pos = GetDressingPosition();
            unit.SetQueuePositions(dressingPositions[pos]);
            dressingWalkers[pos] = unit;
            unit.InitDressing(pos);
            currentDressing++;
        }
        else
        {
            int pos = GetWaitingPos();
            unit.SetQueuePositions(dressingWaitlist[pos]);
            print(pos + "  ||  " + pos + dressingPositions.Count);
            dressingWalkers[pos + dressingPositions.Count] = unit;
        }
    }

    public void UpdateNextQueueDressing(int freeRoom)
    {
        dressingWalkers[freeRoom].CheckForStealOrBuy();
        dressingWalkers[freeRoom] = null;
        currentDressing--;

        if (dressingWalkers[3] != null && dressingWalkers[4] != null)
        {
            int pos = GetDressingPosition();
            dressingWalkers[3].SetQueuePositions(dressingPositions[pos]);
            dressingWalkers[3].InitDressing(pos);
            dressingWalkers[pos] = dressingWalkers[3];
            currentDressing++;

            dressingWalkers[3] = dressingWalkers[4];
            dressingWalkers[4] = null;
            dressingWalkers[3].SetQueuePositions(dressingWaitlist[pos]);
        }
        else if (dressingWalkers[3] != null)
        {
            int pos = GetDressingPosition();
            dressingWalkers[3].SetQueuePositions(dressingPositions[pos]);
            dressingWalkers[3].InitDressing(pos);
            dressingWalkers[pos] = dressingWalkers[3];
            dressingWalkers[3] = null;
            currentDressing++;
        }
        else
        {
            Debug.Log("mnmo more waiting");
        }
    }

    public int GetDressingPosition()
    {
        if (dressingWalkers[0] == null)
            return 0;
        if (dressingWalkers[1] == null)
            return 1;
        if (dressingWalkers[2] == null)
            return 2;

        Debug.LogError("ERROR");
        return -1;
    }

    public int GetWaitingPos()
    {
        if (dressingWalkers[3] == null)
            return 0;
        if (dressingWalkers[4] == null)
            return 1;

        Debug.LogError("ERROR");
        return -1;
    }

    public void SpawnNewCharacter()
    {
        Unit u = Instantiate(UnitPrefab, outsideSpawns[UnityEngine.Random.Range(0, outsideSpawns.Count)]).GetComponent<Unit>();
        Vector3 v = UnityEngine.Random.insideUnitSphere * 5;
        v.y = 0;
        u.transform.position += v;
        u.transform.SetParent(ai);

        u.Init(this);
    }
    public void RemoveCharacter(Unit u)
    {

    }
}
