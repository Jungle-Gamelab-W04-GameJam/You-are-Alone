using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    public bool Switch;
    public GameObject LeverDown;
    public GameObject LeverUp;
    public GameObject Cube;

    private Material cubeMaterial;
    public AudioClip doorSound;  // 재생할 소리 파일
    private AudioSource audioSource;  // 오디오 소스 컴포넌트


    // Start is called before the first frame update
    void Start()
    {
        LeverDown.SetActive(false);
        LeverUp.SetActive(true);
        cubeMaterial = Cube.transform.GetComponent<Renderer>().material;
        // 오디오 소스 컴포넌트를 가져오거나 추가
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
            LeverDown.SetActive(true);
            LeverUp.SetActive(false);

            cubeMaterial.color = Color.green;
        }
        else
        {
            LeverDown.SetActive(false);
            LeverUp.SetActive(true);

            cubeMaterial.color = Color.red;
        }

    }

    public void Use()
    {
        Switch = !Switch;
        audioSource.PlayOneShot(doorSound);  // 소리 재생

    }
}
