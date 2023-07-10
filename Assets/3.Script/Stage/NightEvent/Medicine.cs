using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    [Header("[Revival Face Material]")]
    [SerializeField] Material revivalFace;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    private void OnEnable()
    {
        StartCoroutine(Drop());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().isDie)
            {
                other.GetComponent<PlayerController>().isDie = false;
                other.transform.GetChild(3).gameObject.SetActive(true);

                StartCoroutine(Heal(other));
            }
            else
            {
                Invoke("PosReset", 2f);
            }
        }
    }
    IEnumerator Drop()
    {
        Vector3 endPos = transform.position - new Vector3(0, 1.7f, 0);

        while (Vector3.Distance(endPos, transform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(endPos, transform.position, 0.995f);

            yield return null;
        }

        transform.position = endPos;
    }
    IEnumerator Heal(Collider other)
    {
        yield return new WaitForSeconds(1f);

        other.transform.GetChild(2).gameObject.SetActive(false);
        Material[] mat = new Material[2] { other.GetComponentInChildren<SkinnedMeshRenderer>().material, revivalFace };
        other.GetComponentInChildren<SkinnedMeshRenderer>().materials = mat;

        yield return new WaitForSeconds(2f);

        PosReset();
    }
    private void PosReset()
    {
        transform.position = startPos;
        gameObject.SetActive(false);
    }
}
