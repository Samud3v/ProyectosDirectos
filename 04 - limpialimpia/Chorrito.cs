using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chorrito : MonoBehaviour
{
    private ParticleSystem ps;
    private RaycastHit hit;
    [SerializeField] 
    private float offset = 0.5f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var tmp = ps.emission; 
        if (Input.GetMouseButton(0)) {          
            tmp.enabled = true; 
            // Disparar un raycast y obtener la información de colisión
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // mostrar el rayo que se dispara desde la cámara
            Debug.DrawLine( Camera.main.transform.position, ray.direction * 100);
            if (Physics.Raycast(ray, out hit)) {
                // girar el objeto a la dirección del rayo
                transform.LookAt(hit.point + new Vector3(0, offset, 0));
            }
        }
        else{
            tmp.enabled = false; 
        }
    }
}
