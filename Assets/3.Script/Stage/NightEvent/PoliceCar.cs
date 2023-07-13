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
        StartCoroutine(Arrive_co());
        StartCoroutine(Depart_co());
    }
    IEnumerator Arrive_co()
    {
        Vector3 endPos = transform.position + new Vector3(5, 0, 0);

        while (Vector3.Distance(endPos, transform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(endPos, transform.position, 0.95f);

            yield return null;
        }

        transform.position = endPos;

        yield break;
    }
    IEnumerator Depart_co() //상황 끝날때 발동
    {
        yield return new WaitForSeconds(2f);
        
        Vector3 endPos = transform.position + new Vector3(5, 0, 0);

        while (Vector3.Distance(endPos, transform.position) > 0.01f)
        {
            transform.position += Vector3.right * 5f * Time.deltaTime;

            yield return null;
        }

        transform.localPosition = startPos;
        gameObject.SetActive(false);
    }
}
