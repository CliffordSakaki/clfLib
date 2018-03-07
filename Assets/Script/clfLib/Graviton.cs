using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graviton : MonoBehaviour
{
    private static float _worldScale2 = 1.0f;

    public static float worldScale{ get { return Mathf.Sqrt (_worldScale2); } set { _worldScale2 = value * value; } }

    private static List <Graviton> _gravitonList = new List<Graviton> ();
    private Rigidbody _rig;
    public const float G = (float)6.674e-11;

    public Vector3 getGravityForce (Graviton target)
    {
        if (_rig) {
            Rigidbody tgtrig = target._rig;
            float r2 = Vector3.SqrMagnitude (gameObject.transform.position - target.gameObject.transform.position); //Vector3.Distance (gameObject.transform.position, target.gameObject.transform.position);
            //Debug.Log ("my pos (" + gameObject.transform.position.x + ", " + gameObject.transform.position.y + ", " + gameObject.transform.position.z + ")");
            //Debug.Log ("tgt pos (" + target.gameObject.transform.position.x + ", " + target.gameObject.transform.position.y + ", " + target.gameObject.transform.position.z + ")");

            //Debug.Log ("r = (" + r + ")");
            Vector3 vec = target.transform.position - transform.position;
            //Debug.Log ("vec(" + vec.x + ", " + vec.y + ", " + vec.z + ")");
            return vec.normalized * G * _rig.mass * tgtrig.mass / r2;//(r * r);
        } else {
            return Vector3.zero;
        }
    }

    // Use this for initialization
    void Start ()
    {
        _rig = GetComponent<Rigidbody> ();
        if (_rig) {
            _gravitonList.Add (this);
            _rig.useGravity = false;
        }
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (_rig) {
            foreach (Graviton tgtGraviton in _gravitonList) {
                if (tgtGraviton != this) {
                    _rig.AddForce (getGravityForce (tgtGraviton));
                    //_rig.AddForce
                }
            }
        }
    }
}
