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

    //    [System.Serializable]

    public Collider gravityRangeCollider;
    //{ get; set; }

    public Vector3 getGravityForce (Graviton tgtGraviton)
    {
        if (_rig) {
            Rigidbody tgtRig = tgtGraviton._rig;
            Vector3 tgtPos = tgtGraviton.gameObject.transform.position;

            float r2 = Vector3.SqrMagnitude (gameObject.transform.position - tgtPos); //Vector3.Distance (gameObject.transform.position, target.gameObject.transform.position);
            //Debug.Log ("my pos (" + gameObject.transform.position.x + ", " + gameObject.transform.position.y + ", " + gameObject.transform.position.z + ")");
            //Debug.Log ("tgt pos (" + target.gameObject.transform.position.x + ", " + target.gameObject.transform.position.y + ", " + target.gameObject.transform.position.z + ")");

            //Debug.Log ("r = (" + r + ")");
            Vector3 vec = tgtPos - transform.position;
            //Debug.Log ("vec(" + vec.x + ", " + vec.y + ", " + vec.z + ")");
            return vec.normalized * G * _rig.mass * tgtRig.mass / r2;//(r * r);
        } else {
            return Vector3.zero;
        }
    }

    // Use this for initialization
    void Start ()
    {
        _gravityProcessOnTrigger = null;
        _gravityProcessOnUpdate = null;

        _rig = GetComponent<Rigidbody> ();

        if (_rig) {
            if (gravityRangeCollider.GetComponent<BoxCollider> ()) {
                _gravityProcessOnTrigger = gravityProcessBox;
            }
            if (gravityRangeCollider.GetComponent<SphereCollider> ()) {
                _gravityProcessOnTrigger = gravityProcessSphere;
            }
            if (gravityRangeCollider.GetComponent<CapsuleCollider> ()) {
                _gravityProcessOnTrigger = gravityProcessCapsule;
            }

            if (_gravityProcessOnTrigger == null) {
                _gravitonList.Add (this);
                _rig.useGravity = false;
                _gravityProcessOnUpdate = gravityProcessOnupdate;
            }
        }

    }

    private gravityProcessOnUpdate _gravityProcessOnUpdate;

    private     delegate void gravityProcessOnUpdate ();

    void gravityProcessOnupdate ()
    {
        foreach (Graviton tgtGraviton in _gravitonList) {
            if (tgtGraviton != this && tgtGraviton.gravityRangeCollider == null) {
                _rig.AddForce (getGravityForce (tgtGraviton));
                //_rig.AddForce
            }
        }
    }

    private gravityProcessOnTrigger _gravityProcessOnTrigger;

    private     delegate void gravityProcessOnTrigger (Graviton tgtGraviton);

    //
    //
    //
    void gravityProcessSphere (Graviton tgtGraviton)
    {
        _rig.AddForce (getGravityForce (tgtGraviton));
    }

    //
    //
    //
    void gravityProcessBox (Graviton tgtGraviton)
    {
        _rig.AddForce (getGravityForce (tgtGraviton));
    }

    //
    //
    //
    void gravityProcessCapsule (Graviton tgtGraviton)
    {
        _rig.AddForce (getGravityForce (tgtGraviton));
    }

    // Update is called once per frame
    void Update ()
    {
        if (_gravityProcessOnUpdate != null) {
            _gravityProcessOnUpdate ();
        }
    }

    void OnTriggerStay (Collider tgtCollider)
    {
        if (_gravityProcessOnTrigger != null) {
            _gravityProcessOnTrigger (tgtCollider.gameObject.GetComponent<Graviton> ());
        }
    }
}
