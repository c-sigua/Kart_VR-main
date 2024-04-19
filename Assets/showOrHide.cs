using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class showOrHide : MonoBehaviour
{
    [SerializeField] private GameObject itemToHide;
    [SerializeField] private GameObject itemToShow;

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
