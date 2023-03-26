using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Canvas canvas;
    public SUPERCharacter.SUPERCharacterAIO player;

    void Start()
    {
        canvas.enabled = false;
        player.enabled = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            canvas.enabled = !canvas.enabled;
            player.enabled = !player.enabled;
        }
    }
}
