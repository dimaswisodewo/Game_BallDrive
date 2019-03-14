using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{

    public Transform player;
    public Transform shotPos;
    public Rigidbody bullet;
    private float shotForce = 1000f;
    private float time = 3.0f;

    private void Update()
    {
        transform.LookAt(player.position);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Shot();     
            time = 3.0f;
        }
    }

    private void Shot()
    {
        Rigidbody shot = Instantiate(bullet, shotPos.position, shotPos.rotation) as Rigidbody;
        shot.AddForce(shotPos.forward * shotForce);
    }

}
