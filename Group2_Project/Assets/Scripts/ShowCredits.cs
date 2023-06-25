using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    [SerializeField]
    private GameObject credits;
      
    public void showCredits() {
        credits.SetActive(true);

    }
    public void hideCredits() {
        credits?.SetActive(false);

    }
}
