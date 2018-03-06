using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graviton : MonoBehaviour
{
    private static List <Graviton> _gravityObjects = new List<Graviton> ();
    private Rigidbody _rig;
    public const float G = (float)6.674e-11;

    public Vector3 getGravityForce (GameObject target)
    {
        Rigidbody tgtrig = target.GetComponent<Rigidbody> ();
        if (!_rig) {
            float r = Vector3.Distance (gameObject.transform.position, target.gameObject.transform.position);
            Vector3 vec = target.transform.position - transform.position;
            return vec.normalized * G * _rig.mass * tgtrig.mass / (r * r);
        } else {
            return Vector3.zero;
        }
    }

    // Use this for initialization
    void Start ()
    {
        _gravityObjects.Add (this);
        _rig = GetComponent<Rigidbody> ();
    }
	
    // Update is called once per frame
    void Update ()
    {
		
    }
}
