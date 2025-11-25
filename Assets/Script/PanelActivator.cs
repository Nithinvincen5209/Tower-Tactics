using System.Collections;
using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    public  GameObject[] panels;
    public float delay = 1f; 
    void Start()
    {
        foreach(var panel in panels)
        {
            panel.SetActive(false);
        }

        StartCoroutine(ActivatePanels());
    }
    IEnumerator  ActivatePanels()
    {
        for( int   i=0; i< panels.Length; i++)
        {
           
            panels[i].SetActive(true);
            yield return new WaitForSeconds(delay);
            panels[i].SetActive(false);

        }
    }

    
}
