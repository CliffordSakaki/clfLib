using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent (typeof(GameObject))]
public class Dragger : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    // Use this for initialization
    void Start ()
    {
		
    }
	
    // Update is called once per frame
    void Update ()
    {
		
    }

    public virtual void OnBeginDrag (PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Debug.Log ("OnBeginDrag:(" + pos.x + ", " + pos.y + ")");
    }

    public virtual void OnDrag (PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Debug.Log ("OnBeginDrag:(" + pos.x + ", " + pos.y + ")");
    }

    public virtual void OnEndDrag (PointerEventData eventData)
    {
        Vector2 pos = eventData.position;
        Debug.Log ("OnBeginDrag:(" + pos.x + ", " + pos.y + ")");
    }
}
