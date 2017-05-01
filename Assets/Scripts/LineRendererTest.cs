﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour {
    public static LineRendererTest instance;



    private LineRenderer lineRenderer;
    //定义一个Vector3,用来存储鼠标点击的位置  
    private Vector3 position;
    //用来索引端点  
    private int index = 0;
    //端点数  
    private int LengthOfLineRenderer = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //添加LineRenderer组件  
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        //设置材质  
        //lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
        //设置颜色  
       // lineRenderer.SetColors(Color.red, Color.yellow);
        //设置宽度  
        lineRenderer.SetWidth(0.5f, 0.5f);

    }

    void Update()
    {
        //获取LineRenderer组件  
       
        //鼠标左击  
     
        //连续绘制线段  
        while (index < LengthOfLineRenderer)
        {
            //两点确定一条直线，所以我们依次绘制点就可以形成线段了  
            lineRenderer.SetPosition(index, position);
            index++;
        }


    }

    public void DrawLine(Vector3 pos)
    {
        //将鼠标点击的屏幕坐标转换为世界坐标，然后存储到position中  
        position = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1.0f));
        //端点数+1  
        LengthOfLineRenderer++;
        //设置线段的端点数  
        lineRenderer.SetVertexCount(LengthOfLineRenderer);

    }
    public void CancelLine()
    {
        //将鼠标点击的屏幕坐标转换为世界坐标，然后存储到position中  
       // position = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1.0f));
        //端点数+1  

       // lineRenderer.SetPositions
        LengthOfLineRenderer--;
        //设置线段的端点数  
        lineRenderer.SetVertexCount(LengthOfLineRenderer);

    }


    public void Clear()
    {
        lineRenderer.SetVertexCount(0);
        LengthOfLineRenderer = 0;
        index = 0;
    }


}
