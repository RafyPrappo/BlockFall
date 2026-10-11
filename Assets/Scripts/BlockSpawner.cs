using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float spawnInterval = 2f;
    public float spawnY = 8f;

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
        int randomColumn = Random.Range(0, GridManager.Instance.columns);

        Vector3 spawnPos = new Vector3(
            GridManager.Instance.GetColumnX(randomColumn),
            spawnY,
            0f
        );

        GameObject newBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
        Block blockScript = newBlock.GetComponent<Block>();

        float targetY = GridManager.Instance.GetStackY(0); // ground level for now
        blockScript.Initialize(randomColumn, targetY);
    }
}