using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RotationPuzzle : MonoBehaviour
{
    public int[,] puzzleArray = new int[5,5];

    public GameObject[,] inGameArray = new GameObject[5, 5];
    public int attempts = 0;
    int lastXIndex;
    int lastYIndex;
    int prevTravelDirection;
    int fourLeftBad = 0;
    [SerializeField] List<Vector2> TruePath = new List<Vector2>();
    [SerializeField] GameObject TilePrefab;
    [SerializeField] GameObject GridParent;

    public bool isCompleted;
    public UnityEvent PuzzleCompleted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(puzzleArray.GetLength(0));

        GeneratePuzzleArray();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void ClearArray()
    {
        for(int i = 0; i < puzzleArray.GetLength(0); i++)
        {
            for(int j = 0; j < puzzleArray.GetLength(1); j++)
            {
                puzzleArray[i, j] = 0;
            }
        }
        lastYIndex = 0;
        lastXIndex = 0;
        fourLeftBad = 0;
        prevTravelDirection = 0;
        attempts = 0;
        TruePath.Clear();
    }
    void GeneratePuzzleArray()
    {
        ClearArray();
        do
        {
            if (puzzleArray[lastXIndex,lastYIndex] == 0)
            {
                puzzleArray[lastXIndex, lastYIndex] = 1;
                Debug.Log(lastXIndex + " - " + lastYIndex);
                TruePath.Add(new Vector2(lastXIndex, lastYIndex));
            }
            
            
            if(lastXIndex == 4 && lastYIndex == 4)
            {
                break;
            }
            int currentTravelDirection = 0;
            bool skipRandom = false;
            if(Mathf.Abs(fourLeftBad) == 3)
            {
                skipRandom = true;
                if(fourLeftBad > 0)
                {
                    //go right
                    if(prevTravelDirection == 0)
                    {
                        currentTravelDirection = 1;
                    }
                    else if(prevTravelDirection == 1)
                    {
                        currentTravelDirection = 2;
                    }
                    else if(prevTravelDirection == 2)
                    {
                        currentTravelDirection = 3;
                    }
                    else if(prevTravelDirection == 3)
                    {
                        currentTravelDirection = 0;
                    }
                }
                else if(fourLeftBad < 0)
                {
                    //go left
                    if (prevTravelDirection == 0)
                    {
                        currentTravelDirection = 3;
                        lastXIndex--;
                    }
                    else if (prevTravelDirection == 1)
                    {
                        currentTravelDirection = 0;
                        lastXIndex--;
                    }
                    else if (prevTravelDirection == 2)
                    {
                        currentTravelDirection = 1;
                        lastXIndex++;
                    }
                    else if (prevTravelDirection == 3)
                    {
                        currentTravelDirection = 2;
                        lastYIndex++;
                    }
                }
                
            }
            int upLeft = Random.Range(0, 2);
            //0 is horizontal, 1 is vertical
            int posNeg = Random.Range(0, 2);
            // Debug.Log(posNeg + " " + upLeft);
            //0 is neg, 1 is pos
            if (lastXIndex == 0)
            {
                upLeft = Random.Range(0, 2);
                posNeg = Random.Range(0, 2);
                if (upLeft != 0 && posNeg != 1)
                {
                    upLeft = Random.Range(0, 2);
                    posNeg = Random.Range(0, 2);
                }
            }
            if (lastXIndex == 4)
            {
                upLeft = Random.Range(0, 2);
                posNeg = Random.Range(0, 2);
                if (upLeft != 0 && posNeg != 0)
                {
                    upLeft = Random.Range(0, 2);
                    posNeg = Random.Range(0, 2);
                }
            }
            if (lastYIndex == 0)
            {
                upLeft = Random.Range(0, 2);
                posNeg = Random.Range(0, 2);
                if (upLeft != 1 && posNeg != 1)
                {
                    upLeft = Random.Range(0, 2);
                    posNeg = Random.Range(0, 2);
                }
            }
            if (lastYIndex == 4)
            {
                upLeft = Random.Range(0, 2);
                posNeg = Random.Range(0, 2);
                if (upLeft != 1 && posNeg != 0)
                {
                    upLeft = Random.Range(0, 2);
                    posNeg = Random.Range(0, 2);
                }
            }
            if (lastXIndex == 0 && ((upLeft == 0 && posNeg == 0) || (upLeft == 1 && posNeg == 0)))
            {
                posNeg = 1;
                upLeft = 1;
            }
           if(lastXIndex == puzzleArray.GetLength(0) - 1 && ((upLeft == 0 && posNeg == 1) || (upLeft == 1 && posNeg == 0)))
            {
                posNeg = 1;
                upLeft = 1;
            }
            if (lastYIndex == 0 && ((upLeft == 1 && posNeg == 0) || (upLeft == 0 && posNeg == 0)))
            {
                posNeg = 1;
                upLeft = 0;
            }
            if (lastYIndex == puzzleArray.GetLength(1) - 1 && ((upLeft == 1 && posNeg == 1) || (upLeft == 0 && posNeg == 0)))
            {
                posNeg = 1;
                upLeft = 0;
            }
            if (skipRandom == false)
            {


                if (upLeft == 0)
                {
                    //horizontal
                    if (posNeg == 0)
                    {
                        //West

                        if(lastXIndex != 0)
                        {
                            if (puzzleArray[lastXIndex - 1, lastYIndex] == 0)
                            {
                                currentTravelDirection = 3;
                                lastXIndex--;
                            }
                        }
                            
                        
                       

                    }
                    else
                    {
                        //East

                        if(lastXIndex != 4)
                        {
                            if (puzzleArray[lastXIndex + 1, lastYIndex] == 0)
                            {
                                currentTravelDirection = 1;
                                lastXIndex++;
                            }
                        }
                            
                        
                            
                           
                        
                    }
                }
                else
                {
                    //Vertical
                    if (posNeg == 0)
                    {
                        //North


                        if (lastYIndex != 0)
                        {
                            if (puzzleArray[lastXIndex, lastYIndex - 1] == 0)
                            {
                                currentTravelDirection = 0;
                                lastYIndex--;
                            }
                        }
                            
                        
                    }
                    else
                    {
                        //South

                        if (lastYIndex != 4)
                        {
                            if (puzzleArray[lastXIndex, lastYIndex + 1] == 0)
                            {
                                currentTravelDirection = 2;
                                lastYIndex++;
                            }
                        }                        
                    }
                }
            }


            if (lastXIndex > 4)
            {
                lastXIndex = 4;
                currentTravelDirection = prevTravelDirection;
            }else if(lastXIndex < 0) { lastXIndex = 0; currentTravelDirection = prevTravelDirection; }
            if (lastYIndex > 4)
            {
                lastYIndex = 4;
                currentTravelDirection = prevTravelDirection;
            }
            else if (lastYIndex < 0) { lastYIndex = 0; currentTravelDirection = prevTravelDirection; }



                if(prevTravelDirection != currentTravelDirection)
                {
                    if(prevTravelDirection == 0 && currentTravelDirection == 3)
                    {
                        fourLeftBad++;
                    }
                    else if(prevTravelDirection == 3 && currentTravelDirection == 0)
                    {
                        fourLeftBad--;
                    }
                    if(prevTravelDirection - currentTravelDirection == -1)
                    {
                        fourLeftBad--;
                    }
                    else
                    {
                        fourLeftBad++;
                    }
                }
                prevTravelDirection = currentTravelDirection;
            
        } while (attempts++ < 500);
        
        for (int i = 0; i < puzzleArray.GetLength(0); i++)
        {
            string rowString = "";
            for (int j = 0; j < puzzleArray.GetLength(1); j++)
            {
                rowString += puzzleArray[i, j].ToString() + " ";
            }
            Debug.Log(rowString);
        }
        if (puzzleArray[4, 4] == 0)
        {
            GeneratePuzzleArray();
        }
        else
        {

        
        for (int i = 0; i < puzzleArray.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleArray.GetLength(1); j++)
            {
                GameObject newGridElement = Instantiate(TilePrefab);
                inGameArray[i,j] = newGridElement;
                newGridElement.transform.parent = GridParent.transform;
            }
        }
        for (int i = 0; i < puzzleArray.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleArray.GetLength(1); j++)
            {

                inGameArray[i, j].GetComponent<RotPuzPiece>().cordOnArray = new Vector2(i, j);
                inGameArray[i, j].GetComponent<RotPuzPiece>().arrayOfPuzzle = inGameArray;

                inGameArray[i, j].GetComponent<RotPuzPiece>().rotpuzzleScript = this;
            }
        }
            for (int i = 0; i < TruePath.Count; i++)
            {
                if (i == 0)
                {
                    if (TruePath[i + 1].x == 1)
                    {
                        inGameArray[((int)TruePath[i].x), (int)TruePath[i].y].GetComponent<RotPuzPiece>().type = RotPuzPiece.PieceType.LShape;

                    }
                    else
                    {
                        inGameArray[((int)TruePath[i].x), (int)TruePath[i].y].GetComponent<RotPuzPiece>().type = RotPuzPiece.PieceType.Straight;
                    }
                }
                else if (i == TruePath.Count - 1)
                {
                    if (TruePath[i - 1].x < TruePath[i].x)
                    {
                        inGameArray[((int)TruePath[i].x), (int)TruePath[i].y].GetComponent<RotPuzPiece>().type = RotPuzPiece.PieceType.LShape;
                    }
                    else
                    {
                        inGameArray[((int)TruePath[i].x), (int)TruePath[i].y].GetComponent<RotPuzPiece>().type = RotPuzPiece.PieceType.Straight;
                    }
                }
                else
                {
                    if (Mathf.Abs(TruePath[i - 1].x - TruePath[i + 1].x) == 2 || Mathf.Abs(TruePath[i - 1].y - TruePath[i + 1].y) == 2)
                    {
                        inGameArray[((int)TruePath[i].x), (int)TruePath[i].y].GetComponent<RotPuzPiece>().type = RotPuzPiece.PieceType.Straight;
                    }
                    else
                    {
                        inGameArray[((int)TruePath[i].x), (int)TruePath[i].y].GetComponent<RotPuzPiece>().type = RotPuzPiece.PieceType.LShape;
                    }
                }
            }
        }
    }
    public void TellPuzzleCompleted()
    {
        if(isCompleted == false)
        {
            PuzzleCompleted.Invoke();
            isCompleted = true;
        }
    }
}
