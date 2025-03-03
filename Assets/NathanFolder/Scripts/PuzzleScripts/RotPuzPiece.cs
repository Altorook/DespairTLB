using UnityEngine;
using UnityEngine.UI;

public class RotPuzPiece : MonoBehaviour
{
    public enum PieceType
    {
        Straight = 0,
        LShape = 1,
        TShape = 2,
        Cross = 3,
        Blank = 4,
    }
    public PieceType type;
    public GameObject[,] arrayOfPuzzle;
    public RotPuzPiece[,] RotPuzArray = new RotPuzPiece[5,5];
    public Vector2 cordOnArray;
    public int currentRotation = 0;
    public bool LeftConnection;
    public bool RightConnection;
    public bool UpConnection;
    public bool DownConnection;

    public bool isConnectedToStart = false;
    [SerializeField] int StraightProb;
    [SerializeField] int LShapeProb;
    [SerializeField] int TShapeProb;
    [SerializeField] int CrossProb;
    [SerializeField] int BlankProb;
    [SerializeField] int ConvertToTMax;
    [SerializeField] int ConvertToCrossMax;
    [SerializeField] Sprite[] pieceShapes = new Sprite[5];
    Sprite currentTexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < arrayOfPuzzle.GetLength(0); i++)
        {
            for(int j = 0; j < arrayOfPuzzle.GetLength(1); j++)
            {
                RotPuzArray[i,j] = arrayOfPuzzle[i,j].GetComponent<RotPuzPiece>();
            }
        }
        if(type == PieceType.Straight)
        {
            int RandomT = Random.Range(0, ConvertToTMax);
            int RandomCross = Random.Range(0, ConvertToCrossMax);
            if(RandomT  == 1)
            {
                type = PieceType.TShape;
            }
            if (RandomCross == 1)
            {
                type = PieceType.Cross;
            }
        }
        if(type == PieceType.Blank)
        {
           int randomizePiece = Random.Range(0, StraightProb + TShapeProb + CrossProb + LShapeProb + BlankProb);
            if(randomizePiece  < StraightProb)
            {
                type = PieceType.Straight;
            }
            else if(randomizePiece < StraightProb +LShapeProb)
            {
                type = PieceType.LShape;
            }
            else if(randomizePiece < StraightProb + LShapeProb + TShapeProb)
            {
                type = PieceType.TShape;
            }
            else if(randomizePiece < StraightProb + TShapeProb + CrossProb + LShapeProb)
            {
                type = PieceType.Cross;
            }
            else
            {
                type = PieceType.Blank;
            }
        }
        
        if(type == PieceType.Straight)
        {
            currentTexture = pieceShapes[0];
            LeftConnection = true;
            RightConnection = true;
        }
        else if(type == PieceType.LShape)
        {
            currentTexture = pieceShapes[1];
            UpConnection = true;
            RightConnection = true;
        }
        else if(type == PieceType.TShape)
        {
            currentTexture = pieceShapes[2];
            LeftConnection = true;
            RightConnection = true;
            UpConnection = true;
        }
        else if(type == PieceType.Cross)
        {
            currentTexture = pieceShapes[3];
            LeftConnection = true;
            RightConnection = true;
            UpConnection = true;
            DownConnection = true;
        }
        else
        {
            currentTexture = pieceShapes[4];
            LeftConnection = false;
            RightConnection = false;
            UpConnection = false;
            DownConnection = false;
        }
        this.gameObject.GetComponent<Image>().sprite = currentTexture;
        int rotationsRand = Random.Range(0, 4);
        for(int i = 0; i < rotationsRand; i++)
        {
            Rotate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Rotate()
    {
  
        currentRotation += 90;
        if(currentRotation >= 360)
        {
            currentRotation = 0;
        }
        bool tempRightConnection = false;
        bool tempLeftConnection = false;
        bool tempUpConnection = false;
        bool tempDownConnection = false;

        if (LeftConnection)
        {
            LeftConnection = false;
            tempDownConnection = true;
        }
        if (DownConnection)
        {
            DownConnection = false;
            tempRightConnection = true;
        }
        if (RightConnection)
        {
            RightConnection = false;
            tempUpConnection = true;
        }
        if (UpConnection)
        {
            UpConnection = false;
            tempLeftConnection = true;
        }
        
        LeftConnection = tempLeftConnection;
        DownConnection = tempDownConnection;
        RightConnection = tempRightConnection;
        UpConnection = tempUpConnection;
        UpdateConnections();


        Unconnected();
        this.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
       
    }
    public void RelayConnectionsGained()
    {
        if (cordOnArray.y != 0)
        {
            if (LeftConnection && RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y - 1].RightConnection && !RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y - 1].isConnectedToStart && isConnectedToStart)
            {
                RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y - 1].isConnectedToStart = true;
                RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y - 1].UpdateConnections();
            }
          
        }

        if (cordOnArray.x != 0)
        {
            if (UpConnection && RotPuzArray[(int)cordOnArray.x - 1, (int)cordOnArray.y].DownConnection && !RotPuzArray[(int)cordOnArray.x - 1, (int)cordOnArray.y].isConnectedToStart && isConnectedToStart)
            {
                RotPuzArray[(int)cordOnArray.x - 1, (int)cordOnArray.y].isConnectedToStart = true;
                RotPuzArray[(int)cordOnArray.x - 1, (int)cordOnArray.y].UpdateConnections();
            }
        }

        if (cordOnArray.y < RotPuzArray.GetLength(0) - 1)
        {
            if (RightConnection && RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y + 1].LeftConnection && !RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y + 1].isConnectedToStart && isConnectedToStart)
            {
                RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y + 1].isConnectedToStart = true;
                RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y + 1].UpdateConnections();
            }

        }

        if (cordOnArray.x < RotPuzArray.GetLength(1) - 1)
        {
            if (DownConnection && RotPuzArray[(int)cordOnArray.x + 1, (int)cordOnArray.y].UpConnection && !RotPuzArray[(int)cordOnArray.x + 1, (int)cordOnArray.y].isConnectedToStart && isConnectedToStart)
            {
                RotPuzArray[(int)cordOnArray.x + 1, (int)cordOnArray.y].isConnectedToStart = true;
                RotPuzArray[(int)cordOnArray.x + 1, (int)cordOnArray.y].UpdateConnections();
            }
      
        }
    }
    public void Unconnected()
    {
        for(int i = 0; i < RotPuzArray.GetLength(0); i++)
        {
            for(int j = 0; j < RotPuzArray.GetLength(1); j++)
            {
                RotPuzArray[i, j].isConnectedToStart = false;
            }
        }
        RotPuzArray[0, 0].UpdateConnections();
        RotPuzArray[0, 0].RelayConnectionsGained();
        for (int i = 0; i < RotPuzArray.GetLength(0); i++)
        {
            for (int j = 0; j < RotPuzArray.GetLength(1); j++)
            {
                RotPuzArray[i, j].UpdateConnections();
            }
        }
    }
    public void UpdateConnections()
    {
        if (cordOnArray == new Vector2(0, 0))
        {
            if (LeftConnection)
            {
                isConnectedToStart = true;
            }
            else
            {
                isConnectedToStart = false;
            }
        }
        else
        {
            bool noLeftConnection = false;
            bool noRightConnection = false;
            bool noUpConnection = false;
            bool noDownConnection = false;
            if (cordOnArray.y != 0)
            {
                if (LeftConnection && RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y - 1].RightConnection && RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y - 1].isConnectedToStart)
                {
                    isConnectedToStart = true;
                }
                else
                {
                    noLeftConnection = true;
                }
            }
            else
            {
                noLeftConnection = true;
            }
            if (cordOnArray.x != 0)
            {
                if (UpConnection && RotPuzArray[(int)cordOnArray.x - 1, (int)cordOnArray.y].DownConnection && RotPuzArray[(int)cordOnArray.x - 1, (int)cordOnArray.y].isConnectedToStart)
                {
                    isConnectedToStart = true;
                }
                else
                {
                    noUpConnection = true;
                }
            }
            else
            {
                noUpConnection = true;
            }
            if (cordOnArray.y < RotPuzArray.GetLength(0) - 1)
            {
                if (RightConnection && RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y + 1].LeftConnection && RotPuzArray[(int)cordOnArray.x, (int)cordOnArray.y + 1].isConnectedToStart)
                {
                    isConnectedToStart = true;
                }
                else
                {
                    noRightConnection = true;
                }
            }
            else
            {
                noRightConnection = true;
            }
            if (cordOnArray.x < RotPuzArray.GetLength(1) - 1)
            {
                if (DownConnection && RotPuzArray[(int)cordOnArray.x + 1, (int)cordOnArray.y].UpConnection && RotPuzArray[(int)cordOnArray.x + 1, (int)cordOnArray.y].isConnectedToStart)
                {
                    isConnectedToStart = true;
                }
                else
                {
                    noDownConnection = true;
                }
            }
            else
            {
                noDownConnection = true;
            }
            if (noDownConnection && noLeftConnection && noRightConnection && noUpConnection) { isConnectedToStart = false; }
            
            
        }
        if (isConnectedToStart)
        {
            RelayConnectionsGained();
        }
        if (isConnectedToStart)
        {
            this.gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
