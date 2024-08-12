using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7_ButtonController : MonoBehaviour
{
    public float buttonTime = 2f;

    public bool Switch;
    public GameObject ButtonFalse;
    public GameObject ButtonTrue;
    public GameObject Door;

    private Stage7_Door1 doorController;
    private Material TrueMaterial;
    private Material FalseMaterial;
    public AudioClip doorSound;  // 재생할 소리 파일
    private AudioSource audioSource;  // 오디오 소스 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        // 오디오 소스 컴포넌트를 가져오거나 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        ButtonTrue.SetActive(false);
        ButtonFalse.SetActive(true);
        TrueMaterial = ButtonTrue.transform.GetComponent<Renderer>().material;
        FalseMaterial = ButtonFalse.transform.GetComponent<Renderer>().material;
        TrueMaterial.color = Color.green;
        FalseMaterial.color = Color.red;
        doorController = Door.GetComponent<Stage7_Door1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            ButtonTrue.SetActive(true);
            ButtonFalse.SetActive(false);
            doorController.OpenDoor();
        }
        else
        {
            ButtonTrue.SetActive(false);
            ButtonFalse.SetActive(true);
            doorController.CloseDoor();
        }


    }
    public void Use()
    {
        if (!Switch) // Play only when Switch is false.
        {
            StartCoroutine(SwitchOnCoroutine());
        }
        audioSource.PlayOneShot(doorSound);  // 소리 재생

    }

    private IEnumerator SwitchOnCoroutine()
    {
        Switch = true;
        yield return new WaitForSeconds(buttonTime);
        Switch = false;
    }

    private void OnTriggerEnter(Collider collision) // when it take some bugs, let's make it by triggerEnter.
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop")) // change name by Object????
        {
            Use();
        }
    }

}
