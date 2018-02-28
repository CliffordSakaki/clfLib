﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace clfLib
{
    //
    //
    //
    public class MoveXZByStick : MoveByStick
    {
        protected override void inputSticks (float horizontalValue, float verticalValue)
        {
            if (horizontalValue > 0.0f) {
                charCtrl.move (CharCtrl.direction.xPlus);
            }
            if (horizontalValue < 0.0f) {
                charCtrl.move (CharCtrl.direction.xMinus);
            }
            if (verticalValue > 0.0f) {
                charCtrl.move (CharCtrl.direction.zPlus);
            }
            if (verticalValue < 0.0f) {
                charCtrl.move (CharCtrl.direction.zMinus);
            }
        }
    }

}
