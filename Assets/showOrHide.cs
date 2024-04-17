using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class showOrHide : MonoBehaviour
{

    public GameObject itemToHide;
    public GameObject itemToShow;

    // Start is called before the first frame update
    void Start()
    {
        if (MenuNavigation.timesMenuLoaded > 0)
        {
           itemToHide.SetActive(false);
           itemToShow.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
