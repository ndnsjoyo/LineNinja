using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamAnimOnCapsule : MonoBehaviour {
    public List<GameObject> bamboos;
	// Use this for initialization
    void Awake()
    {
        bamboos = new List<GameObject>();
    }
	void Start () {
		
	}
    //void OnCollisionEnter(Collision other)
    //{
    //    float playerDir = other.gameObject.transform.localEulerAngles.y;

    //    float selfDir = transform.localEulerAngles.y;



    //    if ((selfDir - playerDir) > 90 && (selfDir - playerDir) < 270)
    //    {
    //        foreach (GameObject bam in bamboos)
    //        {
    //            bam.GetComponent<Animator>().SetTrigger("right");
    //        }
    //        print("右");
    //    }
    //    else
    //    {
    //        foreach (GameObject bam in bamboos)
    //        {
    //            bam.GetComponent<Animator>().SetTrigger("left");
    //        }
    //        print("左");
    //    }
    //    //foreach (GameObject bam in bamboos)
    //    //{
    //    //    bam.GetComponent<Animator>().SetInteger("right", 0);
    //    //}
    //}
    // Update is called once per frame
    void Update () {
		
	}
}
