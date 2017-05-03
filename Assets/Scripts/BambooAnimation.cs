using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BambooAnimation : MonoBehaviour {
    public int index = 0;
	// Use this for initialization
	void Start () {
        posInit();
        StartCoroutine(Burn(index));

       
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        float playerDir = other.gameObject.transform.localEulerAngles.y;
        float selfDir = transform.localEulerAngles.y;

  

        if ((selfDir - playerDir) > 0 && (selfDir - playerDir) < 180)
        {
          
               GetComponent<Animator>().SetTrigger("left");
            print("left");
        }
        else
        {
           
                GetComponent<Animator>().SetTrigger("right");
            print("right");

        }
        //foreach (GameObject bam in bamboos)
        //{
        //    bam.GetComponent<Animator>().SetInteger("right", 0);
        //}
    }

    public void Death()
    {
        Tweener tween = transform.DOMoveY(-3, 0.1f);
        tween.SetEase(Ease.Linear);
        
    }
    


    void posInit()
    {
        Vector3 pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, pos.y - 3, pos.z);
    }


    IEnumerator  Burn(int index)
    {
        yield return new WaitForSeconds(index * 0.01f);
      

        Tweener tween= transform.DOMoveY(0, 0.1f);
        tween.SetEase(Ease.Linear);
    }
}
