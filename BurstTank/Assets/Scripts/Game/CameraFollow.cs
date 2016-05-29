using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject Hero;
    public float delaytime = 0;
    float time =0;
    bool lookMapOK = false;
	// Use this for initialization
	void Start () {
        Hero = GameObject.Find("Hero");
	}
	
	// Update is called once per frame
	void Update () {
        if (!lookMapOK)
        {
            time += Time.deltaTime;
            if (time >= delaytime)
            {
                lookMapOK = true;
                gameObject.GetComponent<Camera>().orthographicSize = 1.5f;
            }
        }
        else
        {
            transform.position = Hero.transform.position;
        }
	}
}
