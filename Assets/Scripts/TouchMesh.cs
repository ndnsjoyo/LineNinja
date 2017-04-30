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
        private Transform _UIobj;
        public Transform UIobj
        {
            get
            {
                return _UIobj;
            }
            set
            {
                _UIobj = value;
            }
        }
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
        public TouchMsg(Vector2 pos, Transform obj, float len)
        {

            touchPosition = pos;
            _UIobj = obj;
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
        Destroy(t[i].UIobj.gameObject);
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

    private FingerManager fingerManager;

 

    //public float undoRange = 10;
    //private LineRenderer line;
    //private List<int> triangles = null;

    

    public float Width = 0;
    public GameObject testPoint;
   // public float length = 0;
    public GameObject bamboo;
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



        touch.Add(new TouchMsg(e.Position, MyInstiateUIObj(e.Position), 0));
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
        if (index > 0)
        {
            Vector2 dirNow = e.Position - touch[index].TouPosition;
            Vector2 dirLast = touch[index].TouPosition - touch[index - 1].TouPosition;
            if (JudgeUndo(dirNow, dirLast)) return;
        }


        if (((Vector2)e.Position - touch[index].TouPosition).sqrMagnitude >300)
        {
            float touchlength = (e.Position - touch[index].TouPosition).magnitude;

            print(touchlength);
            //判断线的长度  以及能否继续划线
            if (!CanDraw(touchlength) )return;



            // 撤销操作
          

               

            touch.Add(new TouchMsg(e.Position, MyInstiateUIObj(e.Position), touchlength));
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
            
            return true;
        }
      return false;
    }

    
    void OnFingerUp(FingerUpEvent e)
    {
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
        for(int i=0;i<capsulePos.Count;i++)
        {
            print(lengths[i]);
            GameObject point=MyInstiateCollider(capsulePos[i], normals[i], lengths[i]);
            point.transform.parent = obj.transform;
        }
        obj.transform.position = new Vector3(0, 0.5f, 0);


        GameObject obj2 = new GameObject("bamboos");
        Vector3 pos=new Vector3();
        for(int i=0;i<capsulePos.Count;i++)
        {
           
           /// if((lengths[i] / (Width * 2)- (int)(lengths[i] / (Width * 2))>0.5f)


            int number = (int)(lengths[i] / (Width*2));
            //print(lengths[i] + "         " + number);
            for (int j=0;j<number;j++)
            {
                pos = bonePosition[i] + (j ) * normals[i] / (number);
                GameObject point=MyInstiateBamboo(pos, Width);
                point.transform.parent = obj2.transform;
            }
        }

        GameObject point2=MyInstiateBamboo(bonePosition[bonePosition.Count-1], Width);
        point2.transform.parent = obj2.transform;


    }
  


    //void NormalToVerticles()
    //{
    //    Vector3 temp;
       
       
    //    for (int i= 0;i< normals.Count;i++)
    //    {


    //       temp = (new Vector3(normals[i].z * -1, normals[i].y, normals[i].x)).normalized*Width/2;
    //        verticles.Add(temp + bonePosition[i]);
       
    //        //print(i);
    //        verticles.Add(bonePosition[i] - temp);
          
            
    //    }
      
    //}

    //void LocalVerticle()
    //{
    //    for(int i=0;i<verticles.Count;i++)
    //    {

    //        verticles[i] = verticles[i] - propPosition;
              
    //    }
    //}

    //void BuildTriangle()
    //{
    //    int count = (verticles.Count - 2) ;
    //    int[] _triangles = new int[count*3];
    //    for(int i=0,k=0;i<count;i++,k+=3)
    //    {
    //        _triangles[k] = i % 2 == 0 ? i : i - 1;
    //        _triangles[k + 1] = i + 2;
    //        _triangles[k + 2] = i % 2 == 0 ? i + 3 : i;
    //    }
    //    for(int i=0;i<count*3;i++)
    //    {
    //        triangles.Add(_triangles[i]);
    //    }
    //}

    //void DrawMesh()
    //{
    //    Mesh mesh = new Mesh();
    //    GameObject point = MyInstiate(propPosition);
    //    point.AddComponent<MeshFilter>();
    //    mesh = point.GetComponent<MeshFilter>().mesh;
    //    mesh.name = "zhangaiwu";




    //    //mesh.vertices =  new Vector3[]
    //    //{
    //    //    new Vector3(-0.5f, -0.5f, -0.5f),
    //    //    new Vector3(0.5f, -0.5f, -0.5f),
    //    //    new Vector3(0.5f, 0.5f, -0.5f),
    //    //    new Vector3(-0.5f,0.5f, -0.5f),
    //    //    new Vector3(-0.5f,0.5f, 0.5f),
    //    //    new Vector3(0.5f, 0.5f, 0.5f),
    //    //    new Vector3(0.5f, -0.5f, 0.5f),
    //    //    new Vector3(-0.5f, -0.5f, 0.5f)
    //    //};



    //    //for(int i=0;i<mesh.vertexCount;i++)
    //    //{
    //    //    print(mesh.vertices[i]);
    //    //}


    //    Vector3[] vect = new Vector3[verticles.Count];

    //    for (int i = 0; i < verticles.Count; i++)
    //    {
    //        vect[i] = verticles[i];
            
    //    }

    //    mesh.vertices = vect;


    //    int[] triang = new int[triangles.Count];

    //    for (int i = 0; i < verticles.Count; i++)
    //    {
    //        triang[i] = triangles[i];
    //    }

    //    mesh.triangles = triang;

    //}

    void ListClear()
    {
       
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


    GameObject MyInstiateBamboo(Vector3 pos,float scale)
    {
        GameObject obj=(GameObject)Instantiate(bamboo, pos, new Quaternion(0, 0, 0, 0));

        obj.transform.localScale = new Vector3(scale*1.5f, 1, scale*1.5f);
        //obj.transform.localScale = new Vector3( 0.01f, 1,0.01f);


        return obj;

    }
     
    Transform MyInstiateUIObj(Vector3 pos)
    {
        GameObject obj= (GameObject)Instantiate(UIObj, pos, new Quaternion(0, 0, 0, 0));

        obj.transform.parent = GameObject.Find("Line").transform;
        return obj.transform;

    }

   

}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TouchMesh : MonoBehaviour
//{




//    private List<Vector2> touchPosition = null;//触摸到的位置，世界坐标
//    private List<Vector3> bonePosition = null; //转化到地面上的位置，世界坐标
//    //private List<Vector3> verticles = null;    //生成mesh时的顶点 首先是世界坐标，之后会转化成本地坐标，目前用不到
//    private List<Vector3> normals = null;      //每个胶囊体的方向
//    private List<Vector3> capsulePos = null;   //每个胶囊体的位置
//    private List<float> lengths = null;
//    private int index = 0;
//    private Vector3 propPosition;
//    //private List<int> triangles = null;



//    public float Width = 0;
//    public GameObject testPoint;
//    public float length = 0;





//    private Vector2 _touchPostion;
//    private Vector3 _bonePosition;
//    Vector2 tTouchPostion;
//    Vector3 tbonePostion;
//    Vector3 dir;
//    float mlenght;
//    void Start()
//    {
//        _touchPostion = new Vector2();
//        _bonePosition = new Vector3();








//        touchPosition = new List<Vector2>();
//        // verticles = new List<Vector3>();
//        normals = new List<Vector3>();
//        bonePosition = new List<Vector3>();
//        propPosition = new Vector3();
//        capsulePos = new List<Vector3>();
//        lengths = new List<float>();
//        //  triangles = new List<int>();
//    }

//    // Update is called once per frame


//    void OnFingerDown(FingerDownEvent e)
//    {
//        _touchPostion = (Vector2)e.Position;
//        _bonePosition = rayPos(_touchPostion);
//        propPosition = _bonePosition;




//        //touchPosition.Add((Vector2)e.Position);
//        length = 0;

//    }

//    void OnFingerMove(FingerMotionEvent e)
//    {
//        tTouchPostion = _touchPostion;
//        tbonePostion = _bonePosition;
//        if (((Vector2)e.Position - tTouchPostion).sqrMagnitude > 300)
//        {

//            _touchPostion = (Vector2)e.Position;

//            _bonePosition = rayPos(_touchPostion);

//            dir = _bonePosition - tbonePostion;
//            normals.Add(dir);
//            mlenght = (dir).magnitude;
//            capsulePos.Add((_bonePosition + tbonePostion) / 2);
//            lengths.Add(mlenght + Width * 2);

//        }









//        //if (((Vector2)e.Position - touchPosition[index]).sqrMagnitude > 300)
//        //{

//        //    touchPosition.Add((Vector2)e.Position);
//        //    index++;

//        //}
//    }




//    void OnFingerUp(FingerUpEvent e)
//    {
//        CapsuleBuild();
//        ListClear();
//    }

//    void CapsuleBuild()
//    {

//        // touchToBonePosition();
//        //BuildNormalVec();
//        build();
//        //NormalToVerticles();
//        //LocalVerticle();


//        //BuildTriangle();
//        //DrawMesh();
//    }


//    void touchToBonePosition()
//    {
//        foreach (Vector2 point in touchPosition)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(point);
//            //发射射线
//            RaycastHit hitInfo = new RaycastHit();
//            if (Physics.Raycast(ray, out hitInfo))
//            {
//                if (hitInfo.collider.name == "Ground")
//                {


//                    bonePosition.Add(hitInfo.point);

//                }
//            }


//        }
//    }

//    void BuildNormalVec()
//    {

//        Vector3 lastPos = new Vector3();
//        float mlenght;
//        foreach (Vector3 bonePos in bonePosition)
//        {

//            if (bonePos == bonePosition[0])
//            {
//                propPosition = bonePos;
//                lastPos = bonePos;
//                continue;
//            }
//            mlenght = (bonePos - lastPos).magnitude;
//            print(mlenght);
//            normals.Add(bonePos - lastPos);
//            capsulePos.Add((bonePos + lastPos) / 2);
//            lengths.Add(mlenght + Width * 2);
//            length += mlenght;
//            lastPos = bonePos;
//        }
//    }
//    void build()
//    {
//        GameObject obj = new GameObject("bone");
//        for (int i = 0; i < capsulePos.Count; i++)
//        {

//            GameObject point = MyInstiate(capsulePos[i], normals[i], lengths[i]);
//            point.transform.parent = obj.transform;
//        }
//        obj.transform.position = new Vector3(0, 0.5f, 0);
//    }



//    Vector3 rayPos(Vector3 point)
//    {
//        Vector3 bonePos = new Vector3();
//        Ray ray = Camera.main.ScreenPointToRay(point);
//        //发射射线
//        RaycastHit hitInfo = new RaycastHit();
//        if (Physics.Raycast(ray, out hitInfo))
//        {
//            if (hitInfo.collider.name == "Ground")
//            {

//                bonePos = hitInfo.point;



//            }
//        }

//        return bonePos;
//    }

//    //void NormalToVerticles()
//    //{
//    //    Vector3 temp;


//    //    for (int i= 0;i< normals.Count;i++)
//    //    {


//    //       temp = (new Vector3(normals[i].z * -1, normals[i].y, normals[i].x)).normalized*Width/2;
//    //        verticles.Add(temp + bonePosition[i]);

//    //        //print(i);
//    //        verticles.Add(bonePosition[i] - temp);


//    //    }

//    //}

//    //void LocalVerticle()
//    //{
//    //    for(int i=0;i<verticles.Count;i++)
//    //    {

//    //        verticles[i] = verticles[i] - propPosition;

//    //    }
//    //}

//    //void BuildTriangle()
//    //{
//    //    int count = (verticles.Count - 2) ;
//    //    int[] _triangles = new int[count*3];
//    //    for(int i=0,k=0;i<count;i++,k+=3)
//    //    {
//    //        _triangles[k] = i % 2 == 0 ? i : i - 1;
//    //        _triangles[k + 1] = i + 2;
//    //        _triangles[k + 2] = i % 2 == 0 ? i + 3 : i;
//    //    }
//    //    for(int i=0;i<count*3;i++)
//    //    {
//    //        triangles.Add(_triangles[i]);
//    //    }
//    //}

//    //void DrawMesh()
//    //{
//    //    Mesh mesh = new Mesh();
//    //    GameObject point = MyInstiate(propPosition);
//    //    point.AddComponent<MeshFilter>();
//    //    mesh = point.GetComponent<MeshFilter>().mesh;
//    //    mesh.name = "zhangaiwu";




//    //    //mesh.vertices =  new Vector3[]
//    //    //{
//    //    //    new Vector3(-0.5f, -0.5f, -0.5f),
//    //    //    new Vector3(0.5f, -0.5f, -0.5f),
//    //    //    new Vector3(0.5f, 0.5f, -0.5f),
//    //    //    new Vector3(-0.5f,0.5f, -0.5f),
//    //    //    new Vector3(-0.5f,0.5f, 0.5f),
//    //    //    new Vector3(0.5f, 0.5f, 0.5f),
//    //    //    new Vector3(0.5f, -0.5f, 0.5f),
//    //    //    new Vector3(-0.5f, -0.5f, 0.5f)
//    //    //};



//    //    //for(int i=0;i<mesh.vertexCount;i++)
//    //    //{
//    //    //    print(mesh.vertices[i]);
//    //    //}


//    //    Vector3[] vect = new Vector3[verticles.Count];

//    //    for (int i = 0; i < verticles.Count; i++)
//    //    {
//    //        vect[i] = verticles[i];

//    //    }

//    //    mesh.vertices = vect;


//    //    int[] triang = new int[triangles.Count];

//    //    for (int i = 0; i < verticles.Count; i++)
//    //    {
//    //        triang[i] = triangles[i];
//    //    }

//    //    mesh.triangles = triang;

//    //}

//    void ListClear()
//    {


//        //touchPosition.Clear();
//        //bonePosition.Clear();
//        // verticles.Clear();




//        normals.Clear();
//        capsulePos.Clear();
//        lengths.Clear();
//        // triangles.Clear();
//        index = 0;

//    }


//    GameObject MyInstiate(Vector3 pos, Vector3 dir, float height)
//    {
//        GameObject obj = (GameObject)Instantiate(testPoint, pos, new Quaternion(0, 0, 0, 0));
//        obj.transform.up = dir;
//        obj.GetComponent<CapsuleCollider>().height = height;
//        obj.GetComponent<CapsuleCollider>().radius = Width;
//        return obj;
//    }
//}
