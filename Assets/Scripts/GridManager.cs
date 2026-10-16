using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public int columns = 7;
    public int maxRows = 10;
    public float columnWidth = 1.5f;
    public float groundY = 0f;
    public float blockHeight = 1.5f;

    private int[] columnHeights;
    private Block[,] grid;

    private void Awake()
    {
        Instance = this;
        columnHeights = new int[columns];
        grid = new Block[columns, maxRows];
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

    public int GetColumnHeight(int column)
    {
        return columnHeights[column];
    }

    public bool IsColumnFull(int column)
    {
        return columnHeights[column] >= maxRows;
    }

    public int ReserveNextRow(int column)
    {
        int row = columnHeights[column];
        columnHeights[column]++;
        return row;
    }

    public void RegisterBlock(int column, int row, Block block)
    {
        if (column < 0 || column >= columns || row < 0 || row >= maxRows) return;
        grid[column, row] = block;
    }
}