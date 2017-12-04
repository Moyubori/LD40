using UnityEngine;
using System.Collections;

public class pixelate : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    
    public int horizontal,vertical;
    void Start()
    {
        QualitySettings.antiAliasing = 0;
    }


    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture scaled = RenderTexture.GetTemporary(horizontal,vertical);
        src.filterMode = FilterMode.Point;
        scaled.filterMode = FilterMode.Point;
        Graphics.Blit(src, scaled);
        Graphics.Blit(scaled, dest);
        RenderTexture.ReleaseTemporary(scaled);
    }
    
        void Update()
    {
       /* ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            print(hit.collider.name);
        }*/
    }
}