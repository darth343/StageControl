using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {
    public static GameObject itemBeingDragged;
    public Vector3 startPosition;
    //public Transform startParent;
	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
        SharedData.instance.isHoldingCard = true;
		itemBeingDragged = gameObject;
		startPosition = transform.position;
        //startParent = transform.parent;
        //GetComponent<CanvasGroup>().blocksRaycasts = false;

        
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{

        Debug.Log("draging");
#if UNITY_ANDROID
       // Touch myTouch = Input.GetTouch(0);
       // Vector3 newpos = new Vector3(myTouch.position.x, myTouch.position.y, 1);
        transform.position = eventData.position;
	
#else
        transform.position = Input.mousePosition;
#endif
    }

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
        SharedData.instance.isHoldingCard = false;
        UnitCards spawnedcard= gameObject.GetComponent<UnitCards>();
        SharedData.instance.buildhandler.build(spawnedcard.GOModel);


		itemBeingDragged = null;
        Destroy(gameObject);
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

	#endregion
    
    // Use this for initialization
    void Start () {
	
	}
}
