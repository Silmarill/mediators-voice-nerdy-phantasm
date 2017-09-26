using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour
{
    private bool buttonIsClicked;


    // Use this for initialization
    void Start()
    {
        buttonIsClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void onClickListener() {
        buttonIsClicked = !buttonIsClicked;
        if (buttonIsClicked) Messenger.Broadcast("GameisResumed");
        else Messenger.Broadcast("GameisStopped");
    }

    }

