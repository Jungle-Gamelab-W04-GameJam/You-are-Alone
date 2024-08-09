using System.Collections;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    public bool Switch;
    public GameObject ButtonFalse;
    public GameObject ButtonTrue;
    private Material TrueMaterial;
    private Material FalseMaterial;
    public LightManager lightManager; // LightManager 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        ButtonTrue.SetActive(false);
        ButtonFalse.SetActive(true);
        TrueMaterial = ButtonTrue.transform.GetComponent<Renderer>().material;
        FalseMaterial = ButtonFalse.transform.GetComponent<Renderer>().material;
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
        if (!Switch) // Switch가 false일 때만 실행
        {
            StartCoroutine(SwitchOnCoroutine());
        }
    }

    private IEnumerator SwitchOnCoroutine()
    {
        Switch = true;
        lightManager.OnButtonPress(); // 전구 패턴 재생
        yield return new WaitForSeconds(6);
        Switch = false;
    }

    private void OnCollisionEnter(Collision collision) // 충돌 시 작동
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop")) // 충돌한 오브젝트가 "Prop" 레이어인지 확인
        {
            Use();
        }
    }
}
