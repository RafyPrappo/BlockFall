using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public int columns = 7;
    public float columnWidth = 1f;
    public float groundY = 0f;
    public float blockHeight = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public float GetColumnX(int column)
    {
        float totalWidth = (columns - 1) * columnWidth;
        float startX = -totalWidth / 2f;
        return startX + column * columnWidth;
    }

    public float GetStackY(int stackHeight)
    {
        return groundY + (stackHeight * blockHeight) + (blockHeight / 2f);
    }
}