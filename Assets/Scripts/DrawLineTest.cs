using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineTest : MonoBehaviour {


    public static DrawLineTest instance;
    //绘制线段的材质  
    public Material material;//创建一个Material，拖拽到该参数上即可  




    public List<Vector3> lineInfo;
    // Use this for initialization 
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //初始化鼠标线段链表  
        lineInfo = new List<Vector3>();
    }

    // Update is called once per frame 

    //此绘制方法由系统调用  
    void OnPostRender()
    {
        if (!material)
        {
            Debug.Log("请给材质赋值");
            return;
        }
        print(123123);
    
        //设置该材质通道，0为默认值  
        material.SetPass(0);
        //设置绘制2D图像  
        GL.LoadOrtho();
        //表示开始绘制，绘制累心改为线段  
        GL.Begin(GL.LINES);
        
        //得到鼠标垫信息的总数量  
        int size = lineInfo.Count;
        //遍历鼠标点的链表  
        for (int i = 0; i < size - 1; i++)
        {
            Vector3 start = lineInfo[i];
            Vector3 end = lineInfo[i + 1];
            //绘制线段  
            DrawLine(start.x, start.y, end.x, end.y);
        }
        GL.End();
    }

    void DrawLine(float x1, float y1, float x2, float y2)
    {
        //绘制线段，需要将屏幕中某个点的像素坐标除以屏幕宽或高  
        GL.Vertex(new Vector3(x1 / Screen.width, y1 / Screen.height, 0));
        GL.Vertex(new Vector3(x2 / Screen.width, y2 / Screen.height, 0));
    }

}
