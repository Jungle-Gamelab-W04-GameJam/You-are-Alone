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
            // ���� ���������� �̵� (����� Debug.Log�� ��ü)
            // SceneManager.LoadScene("NextStage");
        }
    }
}