using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    //bullet
    public GameObject prefab;
    public Transform shootPoint;
    public float bulletForce;
    private bool canShoot = true;
    private float timer;
    public float timeToShoot;
    //cannon
    public GameObject cannon;
    public float speed;

    void Update()
    {
        //determino la posicion del cursor
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        float hitDist = 25;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            //roto el cannon segun la posicion del cursor
            Quaternion cannonRotation = Quaternion.LookRotation(targetPoint - cannon.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, cannonRotation, speed * Time.deltaTime);
        }
        

        //disparar
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            canShoot = false;
            //instancio el bullet
            GameObject bullet = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            //disparo el bullet en la direccion del cannon
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(shootPoint.forward * bulletForce);
        }

        if (!canShoot)
        {
            //calculo el tiempo para poder disparar de nuevo
            timer = timer + Time.deltaTime;
            if (timer >= timeToShoot)
            {
                canShoot = true;
                timer = 0;
            }
        }
    }
}