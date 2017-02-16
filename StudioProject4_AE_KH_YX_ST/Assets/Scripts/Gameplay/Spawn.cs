using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawn : MonoBehaviour {
    // How many seconds before next spawn
    public float m_secondsToSpawn;
    private Timer m_timer;
    // The controller that the spawned entity belongs to like EnemyController
    public GameObject m_controller;
    // The entity that the building spawns
    public GameObject m_entity;
    // Which direction away from building does user want to spawn units? left, right, up, down
    public string m_orientationX;
    public string m_orientationZ;
    // How many grids away from the building does user want to spawn units
    public int m_offsetGridX;
    public int m_offsetGridZ;
    // How many to spawn in a flock
    public int m_spawnAmt;

	void Start () {
        m_timer = this.gameObject.AddComponent<Timer>();
        m_timer.Init(0, m_secondsToSpawn, 0); 
	}

	void Update () {
        m_timer.Update();
        if (m_timer.can_run && m_spawnAmt > 0)
        {
            GameObject spawn;
            for (int i = 0; i < m_spawnAmt; ++i)
            {
                spawn = (GameObject)Instantiate(m_entity); // Create a copy of the original "hell"spawn
                spawn.transform.SetParent(m_controller.transform);
                spawn.GetComponent<Health>().MAX_HEALTH = 75;
                spawn.GetComponent<HealthBar>().m_childIndex = i; // i+1 if there's already one entity in the scene
                GameObject handle, handleChild;
                handle  = new GameObject();
                handleChild = new GameObject();
                Image img, imgChild;
                //handle = handleChild = (GameObject)Instantiate(m_entity);
                handle.AddComponent<Image>();
                img = handle.GetComponent<Image>();
                img.transform.SetParent(spawn.transform.parent.GetChild(0));
                img.rectTransform.sizeDelta = new Vector2(50, 10);
                img.rectTransform.pivot = new Vector2(0f, 0.5f);
                img.color = Color.red;
                handleChild.AddComponent<Image>();
                imgChild = handleChild.GetComponent<Image>();
                imgChild.transform.SetParent(img.transform);
                imgChild.rectTransform.sizeDelta = new Vector2(50, 10);
                imgChild.rectTransform.pivot = new Vector2(0f, 0.5f);
                imgChild.color = Color.green;
                //spawn.AddComponent<HealthBar>(); // Give it a healthbar
                Vector2 this_grid = SharedData.instance.gridmesh.GetGridIndexAtPosition(transform.position);
                Debug.Log(transform.position);
                int orientationX;
                int orientationZ;
                switch (m_orientationX)
                {
                    case "right":
                        orientationX = -1;
                        break;
                    default:
                        orientationX = 1;
                        break;
                }
                if (m_orientationZ == "up")
                    orientationZ = -1;
                else
                    orientationZ = 1;
                Vector3 spawn_pos = SharedData.instance.gridmesh.GetPositionAtGrid((int)this_grid.x + m_offsetGridX * orientationX, (int)this_grid.y + m_offsetGridZ * orientationZ); // is actually the grid this object is on's z position + 30, not y
                spawn_pos.y = SharedData.instance.gridmesh.GetTerrainHeightAtGrid(spawn_pos) + 15;
                spawn.transform.position = spawn_pos;
            }
            m_timer.Reset();
            m_spawnAmt = 0;
        }
	}
}
