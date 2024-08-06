using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetObject : MonoBehaviour
{
    public DoorController exitDoor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            GameObject.FindGameObjectWithTag("InteractableDoor").GetComponent<DoorController>().LockOpenDoor();
            exitDoor.OpenDoor();
            Debug.Log("Next stage unlocked!");
            // 다음 스테이지로 이동 (현재는 Debug.Log로 대체)
            // SceneManager.LoadScene("NextStage");
        }
    }
}