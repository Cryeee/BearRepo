using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIFoodsEaten : MonoBehaviour
{
    public enum Foods
    {
        Blueberry,
        Salmon, 
        Honey
    }

    // How many foods are visible at the same currentTime
    public Color[] slots;

    // Image slot (Set imageRef's sprite)
    public GameObject imageRef;

    // Time for image to move up
    public float animationTime = 2;

    // Current image where sprite is assigned
    private Transform tempImage;

    private float animationTimer;

    // How much image moves on y-axis
    private float animationDistance;

    // Starts moving image
    private bool doAnimation;

    private int childIndex;

    // Top image that fades away
    private Image fadeOutImage;

    private bool addImage;

    // Start is called before the first frame update
    void Start()
    {
        // Keeps track in which position image is on canvas
        childIndex = slots.Length * -1;
        animationDistance = imageRef.transform.position.y - transform.position.y;

        //Instantiates empty slots 
        for (int i = 0; i < slots.Length; i++)
        {
            Instantiate(imageRef, transform).SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    // Get every slot
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
        //        tempImage = transform.GetChild(i);

        //        //If slot is empty (not active), set correct sprite and start animating on y-axis
        //        if (!tempImage.gameObject.activeSelf)
        //        {
        //            tempImage.position = transform.position;
        //            //TODO: ASSIGN CURRENT SPRITE TO IMAGE:
        //            tempImage.GetComponent<Image>().color = slots[Random.Range(0, slots.Length)];
        //            tempImage.gameObject.SetActive(true);
        //            animationTimer = 0;
        //            childIndex++;

        //            // Set image's index back to 0 when it fades away:
        //            if (childIndex >= slots.Length)
        //            {
        //                childIndex = 0;
        //            }

        //            doAnimation = true;
        //            break;
        //        }
        //    }
        //}

        // Animate image to move one slot up:
        if (doAnimation && animationTimer < animationTime)
        {
            
            animationTimer += Time.deltaTime;

			#region Top slot
			if (animationTimer >= animationTime)
            {
                // positio fiksi
                for (int i = 0; i < slots.Length; i++)
                {
                    Transform child = transform.GetChild(i);
                    float actualDistance = Mathf.Abs(transform.GetChild(i).position.y - transform.position.y);
                    float factor = Mathf.Round(actualDistance / animationDistance);
                    child.position = new Vector3(child.position.x, transform.position.y + factor * animationDistance, child.position.z);
                }
                doAnimation = false;
                fadeOutImage.gameObject.SetActive(false);
                // if we have actual color, chnage this:
                fadeOutImage.color = new Color(1, 1, 1, 1);
            }
			#endregion

			#region BottomSlots
			for (int i = 0; i < transform.childCount; i++)
            {
                // move all active children up
                tempImage = transform.GetChild(i);
                tempImage.position += new Vector3(0, animationDistance * Time.deltaTime / animationTime, 0);
                if (i == childIndex)
                {
                    fadeOutImage = tempImage.GetComponent<Image>();
                    Color oldColor = fadeOutImage.color;

                    // changes alpha:
                    fadeOutImage.color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - Time.deltaTime / animationTime);
                }
            }
			#endregion
		}
	}

    public void DisplayFoodItem(Sprite icon)
    {
        // Get every slot
        for (int i = 0; i < transform.childCount; i++)
        {
            tempImage = transform.GetChild(i);

            //If slot is empty (not active), set correct sprite and start animating on y-axis
            if (!tempImage.gameObject.activeSelf)
            {
                tempImage.position = transform.position;
                //TODO: ASSIGN CURRENT SPRITE TO IMAGE:
                //tempImage.GetComponent<Image>().color = slots[Random.Range(0, slots.Length)];
                tempImage.GetComponent<Image>().sprite = icon;
                tempImage.gameObject.SetActive(true);
                animationTimer = 0;
                childIndex++;

                // Set image's index back to 0 when it fades away:
                if (childIndex >= slots.Length)
                {
                    childIndex = 0;
                }

                doAnimation = true;
                break;
            }
        }
    }
}
