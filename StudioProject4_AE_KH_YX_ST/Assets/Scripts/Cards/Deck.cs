﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Deck_Detail : System.Object
{
    public int CardAmount;
    public CARD_TYPE CardType;
}

public class Deck : MonoBehaviour {

    public List<Deck_Detail> CardsToInclude = null;
    public List<GameObject> Cards;
    
	// Use this for initialization
	void Start ()
    {

	}

    public void GenerateDeck()
    {
        if (CardsToInclude == null)
            return;

        Cards = new List<GameObject>();

        foreach (Deck_Detail detail in CardsToInclude)
        {
            GameObject temp = null;
            Debug.Log("TEST: " + SharedData.instance.CardDatabase.Count);
            SharedData.instance.CardDatabase.TryGetValue(detail.CardType, out temp);
            if (temp != null)
            {
                for (int i = 0; i < detail.CardAmount; ++i)
                {
                    Cards.Add(Instantiate(temp));
                }
            }
        }

        ShuffleDeck();

        if (CardsToInclude != null)
            CardsToInclude.Clear();   
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void ShuffleDeck()
    {
        for (int i = 0; i < 50; ++i)
        {
            int randomIndex1 = Random.Range(0, Cards.Count);
            int randomIndex2 = Random.Range(0, Cards.Count);
            GameObject temp = Cards[randomIndex1];
            Cards[randomIndex1] = Cards[randomIndex2];
            Cards[randomIndex2] = temp;

        }
    }

    //public void DrawCard()
    //{

    //    var firstCard = Cards.GetEnumerator().Current;
    //    //var lastCard = Cards.;

    //}
}
