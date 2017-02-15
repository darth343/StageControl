using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Material[] materials = new Material[3];
    public bool isAvailable = true;
    public Vector2 position;

    public Vector3 GetWorldPosition()
    {
        return new Vector3(position.x * gameObject.transform.parent.GetComponent<GridArray>().GridSizeX + gameObject.transform.parent.GetComponent<GridArray>().GridSizeX * 0.5f, 0, position.y * gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ + gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ * 0.5f);
    }

    public bool CollidedWithTerrain()
    {
        Terrain ground = SharedData.instance.ground;
        Vector3 minPos = GetWorldPosition() - (new Vector3(gameObject.transform.parent.GetComponent<GridArray>().GridSizeX * 0.5f, 0, gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ * 0.5f));
        Vector3 maxPos = GetWorldPosition() + (new Vector3(gameObject.transform.parent.GetComponent<GridArray>().GridSizeX * 0.5f, 0, gameObject.transform.parent.GetComponent<GridArray>().GridSizeZ * 0.5f));

        if (0.05 < ground.SampleHeight(minPos) &&  0.05 < ground.SampleHeight(maxPos))
        {
            isAvailable = true;
            return true;
        }
        isAvailable = false;
        return false;
    }

    void OnValidate()
    {
        //UpdateAvailability();
    }

    public void UpdateAvailability()
    {
        isAvailable = !CollidedWithTerrain();
        if (isAvailable)
        {
            GetComponent<Renderer>().material = materials[0];
        }
        else
        {
            GetComponent<Renderer>().material = materials[1];
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
