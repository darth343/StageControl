using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UnitCards : MonoBehaviour {

	public string buildingName;
	public string UnitType;
	public int goldValue;
    public GameObject GOModel;
    RectTransform card;
    public Text statsText;
    float w,h;
	// Use this for initialization
	void Start () {
        w = GetComponent<RectTransform>().rect.width;
        h = GetComponent<RectTransform>().rect.height;
        SetText();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnHover()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            card = GetComponent<RectTransform>();
            card.sizeDelta = new Vector2(100, 100);
            Debug.Log("fuck");
        }
    
    }

    public void ResetCardPos()
    {
        SharedData.instance.handhandler.ResetCardPos();
    }

    void OnHoverExit()
    {

    }

    public void SetText()
    {
        statsText.text = "Unit Type: " + UnitType + "\n" + "Gold: " + goldValue;
    }
		
}
