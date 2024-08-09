using System.Collections;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    public bool Switch;
    public GameObject ButtonFalse;
    public GameObject ButtonTrue;
    private Material TrueMaterial;
    private Material FalseMaterial;
    public LightManager lightManager; // Reference to the LightManager script

    public AudioClip clickSound; // Audio clip for the button click sound
    private AudioSource audioSource; // AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        ButtonTrue.SetActive(false);
        ButtonFalse.SetActive(true);
        TrueMaterial = ButtonTrue.transform.GetComponent<Renderer>().material;
        FalseMaterial = ButtonFalse.transform.GetComponent<Renderer>().material;

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            ButtonTrue.SetActive(true);
            ButtonFalse.SetActive(false);
        }
        else
        {
            ButtonTrue.SetActive(false);
            ButtonFalse.SetActive(true);
        }
    }

    public void Use()
    {
        if (!Switch) // Only execute when Switch is false
        {
            // Play the click sound
            PlayClickSound();
            StartCoroutine(SwitchOnCoroutine());
        }
    }

    private IEnumerator SwitchOnCoroutine()
    {
        Switch = true;
        lightManager.OnButtonPress(); // Play the light pattern
        yield return new WaitForSeconds(9);
        Switch = false;
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void OnCollisionEnter(Collision collision) // Trigger action on collision
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop")) // Check if the colliding object is in the "Prop" layer
        {
            Use();
        }
    }
}
