using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HoverHandler : MonoBehaviour {
    public RectTransform selected;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnMouseEnter()
    {
        selected = this.gameObject.GetComponent<RectTransform>();
        //Image card = GetComponent<Image>();
        //selected.rectTransform.localScale.Set(3,3,3);
        //selected.rectTransform.rect.size.Set(100, 100);
        Debug.Log("fuck");

    }

    public void OnMouseExit()
    {

    }
}
