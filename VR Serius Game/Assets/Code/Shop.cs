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

    public Transform ai;

    private List<Unit> counterWalkers = new List<Unit>();
    private float counterCurrentTimer;
    private int counterQueueIteration;

    private List<Unit> dressingWalkers = new List<Unit>();
    private float dressingCurrentTimer;
    private int dressingQueueIteration;

    public int spawnAmount;


    Unit tempU;
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
        if (Input.GetKey(KeyCode.Space))
            tempU.ChangeColor(true);
        else
            tempU.ChangeColor(false);

            //if (CheckCounterQueue())
            //HandleCounterQueue();

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
    }

    public bool CheckDressingQueue()
    {
        return dressingWalkers.Count > 0;
    }

    public void HandleDressingQueue()
    {
        dressingCurrentTimer -= Time.deltaTime;
        if (dressingCurrentTimer <= 0)
        {
            dressingWalkers[0].CheckForStealOrBuy();
            dressingWalkers.RemoveAt(0);
            dressingQueueIteration--;
            for (int i = 0; i < dressingWalkers.Count; i++)
            {
                dressingWalkers[i].SetQueuePositions(dressingPositions[i]);
            }
            dressingCurrentTimer = maxTimer;
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
        return new Vector3(shopExit.transform.localPosition.x, 0, shopExit.transform.localPosition.z);
    }

    public void AddToCounterQueue(Unit unit)
    {
        unit.SetQueuePositions(queuePositions[counterQueueIteration]);
        counterWalkers.Add(unit);
        counterQueueIteration++;
    }
    public void AddToDressing(Unit unit)
    {
        unit.SetQueuePositions(dressingPositions[dressingQueueIteration]);
        dressingWalkers.Add(unit);
        dressingQueueIteration++;
    }

    public void SpawnNewCharacter()
    {
        //GetCharacterFromPool
        Unit u = Instantiate(UnitPrefab, outsideSpawns[Random.Range(0, outsideSpawns.Count)]).GetComponent<Unit>();
        u.transform.SetParent(ai);
        print("spawned");
        u.Init(this);
        tempU = u;
    }
    public void RemoveCharacter(Unit u)
    {

    }
}
