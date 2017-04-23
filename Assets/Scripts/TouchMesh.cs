using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMesh : MonoBehaviour
{

    


    private List<Vector2> touchPosition = null;//触摸到的位置，世界坐标
    private List<Vector3> bonePosition = null; //转化到地面上的位置，世界坐标
    //private List<Vector3> verticles = null;    //生成mesh时的顶点 首先是世界坐标，之后会转化成本地坐标，目前用不到
    private List<Vector3> normals = null;      //每个胶囊体的方向
    private List<Vector3>  capsulePos = null;   //每个胶囊体的位置
    private List<float> lengths = null;
    private int index = 0;
    private Vector3 propPosition;
    //private List<int> triangles = null;



    public float Width = 0;
    public GameObject testPoint;
    public float length = 0;



    void Start()
    {
        touchPosition = new List<Vector2>();
        // verticles = new List<Vector3>();
        normals = new List<Vector3>();
        bonePosition = new List<Vector3>();
        propPosition = new Vector3();
        capsulePos = new List<Vector3>();
        lengths = new List<float>();
      //  triangles = new List<int>();
    }

    // Update is called once per frame


    void OnFingerDown(FingerDownEvent e)
    {
        touchPosition.Add((Vector2)e.Position);
        length = 0;
    }

    void OnFingerMove(FingerMotionEvent e)
    {


        if (((Vector2)e.Position - touchPosition[index]).sqrMagnitude > 300)
        {
            
            touchPosition.Add((Vector2)e.Position);
            index++;

        }
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
        foreach (Vector2 point in touchPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(point);
            //发射射线
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "Plane")
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
            normals.Add(bonePos - lastPos);
            capsulePos.Add((bonePos + lastPos) / 2);
            lengths.Add(mlenght+Width*2);
            length += mlenght;
            lastPos = bonePos;
        }
    }
    void build()
    {
        GameObject obj = new GameObject("bone");
        for(int i=0;i<capsulePos.Count;i++)
        {

            GameObject point=MyInstiate(capsulePos[i], normals[i], lengths[i]);
            point.transform.parent = obj.transform;
        }
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
        touchPosition.Clear();
        bonePosition.Clear();
    // verticles.Clear();
        normals.Clear();
        capsulePos.Clear();
        lengths.Clear();
       // triangles.Clear();
        index = 0;
       
    }


    GameObject MyInstiate(Vector3 pos,Vector3 dir,float height)
    {
        GameObject obj = (GameObject)Instantiate(testPoint, pos, new Quaternion(0, 0, 0, 0));
        obj.transform.up = dir;
        obj.GetComponent<CapsuleCollider>().height = height;
        obj.GetComponent<CapsuleCollider>().radius = Width;
        return obj;
    }
}
