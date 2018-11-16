using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCylinder : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CameraCylinderClicked()
    {
        GameObject player = GameObject.FindWithTag("player");
        player.transform.position = transform.position;
        player.GetComponent<Player>().currentCameraCylinder.SetActive(true);
        player.GetComponent<Player>().currentCameraCylinder = gameObject;
        gameObject.SetActive(false);

    }
}
