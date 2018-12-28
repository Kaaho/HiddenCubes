using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHider : MonoBehaviour {

    public int level;
    public GameObject doortobeclosed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        Dude.OnLevelUp += ShowDoor;
        Dude.OnRoomEnter += HideDoor;
    }


    void OnDisable()
    {
        Dude.OnLevelUp -= ShowDoor;
        Dude.OnRoomEnter -= HideDoor;
    }

    private void ShowDoor(int newlevel)
    {
        if (level == newlevel)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void HideDoor(int newlevel)
    {
        if (level == newlevel)
        {
            Debug.Log("HideDoor");
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }

}
