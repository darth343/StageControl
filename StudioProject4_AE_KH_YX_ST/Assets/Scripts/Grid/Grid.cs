﻿using UnityEngine;
using System.Collections;


public class Grid : MonoBehaviour
{
    public enum GRID_STATE
    {
        AVAILABLE,
        UNAVAILABLE,
        ISPATH,
        INOPENLIST,
        INCLOSELIST,
    }

    public Material[] materials = new Material[5];
    public Vector2 position;
    public GRID_STATE state;

    public Vector3 GetWorldPosition()
    {
        return new Vector3(position.x * gameObject.transform.parent.GetComponent<GridArray>().GridSizeX + gameObject.transform.parent.GetComponent<GridArray>().GridSizeX * 0.5f, 0, position.y * gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ + gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ * 0.5f);
    }

    public GRID_STATE CollidedWithTerrain()
    {
        Terrain ground = SharedData.instance.ground;
        Vector3 minPos = GetWorldPosition() - (new Vector3(gameObject.transform.parent.GetComponent<GridArray>().GridSizeX * 0.5f, 0, gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ * 0.5f));
        Vector3 maxPos = GetWorldPosition() + (new Vector3(gameObject.transform.parent.GetComponent<GridArray>().GridSizeX * 0.5f, 0, gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ * 0.5f));

        if (0.05 < ground.SampleHeight(minPos) &&  0.05 < ground.SampleHeight(maxPos))
        {
            return GRID_STATE.UNAVAILABLE;
        }
        return GRID_STATE.AVAILABLE;
    }

    void OnValidate()
    {
        UpdateAvailability();
    }

    public void ChangeState(GRID_STATE newstate)
    {
        state = newstate;
        UpdateAvailability();
    }

    public void UpdateAvailability()
    {
       switch(state)
       {
           case GRID_STATE.AVAILABLE:
        {
            GetComponent<Renderer>().material = materials[0];
        }
        break;
           case GRID_STATE.UNAVAILABLE:
        {
            GetComponent<Renderer>().material = materials[1];
        }
        break;
           case GRID_STATE.ISPATH:
        {
            GetComponent<Renderer>().material = materials[2];
        }
        break;
           case GRID_STATE.INOPENLIST:
        {
            GetComponent<Renderer>().material = materials[3];
        }
        break;
           case GRID_STATE.INCLOSELIST:
        {
            GetComponent<Renderer>().material = materials[4];
        }
        break;
    }
    }

    public void Select()
    {

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
