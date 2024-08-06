using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Chest"인지 확인
        if (collision.collider.CompareTag("Chest"))
        {
            // Chest 스크립트의 Unlock 메소드 호출
            Chest chest = collision.collider.GetComponent<Chest>();
            if (chest != null)
            {
                chest.Unlock();
                Destroy(gameObject);
            }
        }
    }
}
