using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Color hoverColor;
    public Color dontMoveColor;

    public GameObject wallModel;

    private Renderer rend;
    private Color startColor;

    /// <summary>
    /// 
    /// </summary>

    private List<int> walls;
    private List<bool> isCreated;

    private bool isOverMouse;
    public Rigidbody rbd;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        FindObjectOfType<GameManager>().rbds.Add(rbd);

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        /// <summary>
        /// 
        /// </summary>
        /// 

        walls = new List<int>();
        isCreated = new List<bool>();

        for (int i =0; i<2; i++)
        {
            walls.Add(0); // 동북
            isCreated.Add(false); // 벽이 세워져 있는가
        }

        int random = Random.Range(0, 3);
        //int random = 2;

        if (random == 0 && transform.position.x < 0)
        {
            BuildWall(0);
        }
        else if (random == 1 && transform.position.z <= 11)
        {
            BuildWall(1);
        }
        else if (random == 2)
        {
            if (transform.position.x < 0) BuildWall(0);
            if (transform.position.z <= 11) BuildWall(1);
        }


    }

    public void ChangeColor()
    {
        rend.material.color = startColor;
    }

    private void OnMouseEnter() {
        rend.material.color = hoverColor;
        isOverMouse = true;
    }

    private void OnMouseExit() {
        if (FindObjectOfType<GameManager>().pressedButton != this) rend.material.color = startColor;
        isOverMouse = false;
    }

    private void OnMouseUpAsButton()
    {
        if(FindObjectOfType<GameManager>().pressedButton != null)
        {
            Debug.Log("hi");
            FindObjectOfType<GameManager>().pressedButton.ColorReset();
        }
        ColorChange();
        FindObjectOfType<GameManager>().pressedButton = this;
    }

    private bool IsBuildWall(int direction)
    {
        if (isCreated[direction] == true) return true;
        else return false;
    }

    private void ColorChange()
    {
        rend.material.color = hoverColor;
    }
    
    public void ColorReset()
    {
        rend.material.color = startColor;
    }

    public void BuildWall(int wallDirection)
    {
        if (IsBuildWall(wallDirection)) return;
        else
        {
            if (wallDirection == 0)
            {
                Instantiate(wallModel, transform.position + new Vector3(0.5f,0.3f,0f), Quaternion.identity);
            }
            else
            {
                Instantiate(wallModel, transform.position + new Vector3(0f, 0.3f, 0.5f), Quaternion.Euler(new Vector3(0,90,0)));
            }
        }
    }

    public List<int> GetWalls()
    {
        return walls;
    }

    public void SetWalls(int index, int val)
    {
        walls[index] = val;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (other.gameObject.transform.position.x - transform.position.x >= 0.5)
            {
                walls[0] = 1;
            }

            if (other.gameObject.transform.position.z - transform.position.z >= 0.5)
            {
                walls[1] = 1;
            }
        }
    }

}

public enum Direction
{
    EAST = 0,
    NORTH = 1
}


