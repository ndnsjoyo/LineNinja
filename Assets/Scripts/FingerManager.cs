using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerManager : MonoBehaviour {

    public static FingerManager instance;

    private FingerUpDetector up;
    public FingerUpDetector UpEvent
    {
        get
        {
            return up;
        }
        set
        {
            up = value;
        }

    }
    private FingerMotionDetector motion;
    public FingerMotionDetector motionEvent
    {
        get
        {
            return motion;
        }
        set
        {
            motion = value;
        }

    }
    private FingerDownDetector down;
    public FingerDownDetector downEvent
    {
        get
        {
            return down;
        }
        set
        {
            down = value;
        }

    }



    // Use this for initialization
    void Awake () {
        instance = this;
        up = GetComponent<FingerUpDetector>();
        motion = GetComponent<FingerMotionDetector>();
        down = GetComponent<FingerDownDetector>();
	}
	
	// Update is called once per frame
	public void setDraw(bool canDraw)
    {
        if (canDraw)
        {
            GetComponent<TouchMesh>().enabled = true;
            return;
        }

        GetComponent<TouchMesh>().enabled = false;
    }
    public void setTouch(bool cantouch)

    {
        if (cantouch)
        {
            GetComponent<FingerGestures>().enabled = true;
            return;
        }

        GetComponent<FingerGestures>().enabled = false;
    }

}
