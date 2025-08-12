using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // 플레이어 오브젝트에서 PlayerController 스크립트를 찾아 RespawnPlayer() 함수 호출
            other.GetComponent<PlayerController>().RespawnPlayer();
        }
    }
}