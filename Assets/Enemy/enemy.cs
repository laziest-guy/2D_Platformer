using UnityEngine;
using UnityEngine.UI;


public class Enemy : Interactable
{
    public Slider healthSlider;
    public int maxHp = 50;
    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHp;
            healthSlider.value = currentHp;
        }
    }

    public override void Interact(float damage, Vector2 hitpoint)
    {
        TakeDamage((int)damage);

    }

    private void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{gameObject.name}이(가) {damage} 데미지를 입음. 남은 HP: {currentHp}");

    // 슬라이더 값 업데이트
        if (healthSlider != null)
        {
           healthSlider.value = currentHp;
        }

        if (currentHp <= 0)
        {
            Die();
        }   
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name}이(가) 쓰러짐!");
        Destroy(gameObject);
    }
}

