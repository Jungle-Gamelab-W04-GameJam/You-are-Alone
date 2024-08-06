using UnityEngine;
using System.Collections.Generic;

public class WeightPlatform : MonoBehaviour
{
    public float requiredWeight = 10f;
    public float currentWeight = 0f;
    public bool isActivated = false;
    public Door controlledDoor; // 이 발판이 제어할 문

    private Dictionary<Collider, float> objectWeights = new Dictionary<Collider, float>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            float objectWeight = collision.rigidbody.mass;
            objectWeights[collision.collider] = objectWeight;
            currentWeight += objectWeight;
            CheckActivation();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null && objectWeights.ContainsKey(collision.collider))
        {
            float objectWeight = objectWeights[collision.collider];
            currentWeight -= objectWeight;
            objectWeights.Remove(collision.collider);
            CheckActivation();
        }
    }

    private void CheckActivation()
    {
        if (currentWeight >= requiredWeight && !isActivated)
        {
            isActivated = true;
            ActivatePlatform();
        }
        else if (currentWeight < requiredWeight && isActivated)
        {
            isActivated = false;
            DeactivatePlatform();
        }

        Debug.Log($"Platform {gameObject.name} - Current Weight: {currentWeight}");
    }

    private void ActivatePlatform()
    {
        Debug.Log($"Platform {gameObject.name} activated!");
        if (controlledDoor != null)
        {
            controlledDoor.OpenDoor();
        }
    }

    private void DeactivatePlatform()
    {
        Debug.Log($"Platform {gameObject.name} deactivated!");
        if (controlledDoor != null)
        {
            controlledDoor.CloseDoor();
        }
    }
}