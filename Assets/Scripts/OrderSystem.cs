using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class OrderSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> IngredientsList;
    [SerializeField] List<GameObject> OrderOptions;
    [SerializeField] private GameObject soup;

    AudioSource audioSource;
    public AudioClip error;
    public AudioClip[] successSounds;

    private bool orderCompleted = true;

    private GameObject ingredient1;
    private GameObject ingredient2;
    private GameObject ingredient3;
    private GameObject ingredient4;

    private GameObject Order1;
    private GameObject Order2;
    private GameObject Order3;
    private bool Order1Completed = false;
    private bool Order2Completed = false;
    private bool Order3Completed = false;

    private GameObject ing1;
    private GameObject ing2;
    private GameObject ing3;
    private GameObject ing4;
    private GameObject ord1;
    private GameObject ord2;
    private GameObject ord3;

    [SerializeField] private GameObject Order1Box;
    [SerializeField] private GameObject Order2Box;
    [SerializeField] private GameObject Order3Box;
    [SerializeField] private GameObject Button1Box;
    [SerializeField] private GameObject Button2Box;
    [SerializeField] private GameObject Button3Box;
    [SerializeField] private GameObject Button4Box;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [SerializeField] private GameObject soupObject;

    [SerializeField] private float StallDelay;
    private float timer = 0;

    private int Score = 0;

    private Color parchmentGreen = new Color32(255, 231, 76, 255);
    private Color parchmentRed = new Color32(147, 123, 95, 255);
    private Color parchmentFail = new Color32(255, 123, 95, 255);

    void Start()
    {
        //Set the initial score (for the UI)
        ScoreText.text = "SCORE: " + Score;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (orderCompleted == true) //Order is done and needs to switch
        {
            //Randomize button items
            int random1 = Random.Range(0, IngredientsList.Count); //Pick a random index 
            ing1 = Instantiate(IngredientsList[random1], new Vector3(-6, -4f, 0), Quaternion.identity); //Create the gameobject
            OrderOptions.Add(ing1); //Add it to the list of option for the recipe

            int random2 = Random.Range(0, IngredientsList.Count);
            while (random2 == random1) //Make sure the random is different from the others
            {
                random2 = Random.Range(0, IngredientsList.Count);
            }
            ing2 = Instantiate(IngredientsList[random2], new Vector3(-2, -4f, 0), Quaternion.identity);
            OrderOptions.Add(ing2);

            int random3 = Random.Range(0, IngredientsList.Count);
            while (random3 == random1 || random3 == random2)
            {
                random3 = Random.Range(0, IngredientsList.Count);
            }
            ing3 = Instantiate(IngredientsList[random3], new Vector3(2, -4f, 0), Quaternion.identity);
            OrderOptions.Add(ing3);

            int random4 = Random.Range(0, IngredientsList.Count);
            while (random4 == random1 || random4 == random2 || random4 == random3)
            {
                random4 = Random.Range(0, IngredientsList.Count);
            }
            ing4 = Instantiate(IngredientsList[random4], new Vector3(6, -4f, 0), Quaternion.identity);
            OrderOptions.Add(ing4);

            //Set Order
            int orderRandom1 = Random.Range(0, OrderOptions.Count); //Pick a random from the list of order options (the buttons that are available to the player)
            Order1 = OrderOptions[orderRandom1]; //Set for the check later
            ord1 = Instantiate(Order1, new Vector3(-6, 1.2f, 0), Quaternion.identity); //Spawn the object in the recipe boxes
            ord1.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0); //Scale

            int orderRandom2 = Random.Range(0, OrderOptions.Count);
            while (orderRandom2 == orderRandom1)
            {
                orderRandom2 = Random.Range(0, OrderOptions.Count);
            }
            Order2 = OrderOptions[orderRandom2];
            ord2 = Instantiate(Order2, new Vector3(-4, 1.2f, 0), Quaternion.identity);
            ord2.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

            int orderRandom3 = Random.Range(0, OrderOptions.Count);
            while (orderRandom3 == orderRandom1 || orderRandom3 == orderRandom2)
            {
                orderRandom3 = Random.Range(0, OrderOptions.Count);
            }
            Order3 = OrderOptions[orderRandom3];
            ord3 = Instantiate(Order3, new Vector3(-2, 1.2f, 0), Quaternion.identity);
            ord3.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

            // This code stops the order window prefabs from shooting by disabling their projectile script
            ord1.GetComponent<Projectile>().enabled = !(ord1.GetComponent<Projectile>().enabled);
            ord2.GetComponent<Projectile>().enabled = !(ord2.GetComponent<Projectile>().enabled);
            ord3.GetComponent<Projectile>().enabled = !(ord3.GetComponent<Projectile>().enabled);

            orderCompleted = false;
        }
        else //Order is not yet done
        {
            // OrderOptions[0] = 'D'
            // OrderOptions[1] = 'F'
            // OrderOptions[2] = 'J'
            // OrderOptions[3] = 'K'

            //Check Keypresses
            if (Input.GetKeyDown("d"))
            {
                if (OrderOptions[0] == Order1 && Order1Completed == false)
                {
                    Order1Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order1Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[0].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[0] == Order2 && Order2Completed == false)
                {
                    Order2Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order2Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[0].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[0] == Order3 && Order3Completed == false)
                {
                    Order3Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order3Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[0].GetComponent<Projectile>().Launch();
                }
                else
                {
                    Button1Box.GetComponent<SpriteRenderer>().color = parchmentFail;
                    audioSource.PlayOneShot(error, 0.6F);
                    StartCoroutine(Wait(0.6f));
                }
            }
            else if (Input.GetKeyDown("f"))
            {
                if (OrderOptions[1] == Order1 && Order1Completed == false)
                {
                    Order1Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order1Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[1].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[1] == Order2 && Order2Completed == false)
                {
                    Order2Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order2Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[1].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[1] == Order3 && Order3Completed == false)
                {
                    Order3Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order3Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[1].GetComponent<Projectile>().Launch();
                }
                else
                {
                    Button2Box.GetComponent<SpriteRenderer>().color = parchmentFail;
                    audioSource.PlayOneShot(error, 0.6F);
                    StartCoroutine(Wait(0.6f));
                }
            }
            else if (Input.GetKeyDown("j"))
            {
                if (OrderOptions[2] == Order1 && Order1Completed == false)
                {
                    Order1Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order1Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[2].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[2] == Order2 && Order2Completed == false)
                {
                    Order2Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order2Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[2].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[2] == Order3 && Order3Completed == false)
                {
                    Order3Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order3Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[2].GetComponent<Projectile>().Launch();
                }
                else
                {
                    Button3Box.GetComponent<SpriteRenderer>().color = parchmentFail;
                    audioSource.PlayOneShot(error, 0.6F);
                    StartCoroutine(Wait(0.6f));
                }
            }
            else if (Input.GetKeyDown("k"))
            {
                if (OrderOptions[3] == Order1 && Order1Completed == false)
                {
                    Order1Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order1Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[3].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[3] == Order2 && Order2Completed == false)
                {
                    Order2Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order2Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[3].GetComponent<Projectile>().Launch();
                }
                else if (OrderOptions[3] == Order3 && Order3Completed == false)
                {
                    Order3Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                    Order3Completed = true;
                    int rnd = Random.Range(0, 6);
                    Debug.Log("RANDOM: " + rnd);
                    GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                    OrderOptions[3].GetComponent<Projectile>().Launch();
                }
                else
                {
                    Button4Box.GetComponent<SpriteRenderer>().color = parchmentFail;
                    audioSource.PlayOneShot(error, 0.6F);
                    StartCoroutine(Wait(0.6f));
                }
            }

            //Check if order is completed
            if (Order1Completed == true && Order2Completed == true && Order3Completed == true)
            {
                Debug.Log("Stalling");

                timer += Time.deltaTime;

                if (timer >= StallDelay)
                {
                    soup.GetComponent<Soup>().RevertToStartColor();
                    OrderOptions.Clear();

                    //Destroy Objects
                    Destroy(ord1);
                    Destroy(ord2);
                    Destroy(ord3);
                    Destroy(ing1);
                    Destroy(ing2);
                    Destroy(ing3);
                    Destroy(ing4);

                    //Reset OrderBox Colors
                    Order1Box.GetComponent<SpriteRenderer>().color = parchmentRed;
                    Order2Box.GetComponent<SpriteRenderer>().color = parchmentRed;
                    Order3Box.GetComponent<SpriteRenderer>().color = parchmentRed;

                    //Reset order completion
                    Order1Completed = false;
                    Order2Completed = false;
                    Order3Completed = false;

                    //Add to score
                    Score += 100;
                    ScoreText.text = "SCORE: " + Score;

                    //Reset the timer
                    timer = 0;

                    orderCompleted = true;
                }
            }
        }
    }

    IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        Button1Box.GetComponent<SpriteRenderer>().color = parchmentRed;
        Button2Box.GetComponent<SpriteRenderer>().color = parchmentRed;
        Button3Box.GetComponent<SpriteRenderer>().color = parchmentRed;
        Button4Box.GetComponent<SpriteRenderer>().color = parchmentRed;
    }
}