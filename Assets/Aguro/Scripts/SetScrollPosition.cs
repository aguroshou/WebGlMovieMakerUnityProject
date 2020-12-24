using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScrollPosition : MonoBehaviour
{
    public ScrollRect ScrollRect;
    // Start is called before the first frame update
    void Start()
    {
        ScrollRect.verticalNormalizedPosition = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
