using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlanet : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
        Rigidbody rig = GetComponent<Rigidbody> ();
        rig.velocity = (Quaternion.Euler (0.0f, Random.Range (0.0f, 360.0f), 0.0f) * Vector3.forward);
    }
	
    // Update is called once per frame
    void Update ()
    {
		
    }
}
