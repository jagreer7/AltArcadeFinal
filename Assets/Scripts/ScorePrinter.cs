using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class ScorePrinter : MonoBehaviour
{
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Camera camera;
    // Update is called once per frame
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = "" + GameValues.score;
        StartCoroutine(ChangeColorToOne());
    }

    void Update()
    {
 
    }

    private IEnumerator ChangeColorToOne()
    {
        float tick = 0f;
        float speed = 0.2f;
        Color currentColor = color1;
        Color changeColor = color2;
        Debug.Log("Change Color " + changeColor);
        while (camera.backgroundColor != changeColor)
        {
            tick += Time.deltaTime * speed;

            camera.backgroundColor = Color.Lerp(currentColor, changeColor, tick);
            yield return null;
        }
        StartCoroutine(ChangeColorToTwo());
    }

    private IEnumerator ChangeColorToTwo()
    {
        float tick = 0f;
        float speed = 0.2f;
        Color currentColor = color2;
        Color changeColor = color1;
        Debug.Log("Change Color " + changeColor);
        while (camera.backgroundColor != changeColor)
        {
            tick += Time.deltaTime * speed;

            camera.backgroundColor = Color.Lerp(currentColor, changeColor, tick);
            yield return null;
        }
        StartCoroutine(ChangeColorToOne());
    }
}
