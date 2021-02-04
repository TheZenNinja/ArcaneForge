using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!IsInvoking(nameof(Test)))
                StartCoroutine(Test());
            Invoke(nameof(test), 1);
        }
    }
    void test() => Debug.Log("1 sec");
    IEnumerator Test() 
    {
        Debug.Log("Press");
        float time = 0;
        while (Input.GetKey(KeyCode.F))
        {
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"Release {time}");
    }
}
