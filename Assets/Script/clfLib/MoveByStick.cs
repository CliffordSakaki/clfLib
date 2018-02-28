using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace clfLib
{
    //
    //
    //
    public class MoveByStick:MonoBehaviour
    {
        protected CharCtrl charCtrl;
        // Use this for initialization
        void Start ()
        {
            charCtrl = GetComponent<CharCtrl> ();
        }

        // Update is called once per frame
        void Update ()
        {
            float horizontalValue = Input.GetAxis ("Horizontal");
            float verticalValue = Input.GetAxis ("Vertical");

            inputSticks (horizontalValue, verticalValue);

        }

        protected virtual void inputSticks (float horizontalValue, float verticalValue)
        {
            //  need override
        }

    }

}