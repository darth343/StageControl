using UnityEngine;
using System.Collections;

public class VMovement : MonoBehaviour
{
    Vector3 Goal;
    public Vector3 Velocity;
    public float speed = 10f;
    
	// Use this for initialization
	void Start () 
    {
        Velocity.Set(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position += gameObject.GetComponent<VMovement>().Velocity.normalized * Time.deltaTime * speed;
	}
}
