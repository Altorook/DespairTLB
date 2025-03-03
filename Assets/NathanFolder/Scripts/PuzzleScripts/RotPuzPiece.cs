using UnityEngine;

public class RotPuzPiece : MonoBehaviour
{
    public enum PieceType
    {
        Straight,
        LShape,
        TShape,
        Cross,
        Blank,
    }
    public int currentRotation = 0;
    public bool LeftConnection;
    public bool RightConnection;
    public bool UpConnection;
    public bool DownConnection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        this.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}
