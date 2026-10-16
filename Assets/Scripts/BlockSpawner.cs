using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float spawnInterval = 2f;
    public float spawnY = 7f;

    private float timer;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnBlock();
            timer = spawnInterval;
        }
    }

    private void SpawnBlock()
    {
        int column = PickRandomOpenColumn();
        if (column < 0) return;

        int row = GridManager.Instance.ReserveNextRow(column);
        float targetY = GridManager.Instance.GetStackY(row);

        Vector3 spawnPos = new Vector3(
            GridManager.Instance.GetColumnX(column),
            spawnY,
            0f
        );

        GameObject newBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
        Block blockScript = newBlock.GetComponent<Block>();
        blockScript.Initialize(column, row, targetY);
    }

    private int PickRandomOpenColumn()
    {
        int cols = GridManager.Instance.columns;
        int start = Random.Range(0, cols);
        for (int i = 0; i < cols; i++)
        {
            int candidate = (start + i) % cols;
            if (!GridManager.Instance.IsColumnFull(candidate))
                return candidate;
        }
        return -1;
    }
}