using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("[Die Face Material]")]
    [SerializeField] Material dieFace;

    Rigidbody rig;

    private Animator bloodAni;
    private Vector3 startPos;

    private void Awake()
    {
        TryGetComponent(out rig);
        startPos = transform.localPosition;
        bloodAni = GameObject.Find("BloodEffect").GetComponent<Animator>();
    }
    private void OnEnable()
    {
        AudioManager.instance.KillSound();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Material[] mat = new Material[2] { other.GetComponentInChildren<SkinnedMeshRenderer>().material, dieFace };
            other.GetComponentInChildren<SkinnedMeshRenderer>().materials = mat;

            GameObject blood = other.transform.GetChild(2).gameObject;
            blood.SetActive(true);

            other.GetComponent<PlayerController>().isDie = true;

            rig.velocity = Vector3.zero;
            rig.useGravity = false;

            bloodAni.SetBool("Blood", true);

            StartCoroutine(PosReset_co());
        }
    }
    IEnumerator PosReset_co()
    {
        yield return new WaitForSeconds(2f);

        PosReset();
    }
    private void PosReset()
    {
        bloodAni.SetBool("Blood", false);
        transform.localPosition = startPos;
        rig.useGravity = true;
        gameObject.SetActive(false);
    }
}
