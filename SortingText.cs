using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingText : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
