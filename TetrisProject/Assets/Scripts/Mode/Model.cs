using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public const int NORMAL_ROWS = 20;  //地图正常行数

    public const int MAX_ROWS = 23;     //行数(多加了三行)
    public const int MAX_COLUMNS = 10;  //列数

    private  int score = 0;              //当前分数       
    private int highScore = 0;           //最高分数
    private int numbersGame = 0;         //游戏局数

    //返回游戏数据
    public int Score { get { return score; } }
    public int HigScore { get { return highScore; } }
    public int NumbersGame { get { return numbersGame; } }

    public bool isDataUpdate =false;   //数据是否更新

    private Transform[,] map = new Transform[MAX_COLUMNS,MAX_ROWS];

    private void Awake()
    {
        //print(PlayerPrefs.GetInt("Test"));
        LoadData();
    }
    //判断每一个地图位置是否可用
    public bool IsValidMapPosition(Transform t)
    {
        foreach(Transform child in t)
        {                        
            if (child.tag != "Block")
            {                
                continue;
            }
            Vector2 pos = child.position.Round();

          //  Debug.Log("坐标 " + pos.x + " " + pos.y);
            if(IsInsideMap(pos) == false)
            {
                return false;
            }
            //有图形
            if(map[(int)pos.x,(int)pos.y] != null)
            {
                return false;
            }
        }
        return true;
    }

    //游戏是否结束
    public bool IsGameOver()
    {
        for(int i=NORMAL_ROWS;i<MAX_ROWS;i++)
        {
            for(int j =0;j<MAX_COLUMNS;j++)
            {
                if(map[j,i] !=null)
                {
                    numbersGame++;
                    SaveData();
                    return true;
                }
            }
        }
        return false;
    }

    //是否在地图内
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }
        
    //图形的叠加
    public bool PlaceShape(Transform t)
    {
        foreach(Transform child in t)
        {
            if (child.tag != "Block")
                continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }

    //是否能消除整行
    private bool CheckMap()
    {
        int count = 0;
        for(int i =0;i<MAX_ROWS;i++)
        {
            bool isFull = CheckIsRowFull(i);
            if(isFull)
            {
                count++;
                DeleteRow(i);
                MoveDownRowAbove(i + 1);
                i--;
            }
        }
        if (count > 0)
        {
            //更新分数
            score += (count * 100); 
            if(score > highScore)
            {
                highScore = score;
            }
            isDataUpdate = true;
            return true;
        }
        else
        {
            return false;
        }
    }
	
    //一行是否放满
    private bool CheckIsRowFull(int row)
    {
        for(int i=0;i<MAX_COLUMNS;i++)
        {
            if(map[i,row] == null)
            {
                return false;
            }
        }
        return true;
    }

    //消除一行
    private void DeleteRow(int row)
    {
        for(int i =0;i<MAX_COLUMNS;i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    //把消除一行上方方块下移
    private void MoveDownRowAbove(int row)
    {
        for(int i=row;i<MAX_ROWS;i++)
        {
            MoveDownRow(i);
        }
    }

    //把一行下移
    private void MoveDownRow(int row)
    {
        for(int i=0;i<MAX_COLUMNS;i++)
        {
            //只有格子不为空时才要移动
            if (map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //加载分数和游戏次数
    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        numbersGame = PlayerPrefs.GetInt("NumberGame", 0);
    }

    //存储分数和游戏次数
    private void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumberGame", numbersGame);
    }

    //重新开始游戏
    public void Restart()
    {
        for(int i=0;i< MAX_COLUMNS; i++)
        {
            for(int j=0;j< MAX_ROWS; j++)
            {
                if(map[i,j] != null)
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
                
            }
        }
        score = 0;
    }


    //清空游戏数据
    public void ClearData()
    {
        score = 0;
        highScore = 0;
        numbersGame = 0;
        SaveData();
    }
}
