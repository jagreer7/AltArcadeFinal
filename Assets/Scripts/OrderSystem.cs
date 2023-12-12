using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class OrderSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> IngredientsList;
    [SerializeField] List<GameObject> OrderOptions;
    [SerializeField] private GameObject soup;
    [SerializeField] private string endScene;

    public PostProcessVolume volume;
    private Vignette vignette = null;

    AudioSource audioSource;
    public AudioClip error;
    public AudioClip complete;
    public AudioClip fireClip;
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
    private GameObject ord1action;
    private GameObject ord2action;
    private GameObject ord3action;

    [SerializeField] private GameObject Order1Box;
    [SerializeField] private GameObject Order2Box;
    [SerializeField] private GameObject Order3Box;
    [SerializeField] private GameObject Button1Box;
    [SerializeField] private GameObject Button2Box;
    [SerializeField] private GameObject Button3Box;
    [SerializeField] private GameObject Button4Box;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [SerializeField] private GameObject soupObject;
    [SerializeField] private GameObject witch;
    private bool fireBool = false;

    [SerializeField] private float StallDelay;
    private float timer = 0;

    [SerializeField] private int Score = 0;

    private int someValue = 0;

    private Color parchmentGreen = new Color32(255, 231, 76, 255);
    private Color parchmentRed = new Color32(147, 123, 95, 255);
    private Color parchmentFail = new Color32(255, 123, 95, 255);

    public float randomEventTimer;
    [SerializeField] private GameObject Fire;

    [SerializeField] private float countdownTimer = 120;
    [SerializeField] private TextMeshProUGUI TimerText;

    private bool stirred = false;
    [SerializeField] private GameObject Stir;

    private bool completePlayed = false;

    [SerializeField] private TextMeshProUGUI Action1;
    [SerializeField] private TextMeshProUGUI Action2;
    [SerializeField] private TextMeshProUGUI Action3;

    [SerializeField] private GameObject fireAttention;

    StirControls controls;
    private bool UpBool;
    private bool DownBool;
    private bool LeftBool;
    private bool RightBool;

    private bool UpBoolChecker = true;
    private bool DownBoolChecker = true;
    private bool LeftBoolChecker = true;
    private bool RightBoolChecker = true;

    [SerializeField] private int stirCount;
    [SerializeField] private int stirLimit = 12;

    [SerializeField] private GameObject Cauldron;
    private bool isNotStirrin = true;

    private void Awake()
    {
        controls = new StirControls();

        controls.Stir.Up.performed += ctx => UpBool = true;
        controls.Stir.Up.canceled += ctx => UpBool = false;

        controls.Stir.Down.performed += ctx => DownBool = true;
        controls.Stir.Down.canceled += ctx => DownBool = false;

        controls.Stir.Left.performed += ctx => LeftBool = true;
        controls.Stir.Left.canceled += ctx => LeftBool = false;

        controls.Stir.Right.performed += ctx => RightBool = true;
        controls.Stir.Right.canceled += ctx => RightBool = false;

        controls.Stir.Enable();
    }

    void Start()
    {
        //Set the initial score (for the UI)
        ScoreText.text = "SCORE: " + Score;
        audioSource = GetComponent<AudioSource>();

        //Random Event Timer
        randomEventTimer = Random.Range(30, 60);
        TimerText.text = "TOTAL TIME LEFT: " + Mathf.Floor(countdownTimer);

        volume.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        if (countdownTimer <= 1)
        {
            Debug.Log("Game Over");
            GameValues.score = Score;
            SceneManager.LoadScene(endScene);
        }
        else if (countdownTimer > 1)
        {
            countdownTimer -= Time.deltaTime;
            TimerText.text = "TOTAL TIME LEFT: " + Mathf.Floor(countdownTimer);
            if (randomEventTimer > 0) //Time for random event
            {
                randomEventTimer -= Time.deltaTime;
                if (orderCompleted == true) //Order is done and needs to switch
                {
                    //Randomize button items
                    /*
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
                    */

                    //Set Order
                    /*
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
                    */

                    int orderRandom1 = Random.Range(0, IngredientsList.Count); //Pick a random from the list of order options (the buttons that are available to the player)
                    Order1 = IngredientsList[orderRandom1]; //Set for the check later
                    ord1 = Instantiate(Order1, new Vector3(-7, -0.75f, 0), Quaternion.identity); //Spawn the object in the recipe boxes
                    Action1.text = ord1.GetComponent<Ingredient>().GetActionText(); //Set the action Text
                    ord1action = Instantiate(ord1.GetComponent<Ingredient>().GetAction(), new Vector3(-7, -3.438f, 0), Quaternion.identity); //Spawn the action in the action boxes
                    ord1.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0); //Scale

                    int orderRandom2 = Random.Range(0, IngredientsList.Count);
                    while (orderRandom2 == orderRandom1)
                    {
                        orderRandom2 = Random.Range(0, IngredientsList.Count);
                    }
                    Order2 = IngredientsList[orderRandom2];
                    ord2 = Instantiate(Order2, new Vector3(-5, -0.75f, 0), Quaternion.identity);
                    Action2.text = ord2.GetComponent<Ingredient>().GetActionText();
                    ord2action = Instantiate(ord2.GetComponent<Ingredient>().GetAction(), new Vector3(-5, -3.438f, 0), Quaternion.identity);
                    ord2.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

                    int orderRandom3 = Random.Range(0, IngredientsList.Count);
                    while (orderRandom3 == orderRandom1 || orderRandom3 == orderRandom2)
                    {
                        orderRandom3 = Random.Range(0, IngredientsList.Count);
                    }
                    Order3 = IngredientsList[orderRandom3];
                    ord3 = Instantiate(Order3, new Vector3(-3, -0.75f, 0), Quaternion.identity);
                    Action3.text = ord3.GetComponent<Ingredient>().GetActionText();
                    ord3action = Instantiate(ord3.GetComponent<Ingredient>().GetAction(), new Vector3(-3, -3.438f, 0), Quaternion.identity);
                    ord3.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);

                    // This code stops the order window prefabs from shooting by disabling their projectile script
                    //ord1.GetComponent<Projectile>().enabled = !(ord1.GetComponent<Projectile>().enabled);
                    //ord2.GetComponent<Projectile>().enabled = !(ord2.GetComponent<Projectile>().enabled);
                    //ord3.GetComponent<Projectile>().enabled = !(ord3.GetComponent<Projectile>().enabled);

                    orderCompleted = false;
                }
                else //Order is not yet done
                {
                    /*
                    Teeth = t
                    Frog = f
                    Toe = g (for gross)
                    Worms = m
                    Herbs = h
                    Eye = e
                    Mushroom = m

                    Fire = space
                    Stir = s
                    */
                    

                    //Check Keypresses
                    /*
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
                    */
                    if (Input.GetKeyDown(ord1.GetComponent<Ingredient>().GetKeyCode()))
                    {
                        Order1Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                        Order1Completed = true;
                        int rnd = Random.Range(0, 6);
                        Debug.Log("RANDOM: " + rnd);
                        GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                        ord1.GetComponent<Projectile>().Launch();
                    }
                    if (Input.GetKeyDown(ord2.GetComponent<Ingredient>().GetKeyCode()))
                    {
                        Order2Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                        Order2Completed = true;
                        int rnd = Random.Range(0, 6);
                        Debug.Log("RANDOM: " + rnd);
                        GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                        ord2.GetComponent<Projectile>().Launch();
                    }
                    if (Input.GetKeyDown(ord3.GetComponent<Ingredient>().GetKeyCode()))
                    {
                        Order3Box.GetComponent<SpriteRenderer>().color = parchmentGreen;
                        Order3Completed = true;
                        int rnd = Random.Range(0, 6);
                        Debug.Log("RANDOM: " + rnd);
                        GetComponent<AudioSource>().PlayOneShot(successSounds[rnd]);
                        ord3.GetComponent<Projectile>().Launch();
                    }


                    //Check if order is completed
                    
                    if (Order1Completed == true && Order2Completed == true && Order3Completed == true)
                    {

                        //Play an animation that shows that the user needs to stir
                        Stir.GetComponent<SpriteRenderer>().enabled = true;
                        Stir.GetComponent<Animator>().enabled = true;

                        //Stirring code
                        if ((UpBool == true || DownBool == true || LeftBool == true || RightBool == true) && isNotStirrin == true)
                        {
                            Cauldron.GetComponent<Animator>().SetBool("Stirrin", true);
                            isNotStirrin = false;
                        }

                        if (UpBool == true)
                        {
                            Debug.Log("up");
                            if (UpBoolChecker == true)
                            {
                                Debug.Log("upcheck");
                                UpBoolChecker = false;
                                DownBoolChecker = true;
                                stirCount++;
                            }
                        }
                        if (DownBool == true)
                        {
                            Debug.Log("down");
                            if (DownBoolChecker == true)
                            {
                                Debug.Log("downcheck");
                                UpBoolChecker = true;
                                DownBoolChecker = false;
                                stirCount++;
                            }
                        }
                        if (LeftBool == true)
                        {
                            Debug.Log("left");
                            if (LeftBoolChecker == true)
                            {
                                LeftBoolChecker = false;
                                RightBoolChecker = true;
                                stirCount++;
                            }
                        }
                        if (RightBool == true)
                        {
                            Debug.Log("right");
                            if (RightBoolChecker == true)
                            {
                                RightBoolChecker = false;
                                LeftBoolChecker = true;
                                stirCount++;
                            }
                        }
                        if (stirCount == stirLimit)
                        {
                            stirred = true;
                            if (!completePlayed) 
                            {
                                GetComponent<AudioSource>().PlayOneShot(complete);
                                completePlayed = true;
                            }
                          
                            
                            Debug.Log("Stalling");
                            witch.GetComponent<Animator>().SetTrigger("complete");
                        }

                        if (stirred == true)
                        {
                            timer += Time.deltaTime;
                            if (timer >= StallDelay)
                            {
                                soup.GetComponent<Soup>().RevertToStartColor();
                                OrderOptions.Clear();

                                //Destroy Objects
                                Destroy(ord1);
                                Destroy(ord2);
                                Destroy(ord3);
                                Destroy(ord1action);
                                Destroy(ord2action);
                                Destroy(ord3action);
                                //Destroy(ing1);
                                //Destroy(ing2);
                                //Destroy(ing3);
                                //Destroy(ing4);

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

                                //Reset Stirred
                                stirred = false;
                                Stir.GetComponent<SpriteRenderer>().enabled = false;
                                Stir.GetComponent<Animator>().enabled = false;

                                orderCompleted = true;

                                stirCount = 0;
                                isNotStirrin = true;
                                Cauldron.GetComponent<Animator>().SetBool("Stirrin", false);

                                Debug.Log("Done Stalling");
                                completePlayed = false;
                            }
                        
                        }
                    }
                }
            }
            else //When the Random Fire Event happens
            {
                Debug.Log("FIRE TIME");
                if (fireBool == false)
                {
                    GetComponent<AudioSource>().clip = fireClip;
                    GetComponent<AudioSource>().Play();
                    GetComponent<AudioSource>().loop = true;
                    fireBool = true;
                }
                Fire.GetComponent<SpriteRenderer>().enabled = true;
                Fire.GetComponent<Animator>().enabled = true;
                someValue++;

                fireAttention.GetComponent<SpriteRenderer>().enabled = true;

                vignette.intensity.value = 0.3f * Mathf.Sin(0.02f* (someValue + (1.5f * 3.14159f) - Time.deltaTime)) + 0.3f;

                //Player has to reset the fire timer
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    fireBool = false;
                    //Play the end of the fire animation
                    Fire.GetComponent<Animator>().SetBool("End", true);
                    StartCoroutine(WaitFire(0.6f));
                    GetComponent<AudioSource>().clip = null;
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().loop = false;

                    someValue = 0;
                    vignette.intensity.value = 0;

                    fireAttention.GetComponent<SpriteRenderer>().enabled = false;

                    //Resart the fire timer
                    randomEventTimer = Random.Range(30, 60);
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

    IEnumerator WaitFire(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire.GetComponent<Animator>().SetBool("End", false);
        Fire.GetComponent<SpriteRenderer>().enabled = false;
        Fire.GetComponent<Animator>().enabled = false;
    }
}