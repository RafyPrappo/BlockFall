using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] public int column;
    [HideInInspector] public int row;
    [HideInInspector] public bool isLanded = false;

    public int damageToPlayer = 1;

    private float fallSpeed = 5f;
    private float targetY;
    private bool hasDealtDamage = false;

    public void Initialize(int col, int r, float targetYPos)
    {
        column = col;
        row = r;
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

            GridManager.Instance.RegisterBlock(column, row, this);
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        if (hasDealtDamage) return;
        if (isLanded) return;
        if (!other.CompareTag("Player")) return;

        float blockBottom = GetComponent<Collider>().bounds.min.y;
        float playerCenter = other.bounds.center.y;

        if (blockBottom < playerCenter) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damageToPlayer);
            hasDealtDamage = true;
        }
    }
}