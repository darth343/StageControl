using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UnitCards : MonoBehaviour {

	public string buildingName;
	public string UnitType;
	public int goldValue;
    RectTransform card;
    float w,h;
	// Use this for initialization
	void Start () {
        w = GetComponent<RectTransform>().rect.width;
        h = GetComponent<RectTransform>().rect.height;
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

    void OnHoverExit()
    {

    }
		
}
