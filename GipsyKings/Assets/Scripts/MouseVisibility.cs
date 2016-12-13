using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVisibility : MonoBehaviour {

    public bool visible = false;

	void Start () {
        Cursor.visible = visible;
    }
	
}
