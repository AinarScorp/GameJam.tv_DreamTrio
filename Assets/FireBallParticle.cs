using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallParticle : MonoBehaviour
{
    [SerializeField] FireBall fireBall;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AssignDirection(Vector3 direction)
    {
        this.direction = direction;
        //Debug.Log(direction);
        //Vector3 target_position = new Vector3(direction.x,
        //                                                direction.y,
        //                                                transform.position.z);
        //transform.LookAt(target_position);
    }
}
