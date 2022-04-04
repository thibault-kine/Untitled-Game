using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void SliderHandler()
    {
        Slider[] sliders = new Slider[3];
        
        sliders[0] = GameObject.Find("Mouse Slider").GetComponent<Slider>();
        sliders[0].value = 200f; // valeur par défaut de la sensibilité de la souris
        
        sliders[1] = GameObject.Find("Speed Slider").GetComponent<Slider>();
        sliders[1].value = 10f; // valeur par défaut de la vitesse du joueur
        
        sliders[2] = GameObject.Find("Jump Slider").GetComponent<Slider>();
        sliders[2].value = 3.5f; // valeur par défaut du saut du joueur
    }
}
