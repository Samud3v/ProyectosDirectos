using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bottleController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TMPro.TMP_Text text;

    void Start()
    {
        canvas.enabled = false;
    }

    public void UpdateInfo(string _text, Vector3 _position){
        // Update the bottle's info
        text.text = _text;
        this.transform.position = _position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas.enabled = false;
        }
    }
}
