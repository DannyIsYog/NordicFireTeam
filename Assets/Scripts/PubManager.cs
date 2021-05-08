using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PubManager : MonoBehaviour
{
    [SerializeField] public float RandomMax;
    [SerializeField] public float RandomMin;
    private List<ClientController> Clients;
    private List<Places> Places;
    private List<FightZoneScript> fightZones;
    private List<string> irishNames;

    private TextMeshProUGUI goalClient;
    private PlayerController playerController;

    public List<GameObject> clientPrefabs;
    public ClientController CurrentClient;
    private int _total;
    public bool TV;
   
    private float _randomNum;
    private int _randomNumClient;

    #region Events
    public event Action TVOn;
    public event Action TVOff;

    public event Action NewClient;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        irishNames= new List<string>()
        {
       "O'Sullivan" ,"Mc Gregor", 
        "O'Brien" ,
            "O’Neill" ,
             "O’Carroll",
             "O’Reilly" ,
            "McCarthy",
            "Gallagher", 
             "McLoughlin", 
            "Connolly" ,
             "Collins" ,
            "Fitzgerald", 
             "Maguire" ,
             "O’Callaghan", 
            "O’Mahony" ,
           "O’Shea",
           "O’Callaghan",
           "Cunningham",
           "Sheehan",
           "Donelly",
           "Flanagan",
           "Guimaraes",
           "Antunes",
           "Costa",
           "Machado",
           "Sousa",
           "O'Leary",
           "McDonnell",
           "Frans",
           "Rine",
           "Engberg",
           "Sigismund",
           "Amanda"

        };

        Shuffle(irishNames);
        goalClient = GameObject.Find("UIGoal").GetComponent<TextMeshProUGUI>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        TV = true;

        var fights = GameObject.FindGameObjectsWithTag("FightZone");
        fightZones = new List<FightZoneScript>();
        foreach(var f in fights)
        {
            var fController = f.GetComponent<FightZoneScript>();
            fightZones.Add(fController);
        }
        _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
        var objs = GameObject.FindGameObjectsWithTag("Seat");
        var index = 0;
        Places = new List<Places>();
        Clients = new List<ClientController>();
        foreach(var obj in objs)
        {
            Places.Add(obj.GetComponent<Places>());
            var rand = UnityEngine.Random.Range(0, clientPrefabs.Count);
           
            var prefab = clientPrefabs[rand];
            var client = Instantiate(prefab);
            client.transform.position = obj.transform.position;
            var clientController = client.GetComponentInChildren<ClientController>();
            clientController._id = index;
            clientController.pubs = this;
            clientController.name = irishNames[index];
            Clients.Add(clientController);

            index++;

         
        }
        _total = Clients.Count;
        SortCustomers();
        ChooseNextClient();
        playerController.Delivery += ChooseNextClient;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime >= _randomNum)
        {
            SwapMusicState();
        }
    }

    private void SortCustomers()
    {
        Debug.Log("Sorted Customers");
        for (int i = 0; i < _total; i++)
        {
            Places temp = Places[i];
            int randomIndex = UnityEngine.Random.Range(i, _total);
            Places[i] = Places[randomIndex];
            Places[randomIndex] = temp;
        }
    }

    private static  System.Random rng= new System.Random();

    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    void SwapMusicState()
    {
        if (TV)
        {
            TVOff?.Invoke();
            TV = false;
            _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            _randomNum += Time.unscaledTime;

        }
        else
        {
            SortCustomers();
            TVOn?.Invoke();
            TV = true;
            _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            _randomNum += Time.unscaledTime;
        }
    }

    public void ChooseNextClient()
    {
        _randomNumClient = UnityEngine.Random.Range(0, _total);
        CurrentClient = Clients[_randomNumClient];
        NewClient?.Invoke();
        goalClient.text = "Give this to " + CurrentClient.name;
        CurrentClient.WaitingForDrink = true;
    }

    public Places getPlace(int id)
    {
        return Places[id];
    }

  
}
