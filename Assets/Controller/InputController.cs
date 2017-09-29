using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    // Use this for initialization
    void Start() {
		
    }
	
    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null) {
                Debug.Log("Clicked on: " + hit.collider.name);
                GameController.Instance.ClickedOnTile(TileController.Instance.GetCoordsOfTile(hit.collider.gameObject));
            } else {
                Debug.Log("Click");
            }
        }
    }
}
