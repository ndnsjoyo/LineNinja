using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMesh : MonoBehaviour
{

    private class TouchMsg
    {
        private Vector2 touchPosition;
        public Vector2 TouPosition
        {
            get
            {
                return touchPosition;
            }
            set
            {
                touchPosition = value;
            }
        }
        //private Transform _UIobj;
        //public Transform UIobj
        //{
        //    get
        //    {
        //        return _UIobj;
        //    }
        //    set
        //    {
        //        _UIobj = value;
        //    }
        //}
        private float mlength;
        public float Length
        {
            get
            {
                return mlength;
            }
            set
            {
                mlength = value;
            }
        }
        public TouchMsg(Vector2 pos,  float len)
        {

            touchPosition = pos;
         //   _UIobj = obj;
            mlength = len;
            //touchPosition =new Vector2();
            // UIobjs = new Transform();
        }
     
        public void Add()
        {

        }

    }


    private List<TouchMsg> touch = null;
    private void myRemove(int i, List<TouchMsg> t)
    {
        //Destroy(t[i].UIobj.gameObject);
        t[i] = null;
        t.RemoveAt(i);

    }

   


    private List<Vector3> bonePosition = null; //转化到地面上的位置，世界坐标



    //private List<Vector3> verticles = null;    //生成mesh时的顶点 首先是世界坐标，之后会转化成本地坐标，目前用不到
    private List<Vector3> normals = null;      //每个胶囊体的方向
    private List<Vector3>  capsulePos = null;   //每个胶囊体的位置
    private List<float> lengths = null;
  
 

    private int index = 0;
    private Vector3 propPosition;
    private GameObject lastBamboos=null;
    private GameObject lastCapsules = null;
    private FingerManager fingerManager;

 

    //public float undoRange = 10;
    //private LineRenderer line;
    //private List<int> triangles = null;

    

    public float Width = 0;
    public GameObject testPoint;
   // public float length = 0;
    public GameObject[] bamboos;
    public float AllLength;
    public GameObject UIObj;
    private float screenLength = 0;
    public float lastLength = 0;
    void Awake()
    {
        fingerManager = FingerManager.instance;
      
    }
    void Start()
    {
        touch = new List<TouchMsg>();

    
        // verticles = new List<Vector3>();
        normals = new List<Vector3>();
        bonePosition = new List<Vector3>();
        propPosition = new Vector3();
        capsulePos = new List<Vector3>();
        lengths = new List<float>();
   
      
        lastLength = AllLength;
       

        // line = GetComponent<LineRenderer>();
        //  triangles = new List<int>();
    }
    void OnEnable()
    {
       fingerManager.UpEvent.OnFingerUp += OnFingerUp;
        fingerManager.downEvent.OnFingerDown += OnFingerDown;
        fingerManager.motionEvent.OnFingerMove += OnFingerMove;
        //up
    }
    // Update is called once per frame
    void OnDisable()
    {
        fingerManager.UpEvent.OnFingerUp -= OnFingerUp;
        fingerManager.downEvent.OnFingerDown -= OnFingerDown;
        fingerManager.motionEvent.OnFingerMove -= OnFingerMove;
    }

    void OnFingerDown(FingerDownEvent e)
    {



        touch.Add(new TouchMsg(e.Position, 0));

        LineRendererTest.instance.SetAct(e.Position);
        // print(touch.Count);
        // myRemove(0,touch);
        // touch.RemoveAt(0);
        //touchPosition.Add((Vector2)e.Position);
        //UIobjs.Add(MyInstiateUIObj(e.Position));
        //// print(touchPosition.Count);
        //length = 0;



    }

  


    void OnFingerMove(FingerMotionEvent e)
    {
        LineRendererTest.instance.DrawLine(e.Position);


      //  DrawLineTest.instance.lineInfo.Add(e.Position);
        if (index > 0)
        {
            Vector2 dirNow = e.Position - touch[index].TouPosition;
            Vector2 dirLast = touch[index].TouPosition - touch[index - 1].TouPosition;
            if (JudgeUndo(dirNow, dirLast)) return;
        }


        if (((Vector2)e.Position - touch[index].TouPosition).sqrMagnitude >300)
        {

           

            float touchlength = (e.Position - touch[index].TouPosition).magnitude;

         //   print(touchlength);
            //判断线的长度  以及能否继续划线
            if (!CanDraw(touchlength) )return;



            // 撤销操作
          

               

            touch.Add(new TouchMsg(e.Position,  touchlength));
           // bonePosition.Add(rayPos(e.Position));
            index++;
          
        }
    }
    






    bool CanDraw(float f)
    {
        lastLength -= f/100;



        if (lastLength <= 0)
        {
            lastLength = 0;
            return false; }

        return true;
    }



    bool JudgeUndo(Vector2 now,Vector2 last)
    {
        float angle = Vector2.Angle(now, last);

       
        if(angle>130)
        {
            //  print(UIobjs.Count + "   " + index);
            lastLength += touch[index].Length / 100;


            myRemove(index, touch);
           
            index--;

         //   LineRendererTest.instance.CancelLine();


            return true;
        }
      return false;
    }

    
    void OnFingerUp(FingerUpEvent e)
    {

        if (lastBamboos != null)
            DestroylastBamboo();
        CapsuleBuild();
        ListClear();
    }

    void CapsuleBuild()
    {

        touchToBonePosition();
        BuildNormalVec();
        build();
        //NormalToVerticles();
        //LocalVerticle();


        //BuildTriangle();
        //DrawMesh();
    }


    void touchToBonePosition()
    {

      
        foreach (TouchMsg point in touch)
        {
            //bonePosition.Add(Camera.main.ScreenToWorldPoint(e.p))


            Ray ray = Camera.main.ScreenPointToRay(point.TouPosition);
            //发射射线
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == "Bounce")
                {


                    bonePosition.Add(hitInfo.point);
                    
                }
            }


        }
    }

    void BuildNormalVec()
    {
        
        Vector3 lastPos=new Vector3();
        float mlenght;
       foreach (Vector3 bonePos in bonePosition)
        {

            if (bonePos == bonePosition[0])
            {
                propPosition = bonePos;
                lastPos = bonePos;
                continue; }
            mlenght = (bonePos - lastPos).magnitude;
           // print(mlenght);
            normals.Add(bonePos - lastPos);
            capsulePos.Add((bonePos + lastPos) / 2);
            lengths.Add(mlenght+Width*2);
           // length += mlenght;
            lastPos = bonePos;
        }
    }
    void build()
    {
        if (capsulePos.Count < 1)
            return;
        GameObject obj = new GameObject("bone");





        int index = 0;
        GameObject obj2 = new GameObject("bamboos");
        Vector3 pos = new Vector3();




        for (int i=0;i<capsulePos.Count;i++)
        {
           // print(lengths[i]);
            GameObject point1=MyInstiateCollider(capsulePos[i], normals[i], lengths[i]);
            point1.transform.parent = obj.transform;





            int number = (int)(lengths[i] / (Width * 2));
            //print(lengths[i] + "         " + number);
            for (int j = 0; j < number; j++)
            {
                pos = bonePosition[i] + (j) * normals[i] / (number);
                GameObject point = MyInstiateBamboo(pos, Width, normals[i], index++);
                point.transform.parent = obj2.transform;
             //   point1.GetComponent<BamAnimOnCapsule>().bamboos.Add(point);
            }
            

            if(i==capsulePos.Count-1)
            {
                GameObject point2 = MyInstiateBamboo(bonePosition[bonePosition.Count - 1], Width, normals[normals.Count - 1], index);
              //  point1.GetComponent<BamAnimOnCapsule>().bamboos.Add(point2);
                point2.transform.parent = obj2.transform;
            }

        }
        obj.transform.position = new Vector3(0, 0.5f, 0);
        lastCapsules = obj;


      
        lastBamboos = obj2;




    }
  



    void ListClear()
    {
        LineRendererTest.instance.Clear();
        // DrawLineTest.instance.lineInfo.Clear();
        bonePosition.Clear();
        // verticles.Clear();
        normals.Clear();
        capsulePos.Clear();

        lengths.Clear();
      
        touch.Clear();

        foreach (Transform obj in GameObject.Find("Line").transform)
            Destroy(obj.gameObject);
      
      // triangles.Clear();
      index = 0;
       
    }


    Vector3 rayPos(Vector3 point)
    {
        Vector3 bonePos = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(point);
        //发射射线
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "Bounce")
            {

                bonePos = hitInfo.point;



            }
        }

        return bonePos;
    }

    GameObject MyInstiateCollider(Vector3 pos,Vector3 dir,float height)
    {
        GameObject obj = (GameObject)Instantiate(testPoint, pos, new Quaternion(0, 0, 0, 0));
        obj.transform.up = dir;
        obj.GetComponent<CapsuleCollider>().height = height;
        obj.GetComponent<CapsuleCollider>().radius = Width;
        return obj;
    }


    GameObject MyInstiateBamboo(Vector3 pos,float scale,Vector3 dir,int index)
    {

        int i = (int)Random.Range(0, 2);
        GameObject obj=(GameObject)Instantiate(bamboos[2], pos, new Quaternion(0, 0, 0, 0));

        obj.transform.localScale = new Vector3(1, 1, 1);
        //obj.transform.localScale = new Vector3( 0.01f, 1,0.01f);
        obj.transform.forward = dir;

        obj.GetComponent<BambooAnimation>().index = index;
       // obj.transform.eulerAngles = new Vector3(10,10,10);
        return obj;

    }
     
   
    void DestroylastBamboo()
    {
        foreach(Transform bamboo in lastBamboos.transform)
        {
            bamboo.GetComponent<BambooAnimation>().Death();
        }
        Destroy(lastCapsules);
        Destroy(lastBamboos,0.1f);
    }
   

}

