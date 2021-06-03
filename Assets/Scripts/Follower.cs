using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 Offset;
    public bool OkCollidedd =false;
    // Start is called before the first frame update
    void Start()
    {
        OkCollidedd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OkCollidedd == true)
        {
            transform.position = player.transform.position + Offset;
            transform.rotation = Quaternion.identity;
            return;
           
        }
        transform.position = player.transform.position + Offset;
        transform.rotation = player.transform.rotation;
    }
}
