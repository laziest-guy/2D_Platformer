using UnityEngine;
using System.Collections; // Coroutine을 사용하기 위해 필요합니다.

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab으로 만든 상자
    public float respawnTime = 5.0f; // 재생성될 시간 (5초)

    private GameObject currentPlatform;

    void Start()
    {
        SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        // 상자를 Prefab으로부터 생성하고, 현재 상자에 저장
        currentPlatform = Instantiate(platformPrefab, transform.position, Quaternion.identity);

        // 생성된 상자에게 스포너(이 스크립트) 정보를 전달
        DisappearingPlatform platformScript = currentPlatform.GetComponent<DisappearingPlatform>();
        if (platformScript != null)
        {
            platformScript.spawner = this;
        }
    }

    public void OnPlatformDestroyed()
    {
        // 상자가 파괴되면 이 함수가 호출됩니다.
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // 지정된 시간(respawnTime)만큼 기다립니다.
        yield return new WaitForSeconds(respawnTime);

        // 상자를 다시 생성합니다.
        SpawnPlatform();
    }
}