using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCylinder : MonoBehaviour {
    // Use this for initialization
    public int level;
    public bool levelstart;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        Dude.OnRoomEnter += ChangeCamera;
    }


    void OnDisable()
    {
        Dude.OnRoomEnter -= ChangeCamera;
    }

    public void CameraCylinderClicked()
    {
        GameObject player = GameObject.FindWithTag("player");
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.GetComponent<Player>().currentCameraCylinder.SetActive(true);
        player.GetComponent<Player>().currentCameraCylinder = gameObject;
        gameObject.SetActive(false);
    }

    private void ChangeCamera(int newlevel)
    {
        if (levelstart && level == newlevel)
        {
            GameObject player = GameObject.FindWithTag("player");
            GameObject currcamera = player.GetComponent<Player>().currentCameraCylinder;
            if (currcamera.GetComponent<CameraCylinder>().level != newlevel)
            {
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                player.GetComponent<Player>().currentCameraCylinder.SetActive(true);
                player.GetComponent<Player>().currentCameraCylinder = gameObject;
                gameObject.SetActive(false);
            }
        }
    }
}
