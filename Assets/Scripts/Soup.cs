using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soup : MonoBehaviour
{
    private Color startColor;
    public Animator animator;
    public GameObject splashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        startColor = this.GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
    }

    private IEnumerator ChangeColor(Color changeColor)
    {
        float tick = 0f;
        float speed = 1.5f;
        Color currentColor = this.GetComponent<SpriteRenderer>().color;
        changeColor.r = (currentColor.r + changeColor.r) / 2f;
        changeColor.g = (currentColor.g + changeColor.g) / 2f;
        changeColor.b = (currentColor.b + changeColor.b) / 2f;
        Debug.Log("Change Color " + changeColor);
        Splash(currentColor);
        while (this.GetComponent<SpriteRenderer>().color != changeColor)
        {
            tick += Time.deltaTime * speed;
            
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, changeColor, tick);
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collision(Color color)
    {
        StartCoroutine(ChangeColor(color));
        
        //this.GetComponent<SpriteRenderer>().color = color;
    }

    public void Splash(Color color)
    {
        GameObject splash = Instantiate(splashPrefab, this.transform);
        splash.transform.position = new Vector3(3.90f, -2.2f, 0.0f);
        splash.GetComponent<SpriteRenderer>().color = color;
        Destroy(splash, 1.125f);
        //animator.SetTrigger("Splash");
    }

    public void RevertToStartColor()
    {
        this.GetComponent<SpriteRenderer>().color = startColor;
    }
}
