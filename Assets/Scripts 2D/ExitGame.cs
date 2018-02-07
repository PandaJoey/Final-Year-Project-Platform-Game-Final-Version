using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * A simple Exit Game Class
 */

public class ExitGame : MonoBehaviour  {

    public void ClickToExit() {
        Application.Quit();
    }
}