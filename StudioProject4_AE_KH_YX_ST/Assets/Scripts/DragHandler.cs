using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {
    public static GameObject itemBeingDragged;
    public Vector3 startPosition;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
        SceneData.sceneData.isHoldingCard = true;
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

#if UNITY_ANDROID
       // Touch myTouch = Input.GetTouch(0);
       // Vector3 newpos = new Vector3(myTouch.position.x, myTouch.position.y, 1);
        transform.position = eventData.position;
	
#else
        toggleCardRender(false);
        GameObject holobuild = itemBeingDragged.GetComponent<UnitCards>().GOModel;
        if (SceneData.sceneData.handhandler.onPlayArea(eventData.position) == true)//outside of panel
        {
            holobuild.SetActive(true);
            //GameObject maxgrid = SharedData.instance.gridmesh.GetMaxGrid(getTerrainPos(), holobuild.GetComponent<Building>().size);
            Vector3 cursorpos = getTerrainPos();
            holobuild.transform.position = SceneData.sceneData.gridmesh.SnapBuildingPos(cursorpos, holobuild.GetComponent<Building>().size);
          
        }
        else
        {
            holobuild.SetActive(false);
            //toggleCardRender(true);
    
        }
        transform.position = Input.mousePosition;


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
        if (SceneData.sceneData.handhandler.onPlayArea(eventData.position) == true)
        {
            SceneData.sceneData.isHoldingCard = false;
            //SharedData.instance.buildhandler.build(spawnedcard.GOModel);

            newbuilding.SetActive(true);
            //sending a true with this statement occupy teh grids
            if (SceneData.sceneData.gridmesh.DerenderBuildGrids(true))//check if there are obstructions and build
            {
                itemBeingDragged = null;
                SceneData.sceneData.handhandler.RemoveCard(gameObject);
                newbuilding.GetComponent<Building>().SetBuilding();
            }
            else
            {
                newbuilding.SetActive(false);
                SceneData.sceneData.handhandler.ResetCardPos();
                SceneData.sceneData.gridmesh.DerenderBuildGrids(false);
                toggleCardRender(true);
            }
        }
        else//not in play area
        {
            //SharedData.instance.handhandler.RemoveCard(gameObject);
            newbuilding.SetActive(false);
            SceneData.sceneData.handhandler.ResetCardPos();
            SceneData.sceneData.gridmesh.DerenderBuildGrids(false);
            SceneData.sceneData.isHoldingCard = false;
            toggleCardRender(true);
            //UnityEngine.Debug.Log("released on panel");
        }

        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

	#endregion
    
    Vector3 getTerrainPos()
    {
        RaycastHit vHit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (SceneData.sceneData.ground.GetComponent<Collider>().Raycast(ray, out vHit, 1000.0f))
        {
            return vHit.point;

        }
        else
            return new Vector3(0, 0, 0);
    }
    void toggleCardRender(bool render)
    {
        //itemBeingDragged.GetComponent<MeshRenderer>().enabled = false;

        for (int i = 0; i < itemBeingDragged.transform.GetChild(0).childCount; ++i)
        {
            itemBeingDragged.transform.GetChild(i).gameObject.SetActive(false);

        }
    }


    // Use this for initialization
    void Start () {
	
	}
}
