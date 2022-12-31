using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    private int audioDestroyTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(audioDestroyTime);
        Destroy(gameObject);
    }
}
