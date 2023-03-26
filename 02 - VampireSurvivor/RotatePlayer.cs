using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    Vector2 input;
    float angle = 0;
    float rotationSpeed = 10f;

    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = input.normalized;

        if (input != Vector2.zero)
        {
            angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0,0, -angle)), Time.deltaTime * rotationSpeed);
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }
        else{
            foreach (ParticleSystem particle in particles)
            {
                particle.Stop();
            }
        }
    }
}
