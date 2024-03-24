using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHelper : MonoBehaviour
{

    public PlayerControls Input;

    public static ControllerHelper Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        Input = new PlayerControls();
        Input.Enable();
    }


}
