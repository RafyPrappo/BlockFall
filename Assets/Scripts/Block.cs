using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] public int column;
    [HideInInspector] public bool isLanded = false;

    private float fallSpeed = 5f;
    private float targetY;

    public void Initialize(int col, float targetYPos)
    {
        column = col;
        targetY = targetYPos;

        Vector3 pos = transform.position;
        pos.x = GridManager.Instance.GetColumnX(col);
        transform.position = pos;
    }

    private void Update()
    {
        if (isLanded) return;

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= targetY)
        {
            Vector3 pos = transform.position;
            pos.y = targetY;
            transform.position = pos;
            isLanded = true;
        }
    }
}