using UnityEngine;
using UnityEngine.Events;

public class PillarPuzzleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool[] lightsSolved = new bool[3];
    [SerializeField] Light[] lights;
    public UnityEvent PuzzleFinished;
    void Start()
    {
        
    }
    public void NewPillarSolved(int indexRGB)
    {
        lightsSolved[indexRGB] = true;
        lights[indexRGB].enabled = lightsSolved[indexRGB];
        AreAllSolved();
    }
    public void PillarLost(int indexRGB)
    {
        lightsSolved[indexRGB] = false;
        lights[indexRGB].enabled = lightsSolved[indexRGB];
        AreAllSolved();
    }
    private void AreAllSolved()
    {
        int isFinished = 0;
        for(int i = 0; i < lightsSolved.Length; i++)
        {
            if (lightsSolved[i])
            {
                isFinished++;
            }
        }
        if (isFinished >= 3)
        {
            PuzzleFinished.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
