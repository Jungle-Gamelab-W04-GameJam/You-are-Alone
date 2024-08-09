using UnityEngine;

public class Test4_Rope : MonoBehaviour
{
    public FloorButton floorButton;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the tag "Knife"
        if (other.CompareTag("Knife"))
        {

            Destroy(gameObject);

        }
    }


}
