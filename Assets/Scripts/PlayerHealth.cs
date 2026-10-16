using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;

    [Header("HUD")]
    public Image[] hpBoxes;
    public Color fullColor  = new Color(0.3f, 1f, 0.4f);
    public Color emptyColor = new Color(0.15f, 0.15f, 0.15f, 0.5f);

    private void Start()
    {
        currentHP = maxHP;
        UpdateHUD();
    }

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
        UpdateHUD();

        if (currentHP <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(maxHP, currentHP + amount);
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        for (int i = 0; i < hpBoxes.Length; i++)
        {
            hpBoxes[i].color = (i < currentHP) ? fullColor : emptyColor;
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
