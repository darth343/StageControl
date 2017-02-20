using UnityEngine;
using System.Collections;

/**/
// Controller class of all units
/**/
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthBar))]
public class Unit : MonoBehaviour {
    public GameObject model; // Entity model
    public int m_attkRadius; // Attack radius which is how many grids surrounding the unit it can detect/see enemies
    public float m_attkDist; // Minimum distance before unit attacks closest enemy unit
    private Vector2 m_oldGrid; // The grid the unit was standing on in the previous frame
    private ArrayList m_attkGridList; // The indexes of the grid which this unit can detect
	// Use this for initialization
	void Start () {
        m_attkGridList = new ArrayList();
        // if pos_grid != old_pos_grid then run
        Vector2 pos_grid = SharedData.instance.gridmesh.GetGridIndexAtPosition(transform.position);
        m_attkGridList.Add(pos_grid);
        Vector2 temp1 = pos_grid;
        for (int i = 1; i <= m_attkRadius; ++i)
        {
            temp1.x -= i;
            m_attkGridList.Add(temp1);
            temp1 = pos_grid;
            temp1.x += i;
            m_attkGridList.Add(temp1);
            temp1 = pos_grid;
            temp1.y -= i;
            m_attkGridList.Add(temp1);
            temp1 = pos_grid;
            temp1.y += i;
            m_attkGridList.Add(temp1);
            temp1 = pos_grid;
        }
        SharedData.instance.gridmesh.HighlightUnitPosition(m_attkGridList);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
