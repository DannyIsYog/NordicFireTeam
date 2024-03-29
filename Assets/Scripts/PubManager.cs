using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PubManager : MonoBehaviour
{
    //Random Values
    [SerializeField] public float RandomMax;
    [SerializeField] public float RandomMin;
    private float _randomNum;
    private float _startTime;
    private int _randomNumClient;

    //Lists
    private List<ClientController> Clients;
    private List<Places> Places;
    private List<FightZoneScript> fightZones;
    public List<GameObject> throwablePrefabs;
    public List<Circuit> Circuits;
    private List<string> irishNames;
    private ArrowScript arrowScript;

    //Controllers
    private PlayerController playerController;
    public ClientController CurrentClient;

    public TextMeshProUGUI goalClient;
    public List<GameObject> clientPrefabs;
    private int _total;
    public bool TV;
    private GameObject _TVLight;
    public int Phase;

    #region Events
    public event Action TVOn;
    public event Action TVOff;

    public event Action NewClient;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (Circuit i in Circuits)
        {
            i.Repairs += SwapMusicState;
        }
        Phase = 0;
        _TVLight = GameObject.FindGameObjectWithTag("TVLight");
        Cursor.visible = false;
        irishNames = new List<string>()
        {
       "O'Sullivan" ,"Mc Gregor",
        "O'Brien" ,
            "O'Neill" ,
             "O'Carroll",
             "O'Reilly" ,
            "McCarthy",
            "Gallagher",
             "McLoughlin",
            "Connolly" ,
             "Collins" ,
            "Fitzgerald",
             "Maguire" ,
             "O'Callaghan",
            "O’Mahony" ,
           "O'Shea",
           "O'Callaghan",
           "Cunningham",
           "Sheehan",
           "Donelly",
           "Flanagan",
           "Guimaraes",
           "Antunes",
           "JLopes",
           "Di'Rato",
           "Ru' Prada",
           "O'Connor",
           "Aileen",
           "Happy George",
           "O'Renata",
           "Shane",
           "Uaine",
           "A'Costa",
           "Marc'Reb'Sousa",
           "O'Coins",
           "I'Saltino",
           "The Truck O'Neill",
           "I'gnorant",
           "Patches O'hoolihan",
           "Arsenal Sucks",
           "O'Porto",
           "Costa",
           "Machado",
           "Sousa",
           "O'Leary",
           "McDonnell",
           "Frans",
           "Rine",
           "Engberg",
           "Sigismund",
           "Amanda",
           "O'Brexit"
        };

        Shuffle(irishNames);
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
        _startTime = Time.unscaledTime;

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
        arrowScript = GameObject.Find("Player").GetComponentInChildren<ArrowScript>();
        _total = Clients.Count;
        SortCustomers();
        ChooseNextClient();
        playerController.Delivery += ChooseNextClient;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime - _startTime >= _randomNum && TV)
        {
            SwapMusicState();
        }
    }

    private void SortCustomers()
    {
      
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
            //_randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            //_randomNum += Time.unscaledTime;
            _TVLight.SetActive(false);
            CircuitFry();
        }
        else
        {
            SortCustomers();
            if (Phase < 6)
            {
                Phase += 1;
            }
            TVOn?.Invoke();
            TV = true;
            _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            _randomNum += Time.unscaledTime - _startTime;
            arrowScript.DeActivate();
            _TVLight.SetActive(true);
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

    void CircuitFry()
    {
        int randoms = UnityEngine.Random.Range(0, Circuits.Count);
        Circuits[randoms].Fry();
        arrowScript.Activate(Circuits[randoms].gameObject.transform);

    }

  
}
