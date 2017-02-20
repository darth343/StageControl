using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Card_Link : System.Object
{
    public CARD_TYPE type;
    public GameObject gm;
}

public class SharedData : MonoBehaviour {
    public static SharedData instance = null;
    public GridArray gridmesh;
    public Text debuginfo;
    public Terrain ground;
    public Camera MainCamera;
    public Canvas UI;
    
    //handlers
    public HandHandler handhandler;
    public DragHandler draghandler;
    public Instantiation buildhandler;
    public List<Card_Link> DatabasePopulater = null;
    public SortedList<CARD_TYPE, GameObject> CardDatabase = new SortedList<CARD_TYPE, GameObject>();

    //Decks
    public GameObject PlayerDeck = null;


    public bool isHoldingCard = false;
	// Use this for initialization
	void Start ()
    {
	    if(instance == null)
        {
            if (DatabasePopulater == null)
            {
                Debug.Log("Database populater is null");
            }

            foreach(Card_Link link in DatabasePopulater)
            {
                CardDatabase.Add(link.type, link.gm);
                Debug.Log(link.type.ToString() + "  " + CardDatabase.Count);
            }

            instance = this;
            if (DatabasePopulater != null)
                DatabasePopulater.Clear();

            if(PlayerDeck != null)
            PlayerDeck.GetComponent<Deck>().GenerateDeck();
        }else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
