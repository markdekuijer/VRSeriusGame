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

    public Transform spawnMoneyPos;
    public GameObject money;
    public Transform spawnClothPos;
    public GameObject[] Cloths;

    public Transform ai;

    private List<Unit> counterWalkers = new List<Unit>();
    private float counterCurrentTimer;
    private int counterQueueIteration;

    private List<Unit> dressingWalkers = new List<Unit>();
    private float dressingCurrentTimer;
    private int dressingQueueIteration;

    public int spawnAmount;

    public List<Transform> locations = new List<Transform>();

    //Unit tempU;
    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnNewCharacter();
        }

        counterCurrentTimer = maxTimer;
        dressingCurrentTimer = maxTimer;
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //    tempU.ChangeColor(true);
        //else
        //    tempU.ChangeColor(false);

        if (CheckDressingQueue())
            HandleDressingQueue();

    }

    public bool CheckCounterQueue()
    {
        return counterWalkers.Count > 0;
    }
    //public void HandleCounterQueue()
    //{
    //    counterCurrentTimer -= Time.deltaTime;
    //    if (counterCurrentTimer <= 0)
    //    {
    //        counterWalkers[0].Leave();
    //        counterWalkers.RemoveAt(0);
    //        counterQueueIteration--;
    //        for (int i = 0; i < counterWalkers.Count; i++)
    //        {
    //            counterWalkers[i].SetQueuePositions(queuePositions[i]);
    //        }
    //        counterCurrentTimer = maxTimer;
    //    }
    //}
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
        SpawnNewSet();
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

    public bool CheckDressingQueue()
    {
        return dressingWalkers.Count > 0;
    }

    public void HandleDressingQueue()
    {
        dressingCurrentTimer -= Time.deltaTime;
        if (dressingCurrentTimer <= 0 && dressingWalkers.Count > 0)
        {
            if (dressingWalkers.Count > 2)
                SetNextPosIterations(3);
            else if (dressingWalkers.Count > 1)
                SetNextPosIterations(2);
            else if (dressingWalkers.Count > 0)
                SetNextPosIterations(1);

            dressingCurrentTimer = maxTimer;
        }
    }

    public void SetNextPosIterations(int iter)
    {
        for (int j = 0; j < iter; j++)
        {
            dressingWalkers[0].CheckForStealOrBuy();
            dressingWalkers.RemoveAt(0);
            dressingQueueIteration--;

            for (int i = 0; i < dressingWalkers.Count; i++)
            {
                dressingWalkers[i].SetQueuePositions(dressingPositions[i]);
            }
        }
    }

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
        print("added unit " + unit.gameObject.name);
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
        unit.SetQueuePositions(dressingPositions[dressingQueueIteration]);
        dressingWalkers.Add(unit);
        print("walk towards " + dressingPositions[dressingQueueIteration]);
        dressingQueueIteration++;
    }

    public void SpawnNewCharacter()
    {
        Unit u = Instantiate(UnitPrefab, outsideSpawns[UnityEngine.Random.Range(0, outsideSpawns.Count)]).GetComponent<Unit>();
        Vector3 v = UnityEngine.Random.insideUnitSphere * 5;
        v.y = 0;
        u.transform.position += v;
        u.transform.SetParent(ai);

        u.Init(this);
        //tempU = u;
    }
    public void RemoveCharacter(Unit u)
    {

    }
}
