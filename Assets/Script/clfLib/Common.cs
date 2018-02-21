using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;


//	memo
//		bit field	->	bit array
//	todo
//		drag and drop
//


namespace clfLib
{
    [System.Serializable]
    public struct WayPoint
    {
        public GameObject position;
        public float timeCountSec;
    }

    public class scrCtrlCutinAnimation : MonoBehaviour
    {
        public WayPoint[] wayPoints;
        public GameObject cutinObject;
        // Use this for initialization

        private int nextWayPointNo;
        private int frameCnt;

        void Start ()
        {
            cutinObject.transform.position = wayPoints [0].position.transform.position;
            nextWayPointNo = 0;
            frameCnt = 0;
        }

        // Update is called once per frame
        void Update ()
        {
            if (frameCnt == 0) {
                Debug.Log ("!!");
                nextWayPointNo++;
                if (nextWayPointNo < wayPoints.Length) {
                    frameCnt = (int)(wayPoints [nextWayPointNo].timeCountSec * Common.FramePerSecond);
                } else {
                    frameCnt = -1;
                }
            }

            if (nextWayPointNo < wayPoints.Length) {
                Debug.Log (".." + nextWayPointNo + ", " + frameCnt);
                cutinObject.transform.position = cutinObject.transform.position + (wayPoints [nextWayPointNo].position.transform.position - cutinObject.transform.position) / frameCnt;
                frameCnt--;
            }
        }
    }

    //
    //
    //
    public struct VectorI3
    {
        public int x;
        public int y;
        public int z;

        //
        //
        //
        public static VectorI3 create (int x, int y, int z)
        {
            VectorI3 vec;
            vec.x = x;
            vec.y = y;
            vec.z = z;
            return vec;
        }

        //
        //
        //
        public static implicit operator VectorI3 (Vector3 vec)
        {
            VectorI3 veci;
            veci.x = (int)vec.x;
            veci.y = (int)vec.y;
            veci.z = (int)vec.z;
            return veci;
        }

        //
        //
        //
        public static implicit operator Vector3 (VectorI3 vec)
        {
            Vector3 vecf;
            vecf.x = (float)vec.x;
            vecf.y = (float)vec.y;
            vecf.z = (float)vec.z;
            return vecf;
        }

    }

    //
    //
    //
    public  static class MathI
    {
        //
        //  大きい方の値を返す
        //
        public static    int max (int val1, int val2)
        {
            return (val1 > val2) ? val1 : val2;
        }

        //
        //
        //
        public static int max (int[] vals)
        {
            int val = vals [0];

            for (int i = 1; i < vals.Length; i++) {
                val = max (val, vals [i]);
            }
            return val;
        }

        //
        //  小さい方の値を返す
        //
        public static    int min (int val1, int val2)
        {
            return (val1 > val2) ? val2 : val1;
        }

        //
        //
        //
        public static int min (int[] vals)
        {
            int val = vals [0];

            for (int i = 1; i < vals.Length; i++) {
                val = min (val, vals [i]);
            }
            return val;
        }
    }

    public class HttpUtil:MonoBehaviour
    {


        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public delegate void HttpGetResult (byte[] data);


        public static string getStringByHTTPOfPostAtURL (string url)
        {
            return "";
        }

        public static string getStringByHTTPOfGetAtURL (string url)
        {
            //WWW www = new WWW (url);
            //UnityWebRequest web=new UnityWebRequest(url,"GET",);;

            return "";
        }

    }

    //
    //
    //
    public class Common : MonoBehaviour
    {
        //  インスタンス化禁止
        private Common ()
        {
        }

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
        public static GameObject createGameObjectByPrefav (GameObject prefav)
        {
            return Instantiate (prefav) as GameObject;
        }

        //
        //
        //
        public static string readTextFileFromResource (string resourceName)
        {
            TextAsset textAsset = Resources.Load (resourceName) as TextAsset;
            return textAsset.text;
        }

        //
        //
        //
        public static List< string []> parseCSVtoStringArrayList (string csvString)
        {
            StringReader reader = new StringReader (csvString);
            List <string[]> parsedCSV = new List<string[]> ();
            while (reader.Peek () > -1) {
                string line = reader.ReadLine ();
                string[] csvLine = line.Split (',');
                parsedCSV.Add (csvLine);
            }
            return parsedCSV;
        }

        //
        //
        //
        public static void destroyGameObject (GameObject obj)
        {
            Destroy (obj);
        }

        //
        //
        //
        public static void LoadImageToSprite (Sprite sprite, string imagePath)
        {
            sprite = Resources.Load<Sprite> (imagePath);

        }

        //
        //
        //
        public static float FramePerSecond{ get { return 60.0f; } }
    }
}