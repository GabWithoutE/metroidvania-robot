using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnhideMinimapSection : MonoBehaviour
{
    private bool visited;
    private Image image;
    public Sprite hidden;
    public Sprite uncovered;

    // Use this for initialization
    void Start()
    {
        //Testing purposes, default to not visited
        visited = false;
        image = GetComponent<Image>();
    }

    public void SetVisited(bool visitedIn)
    {
        visited = visitedIn;
        if (visitedIn)
        {
            image.sprite = uncovered;
        }
        else
        {
            image.sprite = hidden;
        }
    }
}