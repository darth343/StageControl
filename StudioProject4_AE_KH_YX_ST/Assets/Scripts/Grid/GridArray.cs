using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridArray : MonoBehaviour
{
    public GameObject StartingGrid;
    public Terrain ground;
    public int GridSizeX = 10;
    public int GridSizeZ = 10;
    public bool isGenerated = false;
    public int m_rows;
    public int m_columns;
    public GameObject[,] gridmesh;
    public Text debugtext;

    // Use this for initialization
    void Start()
    {
        GenerateGrid();
    }

    // Gets gameobject at position passed in or returns null if there is nothing there
    public GameObject GetGridAtPosition(Vector3 position)
    {
        int index_x = (int)(position.x - GridSizeX * 0.5f) / GridSizeX;
        int index_z = (int)(position.z - GridSizeZ * 0.5f) / GridSizeZ;

        if (index_x >= 0 && index_x <= m_rows &&
            index_z >= 0 && index_z <= m_columns)
        {
            return gridmesh[index_x, index_z];
        }

        return null;
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
        Debug.Log("run");
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
                grid.GetComponent<Grid>().position.x = x;
                grid.GetComponent<Grid>().position.y = z;
				grid.GetComponent<Grid> ().isAvailable = !grid.GetComponent<Grid> ().CollidedWithTerrain ();
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
