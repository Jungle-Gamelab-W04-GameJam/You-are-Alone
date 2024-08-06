using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab; // 생성할 아이템의 프리팹
    public Transform spawnPoint; // 아이템이 생성될 위치

    public void Unlock()
    {
        if (itemPrefab != null && spawnPoint != null)
        {
            // 지정된 위치에 아이템 프리팹 생성
            Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // 아이템 생성 후 상자를 비활성화하거나 삭제
        Destroy(gameObject);
    }
}
