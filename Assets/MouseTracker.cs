using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseTracker : MonoBehaviour {

    [SerializeField]
    Camera mainCamera;
	// Use this for initialization
	void Start () { 
	}

    void Update () {
         RaycastHit2D hit;
 
         Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
         if(hit = Physics2D.Raycast(ray.origin, new Vector2(0,0)))
             Debug.Log (hit.collider.name);
     }
    

}
