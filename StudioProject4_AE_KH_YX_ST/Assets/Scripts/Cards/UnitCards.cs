﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CARD_TYPE
{
    BALLISTA_FACTORY,
    BUSTER_FACTORY,
    IRON_GOLEM_FACTORY,
    CLOCKWORK_FACTORY,
    RAILGUN_FACTORY,
    SPIDERTANK_FACTORY,
    NEWDECK
}


public class UnitCards : MonoBehaviour {

	public string buildingName;
	public string UnitType;
	public int goldValue;
    public GameObject GOModel;
    RectTransform card;
    public Text statsText;
    public Text nameText;
    public CARD_TYPE cardType;
    float w,h;
	// Use this for initialization
	void Start () {
        w = GetComponent<RectTransform>().rect.width;
        h = GetComponent<RectTransform>().rect.height;
        SetText();
        //take this out tlater
        GOModel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    

    public void GenerateBuilding()
    {
        //create a building when drawn
        GOModel = Instantiate(GOModel);
    }

    public void ConstructBuilding()
    {

    }

    public void ResetCardPos()
    {
        SharedData.instance.handhandler.ResetCardPos();
    }

    public void SetText()
    {
        statsText.text = "Unit Type: " + UnitType + "\n" + "Gold: " + goldValue;
        nameText.text = buildingName;
    }
		
}
