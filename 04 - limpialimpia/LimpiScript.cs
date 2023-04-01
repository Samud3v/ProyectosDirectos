using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpiScript : MonoBehaviour
{
    public Material material;
    public Texture2D maskTexture;
    public float radius = 0.05f;

    //private RaycastHit hit;
    //private Texture2D tempMaskTexture;

    void Start()
    {
        //tempMaskTexture = new Texture2D(maskTexture.width, maskTexture.height);
        //tempMaskTexture.SetPixels(maskTexture.GetPixels());
    }

    // void Update(){
    //     if(Input.GetMouseButton(0)){
    //         // dispara un rayo desde la camara hasta el punto donde se hizo click
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         Debug.DrawLine( Camera.main.transform.position, ray.direction * 100);
    //         if(Physics.Raycast(ray, out hit)){
    //             // obtiene la posicion del punto donde se hizo click en coordenadas de la textura
    //             Vector2 pixelUV = hit.textureCoord;
    //             pixelUV.x *= maskTexture.width;
    //             pixelUV.y *= maskTexture.height;
    //             int brushSize = (int)(radius * maskTexture.width);

    //             Debug.Log(pixelUV);
    //             // modifica la textura de la mascara
    //             for(int i = -brushSize; i <= brushSize; i++){
    //                 for(int j = -brushSize; j <= brushSize; j++){
    //                     if (i * i + j * j <= brushSize * brushSize) {
    //                         int x = Mathf.FloorToInt(pixelUV.x + i);
    //                         int y = Mathf.FloorToInt(pixelUV.y + j);
    //                         if (x >= 0 && x < maskTexture.width && y >= 0 && y < maskTexture.height) {
    //                                 //Color color = tempMaskTexture.GetPixel(x, y);
    //                                 Color color = new Color(1, 1, 1, 1);
    //                                 maskTexture.SetPixel(x, y, color);
    //                             }
    //                     }
    //                 }
    //             }

    //             // aplica los cambios a la textura
    //             maskTexture.Apply();

    //             // aplicar la textura de la mascara al material
    //             material.SetTexture("_Mask", maskTexture);
    //         }
    //     }
    // }

    void OnGUI(){
        GUI.DrawTexture(new Rect(0, Screen.height - 300, 300, 300), maskTexture );
    }

    void OnParticleCollision(GameObject other){
        if(other.GetComponent<ParticleSystem>() != null){
            List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
            other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);
            int numCollisions = collisionEvents.Count;
            if(numCollisions > 0){
                RaycastHit hit = new RaycastHit();
                Ray ray = new Ray( new Vector3(0,0,0), collisionEvents[0].intersection);
                if(Physics.Raycast(ray, out hit)){
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= maskTexture.width;
                    pixelUV.y *= maskTexture.height;
                    int brushSize = (int)(radius * maskTexture.width);

                    Debug.Log(pixelUV);
                    // modifica la textura de la mascara
                    for(int i = -brushSize; i <= brushSize; i++){
                        for(int j = -brushSize; j <= brushSize; j++){
                            if (i * i + j * j <= brushSize * brushSize) {
                                int x = Mathf.FloorToInt(pixelUV.x + i);
                                int y = Mathf.FloorToInt(pixelUV.y + j);
                                if (x >= 0 && x < maskTexture.width && y >= 0 && y < maskTexture.height) {
                                        Color color = new Color(1, 1, 1, 1);
                                        maskTexture.SetPixel(x, y, color);
                                    }
                            }
                        }
                    }

                    // aplica los cambios a la textura
                    maskTexture.Apply();

                    // aplicar la textura de la mascara al material
                    material.SetTexture("_Mask", maskTexture);
                }
            }
        }
    }
}
