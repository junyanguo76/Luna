using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject Star;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StarGo()
    {
        Instantiate(Star,new Vector3(-7.35f,-5f,0),Quaternion.identity);
    }
}
