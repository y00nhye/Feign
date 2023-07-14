using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }
    private void OnEnable()
    {
        StartCoroutine(Depart_co());
    }
    IEnumerator Depart_co()
    {
        Vector3 endPos = transform.position + new Vector3(12, 0, 0);

        while (Vector3.Distance(endPos, transform.position) > 0.1f)
        {
            transform.position += Vector3.right * 5f * Time.deltaTime;

            yield return null;
        }

        transform.localPosition = startPos;
        gameObject.SetActive(false);
    }
}
