﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridArray : MonoBehaviour
{
    public GameObject StartingGrid;
    public Terrain ground;
    public int GridSizeX = 10;
    public int GridSizeZ = 10;
    public float GridHyp;
    public bool isGenerated = false;
    public int m_rows;
    public int m_columns;
    public GameObject[,] gridmesh;
    public Text debugtext;
    public Vector2 tempmax, tempmin;

    // Use this for initialization
    void Start()
    {
        GenerateGrid();
        GridHyp = Mathf.Sqrt(GridSizeX * GridSizeX + GridSizeZ * GridSizeZ);

    }

    // Gets gameobject at position passed in or returns null if there is nothing there
    public GameObject GetGridAtPosition(Vector3 position)
    {
        int index_x = (int)(position.x - GridSizeX * 0.5f) / GridSizeX;
        int index_z = (int)(position.z - GridSizeZ * 0.5f) / GridSizeZ;

        if (index_x < 0 || index_x >= 50)
        {
            int a = 0;
        }

        if (index_z < 0 || index_z >= 50)
        {
            int a = 0;
        }

        if (index_x >= 0 && index_x <= m_rows &&
            index_z >= 0 && index_z <= m_columns)
        {
            return gridmesh[index_x, index_z];
        }

        return null;
    }


    public Vector3 SnapBuildingPos(Vector3 position , float size)
    {
        float offset = (size - 1f);
        Vector3 maxpos = new Vector3(position.x + (GridSizeX *0.5f) * offset, position.y, position.z + (GridSizeZ * 0.5f) * offset);
        GameObject max = GetGridAtPosition(maxpos);
        Vector3 snaplocation = max.GetComponent<Grid>().GetWorldPosition();
        snaplocation.z -= (GridSizeZ*0.5f) * offset;
        snaplocation.x -= (GridSizeX*0.5f) * offset;
        RenderBuildGrids(max, size);
        //max.GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);
        return snaplocation;
            
    }
    public void RenderBuildGrids(GameObject max, float size)
    {
        //Debug.Log("showing grid");
        DerenderBuildGrids(false);
        float scale = size -1;
        Vector3 maxpos = max.GetComponent<Grid>().GetWorldPosition();
        Vector2 mxIndex = new Vector2(max.GetComponent<Grid>().position.x, max.GetComponent<Grid>().position.y);
        Vector2 mnIndex = new Vector2(mxIndex.x - scale, mxIndex.y - scale);
        //int diffX = index_maxx - (index_minx + 1);
        //int diffZ = index_maxz - (index_minz + 1);
        int maxX = (int)mxIndex.x; int minX = (int)mnIndex.x;
        int maxY = (int)mxIndex.y; int minY = (int)mnIndex.y;
        for (int i = minX ; i <= maxX; ++i)
        {
            for (int j = minY ; j <= maxY; ++j)
            {
                gridmesh[i, j].GetComponent<Renderer>().enabled = true;
            }
        }
        //store the min max of rendered gfrids
        tempmax = mxIndex;
        tempmin = mnIndex;
    }
    public bool DerenderBuildGrids(bool isbuild)
    {
        bool buildsucess = true;

        if (isbuild)
        {
            for (int i = (int)tempmin.x; i <= (int)tempmax.x; ++i)
            {   for (int j = (int)tempmin.y; j <= (int)tempmax.y; ++j)
                {
                    //Debug.Log("X: " + i + " Y: " + j);
                    if (gridmesh[i, j].GetComponent<Grid>().state == Grid.GRID_STATE.UNAVAILABLE && isbuild)
                    {
                        buildsucess = false;//there is a unavailble slot. Return false and send card back to hand;
                        return buildsucess;
                    }
                }
            }
        }

        for (int i = (int)tempmin.x; i <= (int)tempmax.x; ++i)
        {
            for (int j = (int)tempmin.y; j <= (int)tempmax.y; ++j)
            {

                if(isbuild)//is it render or building(boolean teaken in)
                gridmesh[i, j].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);

                gridmesh[i, j].GetComponent<Renderer>().enabled = false;
                
            }
        }

        return buildsucess;
    }



    // Takes in a gameobject position and scale and returns which grids it occupies in the form on their grid index x and y
    public Vector2[] GetOccupiedGrids(Vector3 position, Vector3 scale)
    {
        int index_x = (int)(position.x - GridSizeX * 0.5f) / GridSizeX;
        int index_z = (int)(position.z - GridSizeZ * 0.5f) / GridSizeZ;
        float halfScaleX = (scale.x * 0.5f - GridSizeX * 0.5f) / GridSizeX;
        float halfScaleZ = (scale.z * 0.5f - GridSizeZ * 0.5f) / GridSizeZ;//scale.z / 10f * 0.5f;
        Vector3 minpos = new Vector3(position.x + halfScaleX, position.y, position.z + halfScaleZ);
        Vector3 maxpos = new Vector3(position.x - halfScaleX, position.y, position.z - halfScaleZ);
        //Vector3 min = new Vector3(position.x + 10, position.y, position.z + 10);
        //Vector3 max = new Vector3(position.x - 10, position.y, position.z - 10);

        int index_minx = (int)(minpos.x - GridSizeX * 0.5f) / GridSizeX;
        int index_minz = (int)(minpos.z - GridSizeZ * 0.5f) / GridSizeZ;
        int index_maxx = (int)(maxpos.x - GridSizeX * 0.5f) / GridSizeX;
        int index_maxz = (int)(maxpos.z - GridSizeZ * 0.5f) / GridSizeZ;

        //gridmesh[index_x, index_z].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);
        //gridmesh[index_minx + 1, index_minz + 1].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);
        //gridmesh[index_maxx, index_maxz].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);

        int diffX = index_maxx - (index_minx + 1);
        int diffZ = index_maxz - (index_minz + 1);
        //for (int i = index_minx + 1; i < index_minx + diffX; ++i)
        //{
        //    Debug.Log("Why you work?!");
        //    for (int j = index_minz + 1; j < index_minz + diffZ; ++j)
        //    {
        //        gridmesh[index_minx + i, index_minz + j].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);
        //    }
        //}
        for (int i = index_minx + 1; i >= index_maxx; --i)
        {
            for (int j = index_minz + 1; j >= index_maxz; --j)
            {
                //Debug.Log("X: " + i + " Y: " + j);
                gridmesh[i, j].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);
                gridmesh[i, j].GetComponent<Renderer>().enabled = true;
            }
        }

        if (index_x < 0 || index_x >= 50)
        {
            int a = 0;
        }

        if (index_z < 0 || index_z >= 50)
        {
            int a = 0;
        }

        if (index_x >= 0 && index_x <= m_rows &&
            index_z >= 0 && index_z <= m_columns)
        {
            //return gridmesh[index_x, index_z];
        }

        return null;
    }

    // Used to highlight a grid that unit is standing on
    public void HighlightUnitPosition(ArrayList list)
    {
        Vector2 index;
        for (int i = 0; i < list.Count; ++i)
        {
            index = (Vector2)list[i];
            if (index.x >= 0 && index.x <= m_rows && index.y >= 0 && index.y <= m_columns)
            {
                gridmesh[(int)index.x, (int)index.y].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.UNAVAILABLE);
                gridmesh[(int)index.x, (int)index.y].GetComponent<Renderer>().enabled = true;
            }
        }
    }

    public Vector3 GetGridPosition(Grid grid)
    {
        if (grid.position.x >= 0 && grid.position.x <= m_rows &&
            grid.position.y >= 0 && grid.position.y <= m_columns)
        {
            return gridmesh[(int)grid.position.x, (int)grid.position.y].GetComponent<Grid>().position;
        }

        return new Vector3(0, 0, 0);
    }

    // Gets grid index in Vec2 at position argument or returns zero vector if theres none
    public Vector3 GetGridIndexAtPosition(Vector3 position)
    {
        // index_x and z are coordinates offsetted by (half of size of 1 grid) and converted to grid coordinates
        int index_x = (int)(position.x - GridSizeX * 0.5f) / GridSizeX;
        int index_z = (int)(position.z - GridSizeZ * 0.5f) / GridSizeZ;

        if (index_x >= 0 && index_x <= m_rows &&
            index_z >= 0 && index_z <= m_columns)
        {
            return new Vector3(index_x, index_z);
        }

        return Vector2.zero;
    }

    // Gets position from supplied grid coordinates
    public Vector3 GetPositionAtGrid(int gridx_, int gridz_)
    {
        float positionX = gridx_ * GridSizeX + GridSizeX * 0.5f; // might lose precision at GetGridAtPosition's static conversion to int but its negligible
        float positionZ = gridz_ * GridSizeZ + GridSizeZ * 0.5f;
        return new Vector3(positionX, 0, positionZ);
    }

    public float GetTerrainHeightAtGrid(Vector3 pos)
    {
        return ground.SampleHeight(pos);
    }

    void GenerateGrid()
    {
        if (isGenerated)
        {
            gridmesh = new GameObject[m_rows, m_columns];
            for (int i = 0; i < transform.childCount; ++i)
            {
                int index_x = (int)transform.GetChild(i).GetComponent<Grid>().position.x;
                int index_z = (int)transform.GetChild(i).GetComponent<Grid>().position.y;
                gridmesh[index_x, index_z] = transform.GetChild(i).gameObject;
            }
                return;
        }
        m_rows = (int)ground.terrainData.size.x / GridSizeX;
        m_columns = (int)ground.terrainData.size.z / GridSizeZ;
        gridmesh = new GameObject[m_rows, m_columns];
        // Create rows
        for (int x = 0; x < m_rows; x++)
        { // Create columns
            for (int z = 0; z < m_columns; z++)
            {
                // Create a copy of the plane and offset it according to [current width, current column] using Instantiate
                GameObject grid = (GameObject)Instantiate(StartingGrid);
                grid.name = "Row: " + x + " Col: " + z;
                grid.transform.position = new Vector3(x * GridSizeX + GridSizeX * 0.5f, 0.8f, z * GridSizeZ + GridSizeZ * 0.5f);
                grid.transform.localScale = new Vector3(GridSizeX, GridSizeZ, 1);
                grid.transform.SetParent(gameObject.transform);

                //disable grid rendering(for actual playtest)
                grid.GetComponent<Renderer>().enabled = false;
                
                grid.GetComponent<Grid>().position.x = x;
                grid.GetComponent<Grid>().position.y = z;
				grid.GetComponent<Grid> ().state = grid.GetComponent<Grid>().CollidedWithTerrain();
                grid.GetComponent<Grid>().UpdateAvailability();
                //, new Vector3(m_startingPlane.transform.position.x + x, m_startingPlane.transform.position.y, m_startingPlane.transform.position.z + z), m_startingPlane.transform.rotation);
                gridmesh[x, z] = grid;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
