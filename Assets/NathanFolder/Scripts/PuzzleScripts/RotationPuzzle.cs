using System.Collections;
using UnityEngine;

public class RotationPuzzle : MonoBehaviour
{
    public int[,] puzzleArray = new int[5,5];
    public int attempts = 0;
    int lastXIndex;
    int lastYIndex;
    int prevTravelDirection;
    int fourLeftBad = 0;
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
    
    void GeneratePuzzleArray()
    {
        
        do
        {
            puzzleArray[lastXIndex, lastYIndex] = 1;
            Debug.Log(lastXIndex + " - " + lastYIndex);
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
            int upLeft = Random.Range(0,2);
            //0 is horizontal, 1 is vertical
            int posNeg = Random.Range(0, 2);
           // Debug.Log(posNeg + " " + upLeft);
            //0 is neg, 1 is pos
            if(lastXIndex == 0 && ((upLeft == 0 && posNeg == 0) || (upLeft == 1 && posNeg == 0)))
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

                        
                            if (puzzleArray[lastXIndex - 1, lastYIndex] == 0)
                            {
                                currentTravelDirection = 3;
                                lastXIndex--;
                            }
                        
                       

                    }
                    else
                    {
                        //East

                        
                            if (puzzleArray[lastXIndex + 1, lastYIndex] == 0)
                            {
                                currentTravelDirection = 1;
                                lastXIndex++;
                            }
                        
                            
                           
                        
                    }
                }
                else
                {
                    //Vertical
                    if (posNeg == 0)
                    {
                        //North

                       

                            if (puzzleArray[lastXIndex, lastYIndex - 1] == 0)
                            {
                                currentTravelDirection = 0;
                                lastYIndex--;
                            }
                        
                    }
                    else
                    {
                        //South
                       

                            if (puzzleArray[lastXIndex, lastYIndex + 1] == 0)
                            {
                                currentTravelDirection = 2;
                                lastYIndex++;
                            }
                        
                    }
                }
            }
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
    }
}
