using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//三维向量转化
public static class Vector3Extension
{
    public static Vector2 Round(this Vector3 v)
    {
        int x = Mathf.RoundToInt(v.x);
        int y = Mathf.RoundToInt(v.y);
        
        return new Vector2(x, y);
    }
}
