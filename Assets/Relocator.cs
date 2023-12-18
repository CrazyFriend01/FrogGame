using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocator : MonoBehaviour
{
    public GameObject RelocatorTarget;
    //public GameObject Relocat;
    public Vector3 targetOffset;
    private Vector3 targetPos;
    RectTransform trans;

    // Start is called before the first frame update
    void Start()
    {
        //RelocatorTarget = GetComponent<GameObject>();
        trans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = RelocatorTarget.transform.localPosition;
        trans.anchoredPosition3D = targetPos + targetOffset;
    }
}
