using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    public Color hoverColor;

    public GameObject particleModel;

    private AudioSource audioSource;
    private bool isOverMouse;
    public Rigidbody rbd;

    private Node parentNode;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        audioSource = GetComponent<AudioSource>();
        rbd = GetComponent<Rigidbody>();
        FindObjectOfType<GameManager>().rbds.Add(rbd);
    }

    private void Update()
    {
        //if (FindObjectOfType<GameManager>().selectedWall != this && !isOverMouse) rend.material.color = startColor;
    }

    public void ChangeColor()
    {
        rend.material.color = startColor;
    }

    private void OnMouseUpAsButton()
    {
        if (FindObjectOfType<GameManager>().selectedWall != null)
        {
            FindObjectOfType<GameManager>().selectedWall.ColorReset();
        }
        ColorChange();
        FindObjectOfType<GameManager>().selectedWall = this;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
        isOverMouse = true;
    }

    private void OnMouseExit()
    {
        if (FindObjectOfType<GameManager>().selectedWall != this) rend.material.color = startColor;
        isOverMouse = false;
    }

    private void ColorChange()
    {
        rend.material.color = hoverColor;
    }

    public void ColorReset()
    {
        rend.material.color = startColor;
    }

    public void DestroyWall()
    {
        PlayParticle();
        audioSource.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (gameObject.transform.rotation.y >= 0.65) parentNode.SetWalls(1, 0);
        else parentNode.SetWalls(0, 0);
        Invoke("InvokeDestroy", 1f);
    }

    private void PlayParticle()
    {
        Instantiate(particleModel, transform.position, Quaternion.identity);
    }

    private void InvokeDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Node")
        {
            parentNode = other.GetComponent<Node>();
        }
    }
}
