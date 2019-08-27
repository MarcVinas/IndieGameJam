using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    //proyectil
    public GameObject prefab;
    public Transform shootPoint;
    public float bulletForce;
    public GameObject cannon;

    public float speed;

    //rotacion del cañon
    private float angle;

    void Update()
    {
        //determino la posicion del cursor
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        float hitDist = 25;

        if (Physics.Raycast(ray, out hit))
        {
            //Transform objectHit = hit.transform;
            //Vector3 objectVec = hit.transform.position;
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion cannonRotation = Quaternion.LookRotation(targetPoint - cannon.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, cannonRotation, speed * Time.deltaTime);
        }

        //el cañon rota segun la posicion del cursor
        //angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
        

        //disparar
        if (Input.GetButtonDown("Fire1"))
        {
            //instancio el proyectil
            GameObject bullet = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            //disparo el proyectil en la direccion del cañon
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(shootPoint.forward * bulletForce);
        }
    }
}