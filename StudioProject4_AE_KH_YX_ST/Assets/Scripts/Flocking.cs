using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flocking : MonoBehaviour
{
    float CohesionRadius = 50;
    float SeperationRadius = 20;
    float NeighborRadius = 50;
    public List<GameObject> neighbours = new List<GameObject>();
    public bool isLeader = false;
    public float speed = 1f;

    Vector3 value = new Vector3();

    List<GameObject> UpdateNeighbours()
    {
        neighbours.Clear();
        // iterate through entitylist
        for (int i = 0; i < SceneData.sceneData.EntityList.transform.childCount; ++i)
        {
            if (gameObject == SceneData.sceneData.EntityList.transform.GetChild(i).gameObject)
                continue;

            if ((SceneData.sceneData.EntityList.transform.GetChild(i).position - gameObject.transform.position).sqrMagnitude < NeighborRadius * NeighborRadius)
            {
                neighbours.Add(SceneData.sceneData.EntityList.transform.GetChild(i).gameObject);
            }
        }

        return neighbours;
    }

    public Vector3 computeAlignment()
    {
        value.Set(0, 0, 0);
        foreach(GameObject neighbour in neighbours)
        {
            value.x += neighbour.GetComponent<VMovement>().Velocity.x;
            value.y += neighbour.GetComponent<VMovement>().Velocity.y;
            value.z += neighbour.GetComponent<VMovement>().Velocity.z;
        }

        value.x /= neighbours.Count;
        value.y /= neighbours.Count;
        value.z /= neighbours.Count;

        return value.normalized;
    }
    public Vector3 computeSeperation()
    {
        value.Set(0, 0, 0);
        foreach (GameObject neighbour in neighbours)
        {
            value.x += neighbour.transform.position.x - transform.position.x;
            value.y += neighbour.transform.position.y - transform.position.y;
            value.z += neighbour.transform.position.z - transform.position.z;
        }

        value.x /= neighbours.Count;
        value.y /= neighbours.Count;
        value.z /= neighbours.Count;
        value.x *= -1;
        value.y *= -1;
        value.z *= -1;

        return value.normalized;
    }
    public Vector3 computeCohesion()
    {
        value.Set(0, 0, 0);
        foreach (GameObject neighbour in neighbours)
        {
            value.x += neighbour.transform.position.x;
            value.y += neighbour.transform.position.y;
            value.z += neighbour.transform.position.z;
        }

        value.x /= neighbours.Count;
        value.y /= neighbours.Count;
        value.z /= neighbours.Count;

        value = new Vector3(value.x - transform.position.x, value.y - transform.position.y, value.z - transform.position.z);

        return value.normalized;
    }

    void Update()
    {
        UpdateNeighbours();

        bool leaderNearby = false;
        int leaderindex = 0;
        for (int i = 0; i < neighbours.Count; ++i)
        {
            if (neighbours[i].GetComponent<Flocking>().isLeader == true)
            {
                leaderNearby = true;
                leaderindex = i;
                break;
            }
            if(isLeader)
            Debug.DrawLine(transform.position, neighbours[i].transform.position, Color.yellow);
        }

        if (leaderNearby == false)
        {
            isLeader = true;
        }
        else if(leaderNearby && isLeader)
        {
            //isLeader = true;
            neighbours[leaderindex].GetComponent<Flocking>().isLeader = false;
        }

        if (isLeader == false)
        {
            Vector3 alignment = GetComponent<Flocking>().computeAlignment();
            Vector3 seperation = GetComponent<Flocking>().computeSeperation();
            //Vector3 seperation = new Vector3();// GetComponent<Flocking>().computeSeperation();
            Vector3 cohesion = GetComponent<Flocking>().computeCohesion();
            //Vector3 cohesion = new Vector3();// GetComponent<Flocking>().computeCohesion();

            GetComponent<VMovement>().Velocity.x += alignment.x * SceneData.sceneData.AlignmentWeight + cohesion.x * SceneData.sceneData.CohesionWeight + seperation.x * SceneData.sceneData.SeperationWeight;
            GetComponent<VMovement>().Velocity.y += alignment.y * SceneData.sceneData.AlignmentWeight + cohesion.y * SceneData.sceneData.CohesionWeight + seperation.y * SceneData.sceneData.SeperationWeight;
            GetComponent<VMovement>().Velocity.z += alignment.z * SceneData.sceneData.AlignmentWeight + cohesion.z * SceneData.sceneData.CohesionWeight + seperation.z * SceneData.sceneData.SeperationWeight;
        }

        GetComponent<VMovement>().Velocity.Normalize();
        Debug.DrawLine(transform.position + new Vector3(0, 5, 0), transform.position + GetComponent<VMovement>().Velocity * 10 + new Vector3(0, 5, 0), Color.blue);
        transform.position += GetComponent<VMovement>().Velocity * speed;
    }
}
