using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  

    public float healthBarLength;

    // Use this for initialization
    void Start()
    {
        healthBarLength = Screen.width / 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), (int)GetComponent<EnemyBehaviour>().Health + "/" + (int)GetComponent<EnemyBehaviour>().BaseHealth);
    }

   
}
