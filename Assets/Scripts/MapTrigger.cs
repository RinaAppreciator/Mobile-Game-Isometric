using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class MapTrigger : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("something entered trigger");
        Debug.Log(other.name);
        IsometricPlayerMovementController controller = other.GetComponent<IsometricPlayerMovementController>();

        if (controller != null)
        {
            Debug.Log("Player entered trigger");
            level1.SetActive(false);
            level2.SetActive(true);
            other.transform.position = new Vector3(0.140000001f, -3.55299997f, 0);
           
        }



    }

}
