using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace clfLib
{
    public class MoveByStick : MonoBehaviour
    {
        private CharCtrl charCtrl;
        // Use this for initialization
        void Start ()
        {
            charCtrl = GetComponent<CharCtrl> ();
        }
	
        // Update is called once per frame
        void Update ()
        {
            inputSticksAndButtons ();

        }


        protected virtual void inputSticksAndButtons ()
        {
            float horizontalValue = Input.GetAxis ("Horizontal");
            float verticalValue = Input.GetAxis ("Vertical");

            Debug.Log ("horizontalValue = " + horizontalValue);
            Debug.Log ("verticalValue = " + verticalValue);

            if (horizontalValue > 0.0f) {
                charCtrl.move (CharCtrl.direction.right);
            }
            if (horizontalValue < 0.0f) {
                charCtrl.move (CharCtrl.direction.left);
            }
            if (verticalValue > 0.0f) {
                charCtrl.move (CharCtrl.direction.up);
            }
            if (verticalValue < 0.0f) {
                charCtrl.move (CharCtrl.direction.down);
            }
        }
    }
}