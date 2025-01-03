using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum turn
{
    cross,
    nulls
}
//[ExecuteAlways]
public class Controller : MonoBehaviour
{
    public Cell[] coloumn = new Cell[10];
    public GameObject[] Figures = new GameObject[2];
    public GameObject winLine;
    public turn currentTurn;

    //public GameObject test;


    public void SetFigure(int btn)
    {
        //int cellIndex = 0;
        for(int i = 0; i < coloumn[btn].line.Length; i++)
        {
            if (coloumn[btn].line[i] == 0)
            {
                coloumn[btn].line[i] = (int)currentTurn + 1;
                //cellIndex = i;

                Instantiate(Figures[(int)currentTurn], coloumn[btn].spawnPointsCell[i].position, Quaternion.identity, coloumn[btn].spawnPointsCell[i]);

                if(i >= 3)
                {
                    CheckWinColoum(btn, i, (int)currentTurn + 1);
                }
                CheckWinLines(btn, i, (int)currentTurn + 1);
                CheckWinDiagonalsRight(btn, i, (int)currentTurn + 1);
                CheckWinDiagonalsLeft(btn, i, (int)currentTurn + 1);

                currentTurn = (turn)Convert.ToInt32(!Convert.ToBoolean((int)currentTurn));
                break;
            }
        }
    }

    private void CheckWinDiagonalsRight(int col, int currentCell, int currentFigure)
    {
        int winDetect = 0;
        int verticalIndex = -3;
        for(int i = col - 3; i < col + 4; i++)
        {
            if (currentCell + verticalIndex >= 0 && currentCell + verticalIndex <= 4 && i >= 0 && i <= 9)
            {
                if (coloumn[i].line[currentCell + verticalIndex] == currentFigure)
                {
                    winDetect++;
                }
                else
                {
                    winDetect = 0;
                }
                if (winDetect == 4)
                {
                    GameObject temp = Instantiate(winLine, coloumn[i].spawnPointsCell[currentCell + verticalIndex].position + new Vector3(-1.527f, -1.527f, 0), Quaternion.Euler(new Vector3(0, 0, 45f)), coloumn[i].spawnPointsCell[currentCell + verticalIndex]);
                    temp.transform.localScale += new Vector3(150, 0, 0);
                    Debug.Log("WINNER: " + currentTurn.ToString());
                    break;
                }
            }
            verticalIndex++;
        }
    }

    private void CheckWinDiagonalsLeft(int col, int currentCell, int currentFigure)
    {
        int winDetect = 0;
        int verticalIndex = 3;
        for (int i = col - 3; i < col + 4; i++)
        {
            if (currentCell + verticalIndex >= 0 && currentCell + verticalIndex <= 4 && i >= 0 && i <= 9)
            {
                if (coloumn[i].line[currentCell + verticalIndex] == currentFigure)
                {
                    winDetect++;
                }
                else
                {
                    winDetect = 0;
                }
                if (winDetect == 4)
                {
                    GameObject temp = Instantiate(winLine, coloumn[i].spawnPointsCell[currentCell + verticalIndex].position + new Vector3(-1.527f, 1.527f, 0), Quaternion.Euler(new Vector3(0, 0, -45f)), coloumn[i].spawnPointsCell[currentCell + verticalIndex]);
                    temp.transform.localScale += new Vector3(150, 0, 0);
                    Debug.Log("WINNER: " + currentTurn.ToString());
                    break;
                }
            }
            verticalIndex--;
        }
    }

    private void CheckWinLines(int col, int currentCell, int currentFigure)
    {
        int winDetect = 0;
        for(int i = col - 3; i < col + 4; i++)
        {
            if (i >= 0 && i <= 9)
            {
                if (coloumn[i].line[currentCell] == currentFigure)
                {
                    winDetect++;
                }
                else
                {
                    winDetect = 0;
                }
                if (winDetect == 4)
                {
                    Instantiate(winLine, coloumn[i].spawnPointsCell[currentCell].position + new Vector3(-1.527f, 0, 0), Quaternion.identity, coloumn[i].spawnPointsCell[currentCell]);
                    Debug.Log("WINNER: " + currentTurn.ToString());
                    break;
                }
            }
        }
    }

    private void CheckWinColoum(int col, int currentCell, int currentFigure)
    {
        if (coloumn[col].line[currentCell - 1] == currentFigure && coloumn[col].line[currentCell - 2] == currentFigure && coloumn[col].line[currentCell - 3] == currentFigure)
        {
            Instantiate(winLine, coloumn[col].spawnPointsCell[currentCell].position + new Vector3(0, -1.527f, 0), Quaternion.Euler(new Vector3(0, 0, 90f)), coloumn[col].spawnPointsCell[currentCell]);

            Debug.Log("WINNER: " + currentTurn.ToString());
        }
    }

    [ContextMenu("Find")]
    public void FindAllObj()
    {
        for (int i = 0; i < coloumn.Length; i++)
        {
            for (int j = 0; j < coloumn[i].spawnPointsCell.Length; j++)
            {
                coloumn[i].spawnPointsCell[j] = GameObject.Find("Canvas/GameSpace/Line (" + (j+1) + ")/Image (" + (i+1) + ")").transform;
            }
        }

        //test = GameObject.Find("Canvas/GameSpace/Line (5)/Image (3)");
    }
}
