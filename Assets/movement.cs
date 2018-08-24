using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public float speed = 1f;
    GameObject rot;
    public bool targeted = true;
    public static Navigator nav;
    public GameObject template;
    public Material closestMaterial = null;

    // Use this for initialization
    void Start () {
		rot = GameObject.FindGameObjectWithTag("Rotating");
        nav = new Navigator();
        

        nav.Setup(closestMaterial, template);
        
    }
	
	// Update is called once per frame
	void Update () {
        
        //Code based off anchor coords
        
        GameObject rotating = rot;
        Vector3 position = rotating.transform.position;
        if (position.z > -13 && position.x > 9.69)
        {
            if (position.z < 10.85)
            {
                rotating.transform.position += new Vector3(0, 0, speed);
            }
            else
            {
                rotating.transform.position -= new Vector3(speed, 0, 0);
            }
        }
        else if (position.z > -13)
        {
            if (position.x < -7.62)
            {
                rotating.transform.position -= new Vector3(0, 0, speed);
            }
            else
            {
                rotating.transform.position -= new Vector3(speed, 0, 0);
            }
        }
        else
        {
            if (position.x < 9.69)
            {
                rotating.transform.position += new Vector3(speed, 0, 0);
            }
            else
            {
                rotating.transform.position += new Vector3(0, 0, speed);
            }
        }

        if (targeted)
        {
            nav.Target();
        }
    }
}
