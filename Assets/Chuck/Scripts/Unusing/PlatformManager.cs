using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public WeightPlatform platform1;
    public WeightPlatform platform2;
    public Door door;

    private void Update()
    {
        if (platform1.isActivated && platform2.isActivated)
        {
            door.OpenDoor();
        }
        else
        {
            door.CloseDoor();
        }
    }
}