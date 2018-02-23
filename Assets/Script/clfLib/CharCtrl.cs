using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace clfLib
{
    //
    //  内部移動単位はステップとする
    //
    //  Step per Unit ステップ移動すると1ユニット移動
    //
    //  1フレームで1ステップ移動して1秒（60フレーム）で1ユニット移動するならStepPerUnit=60
    //
    //  StepPerUnitは数字が小さいほど移動速度が速くなる
    //
    //  移動は4方向を仮装化する
    //  up/down/left/right
    //
    //  それぞれxyzの移動ステップ数を設定してmoveで内部座標に加算する
    //
    public class CharCtrl : MonoBehaviour
    {

        // Use this for initialization
        void Start ()
        {
            setMoveValue (clfLib.CharCtrl.direction.xPlus, clfLib.CharCtrl.moveValueXPlus);
            setMoveValue (clfLib.CharCtrl.direction.xMinus, clfLib.CharCtrl.moveValueXMinus);
            setMoveValue (clfLib.CharCtrl.direction.yPlus, clfLib.CharCtrl.moveValueYPlus);
            setMoveValue (clfLib.CharCtrl.direction.yMinus, clfLib.CharCtrl.moveValueYMinus);
            setMoveValue (clfLib.CharCtrl.direction.zPlus, clfLib.CharCtrl.moveValueZPlus);
            setMoveValue (clfLib.CharCtrl.direction.zMinus, clfLib.CharCtrl.moveValueZMinus);
		
        }
	
        // Update is called once per frame
        void Update ()
        {
		
        }

        //
        //
        //
        private Vector3 _origin;
        private VectorI3 _step;
        private VectorI3 _stepPerUnit = VectorI3.create (60, 60, 60);

        //
        //  move
        //

        //  方向
        public enum direction
        {
            xPlus,
            xMinus,
            yPlus,
            yMinus,
            zPlus,
            zMinus,

            max
        }

        //  方向毎の移動量
        private clfLib.VectorI3[] _moveValue = new clfLib.VectorI3[(int)direction.max];

        public readonly static clfLib.VectorI3 moveValueYPlus = clfLib.VectorI3.create (0, 1, 0);
        public readonly static clfLib.VectorI3 moveValueYMinus = clfLib.VectorI3.create (0, -1, 0);
        public readonly static clfLib.VectorI3 moveValueXPlus = clfLib.VectorI3.create (1, 0, 0);
        public readonly static clfLib.VectorI3 moveValueXMinus = clfLib.VectorI3.create (-1, 0, 0);
        public readonly static clfLib.VectorI3 moveValueZPlus = clfLib.VectorI3.create (0, 0, 1);
        public readonly static clfLib.VectorI3 moveValueZMinus = clfLib.VectorI3.create (0, 0, -1);

        public void setMoveValue (direction direct, VectorI3 veci)
        {
            _moveValue [(int)direct] = veci;
        }

        public void move (direction dirct)
        {
            moveByStep (_moveValue [(int)dirct]);
        }

        //
        //  origin
        //      オブジェクトの空間上の原点
        //
        public    Vector3 origin{ get { return _origin; } set { _origin = value; } }

        //  現在位置をオリジンとする
        public void setOriginNow ()
        {
            origin = gameObject.transform.position;
        }

        //
        //  step per unit
        //
        float xUnitPerStep{ get { return 1.0f / _stepPerUnit.x; } }

        float yUnitPerStep{ get { return 1.0f / _stepPerUnit.y; } }

        float zUnitPerStep{ get { return 1.0f / _stepPerUnit.z; } }

        public void setStepPerUnit (VectorI3 vec)
        {
            setStepPerUnit (vec.x, vec.y, vec.z);
        }

        public void setStepPerUnit (int x, int y, int z)
        {
            _stepPerUnit.x = x;
            _stepPerUnit.y = y;
            _stepPerUnit.z = z;
        }

        //
        //  move by step
        //
        public void moveByStep (VectorI3 vec)
        {
            moveByStep (vec.x, vec.y, vec.z);
        }

        public void moveByStep (int x, int y, int z)
        {
            //  内部座標計算
            _step.x += x;
            _step.y += y;
            _step.z += z;

            //  実座標計算
            Vector3 pos = _origin;

            pos.x += xUnitPerStep * _step.x;
            pos.y += yUnitPerStep * _step.y;
            pos.z += zUnitPerStep * _step.z;

            gameObject.transform.position = pos;

            Debug.Log ("pos.x = " + pos.x + "pos.y = " + pos.y + "pos.z = " + pos.z);
        }
    }
}