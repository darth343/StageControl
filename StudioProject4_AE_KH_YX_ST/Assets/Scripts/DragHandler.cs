using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {
    public static GameObject itemBeingDragged;
    public Vector3 startPosition;
    public Stopwatch debugtimer = new Stopwatch();
    //public Transform startParent;
	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
        SharedData.instance.isHoldingCard = true;
		itemBeingDragged = gameObject;
		startPosition = transform.position;

        //dragged onto play area , render the hologram mode
        
        //startParent = transform.parent;
        //GetComponent<CanvasGroup>().blocksRaycasts = false;

        
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{

        //Debug.Log("draging");
#if UNITY_ANDROID
       // Touch myTouch = Input.GetTouch(0);
       // Vector3 newpos = new Vector3(myTouch.position.x, myTouch.position.y, 1);
        transform.position = eventData.position;
	
#else
        debugtimer.Start();
        GameObject holobuild = itemBeingDragged.GetComponent<UnitCards>().GOModel;
        if (SharedData.instance.handhandler.onPlayArea(eventData.position) == true)//outside of panel
        {
            holobuild.SetActive(true);
            //GameObject maxgrid = SharedData.instance.gridmesh.GetMaxGrid(getTerrainPos(), holobuild.GetComponent<Building>().size);
            Vector3 cursorpos = getTerrainPos();
            holobuild.transform.position = SharedData.instance.gridmesh.SnapBuildingPos(cursorpos, holobuild.GetComponent<Building>().size);
        }
        else
        {
            holobuild.SetActive(false);
        }
        transform.position = Input.mousePosition;
        debugtimer.Stop();

        //UnityEngine.Debug.Log("On Drag: " + debugtimer.Elapsed);
#endif
    }

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
        //have to add mobile compatability later
        //check if card is in play area
        UnitCards spawnedcard = gameObject.GetComponent<UnitCards>(); 
        GameObject newbuilding = spawnedcard.GOModel;
        if (SharedData.instance.handhandler.onPlayArea(eventData.position) == true)
        {
            SharedData.instance.isHoldingCard = false;
            //SharedData.instance.buildhandler.build(spawnedcard.GOModel);

            newbuilding.SetActive(true);
            //sending a true with this statement occupy teh grids
            if (SharedData.instance.gridmesh.DerenderBuildGrids(true))//check if there are obstructions and build
            {
                itemBeingDragged = null;
                SharedData.instance.handhandler.RemoveCard(gameObject);
            }
            else
            {
                newbuilding.SetActive(false);
                SharedData.instance.handhandler.ResetCardPos();
                SharedData.instance.gridmesh.DerenderBuildGrids(false);
            }
        }
        else//not in play area
        {
            //SharedData.instance.handhandler.RemoveCard(gameObject);
            newbuilding.SetActive(false);
            SharedData.instance.handhandler.ResetCardPos();
            //UnityEngine.Debug.Log("released on panel");
        }

        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

	#endregion
    
    Vector3 getTerrainPos()
    {
        RaycastHit vHit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (SharedData.instance.ground.GetComponent<Collider>().Raycast(ray, out vHit, 1000.0f))
        {
            return vHit.point;

        }
        else
            return new Vector3(0, 0, 0);
    }

    // Use this for initialization
    void Start () {
	
	}
}
