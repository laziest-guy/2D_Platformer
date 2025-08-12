using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    // 스포너를 담을 변수 추가
    public PlatformSpawner spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2초 뒤에 상자를 파괴합니다.
            // 기존 Destroy(gameObject, 2.0f); 코드를 아래처럼 변경합니다.
            Destroy(gameObject, 2.0f);
        }
    }
    
    // 오브젝트가 파괴될 때 자동으로 호출되는 함수
    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.OnPlatformDestroyed();
        }
    }
}