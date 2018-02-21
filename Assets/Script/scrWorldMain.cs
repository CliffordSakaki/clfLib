using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using clfLib;

public class scrWorldMain : MonoBehaviour
{
    private clfLib.Map _map;
    private GameObject _myChar;
    private clfLib.CharCtrl _myChar_CharCtrl;
    public GameObject prefMyChar;
    // Use this for initialization
    void Start ()
    {
        //
        //
        //
        _map = gameObject.GetComponent<clfLib.Map> ();
        _map.setMapAlign (-0.5f, -0.5f, -0.5f);
        _map.setMapObjectCnt (16, 2, 16);
        _map.setMapObjectGapByUnit (1.0f, 1.0f, 1.0f);
        for (int x = 0; x < _map.xMapObjectCnt; x++) {
            for (int z = 0; z < _map.zMapObjectCnt; z++) {
                for (int y = 0; y < _map.yMapObjectCnt; y++) {
                    int mapObjectID = 0;
                    switch (y) {
                    case 0:
                        {
                            mapObjectID = 1;
                        }
                        break;
                    case 1:
                        {
                            if (
                                x == 0 || (x % (_map.xMapObjectCnt - 1)) == 0 ||
                                z == 0 || (z % (_map.zMapObjectCnt - 1)) == 0) {
                                mapObjectID = 1;
                            } else if (x % 2 == 0 && z % 2 == 0) {
                                mapObjectID = 2;
                            }
                        }
                        break;
                    }
                    _map.setMapObjectByID (mapObjectID, x, y, z);
                }
            }
        }

        //
        //
        //
        _myChar = clfLib.Common.createGameObjectByPrefav (prefMyChar);
        _map.setObjectPositionOnMapCell (_myChar, 1, 1, 2);
        _myChar_CharCtrl = _myChar.GetComponent<clfLib.CharCtrl> ();
        _myChar_CharCtrl.setOriginNow ();
        _myChar_CharCtrl.setMoveValue (clfLib.CharCtrl.direction.up, clfLib.CharCtrl.moveValueUp);
        _myChar_CharCtrl.setMoveValue (clfLib.CharCtrl.direction.down, clfLib.CharCtrl.moveValueDown);
        _myChar_CharCtrl.setMoveValue (clfLib.CharCtrl.direction.right, clfLib.CharCtrl.moveValueRight);
        _myChar_CharCtrl.setMoveValue (clfLib.CharCtrl.direction.left, clfLib.CharCtrl.moveValueLeft);
    }
	
    // Update is called once per frame
    void Update ()
    {
    }
}
