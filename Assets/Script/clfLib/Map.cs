using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace clfLib
{
    public class Map : MonoBehaviour
    {
        // Use this for initialization
        void Start ()
        {
        }

        // Update is called once per frame
        void Update ()
        {
        }

        //
        //
        //
        [System.Serializable]
        public struct mapObject
        {
            public GameObject prefMapObject;
            public bool CanNotEnter;
        }

        public mapObject[] prefMapObjects;

        //
        //
        //
        private Vector3 _mapObjectGap;

        private Vector3 _MapAlign;
        private Vector3 _MapOffset;

        private struct mapCell
        {
            public GameObject mapObject;
            public int mapObjectId;
        }

        private mapCell[,,] _mapCells;
        private clfLib.VectorI3 _mapObjectCnt;

        //
        //
        //
        public void setMapObjectGapByUnit (float gapX, float gapY, float gapZ)
        {
            _mapObjectGap.x = gapX;
            _mapObjectGap.y = gapY;
            _mapObjectGap.z = gapZ;

            //  配置済みのマップオブジェクトの位置調整
            setMapOffset ();
            setAllMapObjectPosition ();
        }

        //
        //
        //
        public int xMapObjectCnt { get { return _mapObjectCnt.x; } }

        public int yMapObjectCnt { get { return _mapObjectCnt.y; } }

        public int zMapObjectCnt { get { return _mapObjectCnt.z; } }

        //
        //
        //
        public float xMapSize{ get { return xMapObjectCnt * _mapObjectGap.x; } }

        public float yMapSize{ get { return yMapObjectCnt * _mapObjectGap.y; } }

        public float zMapSize{ get { return zMapObjectCnt * _mapObjectGap.z; } }

        //
        //
        //
        public void setMapObjectCnt (clfLib.VectorI3 cnt)
        {
            setMapObjectCnt (cnt.x, cnt.y, cnt.z);
        }

        public void setMapObjectCnt (int xCnt, int yCnt, int zCnt)
        {
            mapCell[,,] newMapCells = new mapCell[xCnt, yCnt, zCnt];

            for (int x = 0; x < MathI.min (_mapObjectCnt.x, xCnt); x++) {
                for (int y = 0; y < MathI.min (_mapObjectCnt.y, yCnt); y++) {
                    for (int z = 0; z < MathI.min (_mapObjectCnt.z, zCnt); z++) {
                        newMapCells [x, y, z].mapObjectId = _mapCells [x, y, z].mapObjectId;
                        newMapCells [x, y, z].mapObject = _mapCells [x, y, z].mapObject;
                        _mapCells [x, y, z].mapObject = null;

                    }
                }
            }

            removeAllMapObject ();

            _mapCells = newMapCells;

            _mapObjectCnt.x = xCnt;
            _mapObjectCnt.y = yCnt;
            _mapObjectCnt.z = zCnt;

            setMapOffset ();
            setAllMapObjectPosition ();
        }

        //
        //
        //
        public void    setObjectPositionOnMapCell (int x, int y, int z)
        {
            if (checkMapObjectIdByMapCell (x, y, z) > 0) {
                GameObject mapObject = _mapCells [x, y, z].mapObject;
                setObjectPositionOnMapCell (mapObject, x, y, z);
            }
        }

        public void setObjectPositionOnMapCell (GameObject gameobj, int x, int y, int z)
        {
            Vector3 pos;
            pos.x = _mapObjectGap.x * x + _MapOffset.x;
            pos.y = _mapObjectGap.y * y + _MapOffset.y;
            pos.z = _mapObjectGap.z * z + _MapOffset.z;

            gameobj.transform.position = pos;
        }

        //
        //
        //
        public void setAllMapObjectPosition ()
        {
            for (int x = 0; x < _mapObjectCnt.x; x++) {
                for (int y = 0; y < _mapObjectCnt.y; y++) {
                    for (int z = 0; z < _mapObjectCnt.z; z++) {
                        setObjectPositionOnMapCell (x, y, z);
                    }
                }
            }
        }



        //
        //
        //
        public int checkMapObjectIdByMapCell (int x, int y, int z)
        {
            if (0 <= x && x < _mapObjectCnt.x && 0 <= y && y < _mapObjectCnt.y && 0 <= z && z < _mapObjectCnt.z) {
                //  セル画範囲内ならオブジェクトの存在チェック
                return _mapCells [x, y, z].mapObjectId;
            }
            return -1;
        }

        //
        //
        //
        public void setMapObjectByID (int mapObjectId, int x, int y, int z)
        {
            removeMapObject (x, y, z);
            _mapCells [x, y, z].mapObjectId = mapObjectId;
            if (prefMapObjects [mapObjectId].prefMapObject != null) {
                _mapCells [x, y, z].mapObject = Common.createGameObjectByPrefav (prefMapObjects [mapObjectId].prefMapObject);
                setObjectPositionOnMapCell (x, y, z);
            }
        }

        //
        //
        //
        public void removeMapObject (int x, int y, int z)
        {
            if (checkMapObjectIdByMapCell (x, y, z) > 0) {
                GameObject obj = _mapCells [x, y, z].mapObject;

                _mapCells [x, y, z].mapObjectId = -1;
                _mapCells [x, y, z].mapObject = null;

                Common.destroyGameObject (obj);
            }
        }

        //
        //
        //
        public void removeAllMapObject ()
        {
            for (int x = 0; x < xMapObjectCnt; x++) {
                for (int y = 0; y < yMapObjectCnt; y++) {
                    for (int z = 0; z < zMapObjectCnt; z++) {
                        removeMapObject (x, y, z);
                    }
                }
            }
        }
                   
        //
        //
        //
        public void setMapAlign (float x, float y, float z)
        {
            _MapAlign.x = x;
            _MapAlign.y = y;
            _MapAlign.z = z;

            setMapOffset ();
            setAllMapObjectPosition ();
        }

        //
        //
        //
        private void setMapOffset ()
        {
            _MapOffset.x = xMapSize * _MapAlign.x + _mapObjectGap.x / 2.0f;
            _MapOffset.y = yMapSize * _MapAlign.y + _mapObjectGap.y / 2.0f;
            _MapOffset.z = zMapSize * _MapAlign.z + _mapObjectGap.z / 2.0f;
        }
    }
}