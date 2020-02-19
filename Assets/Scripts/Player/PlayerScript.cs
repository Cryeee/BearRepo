using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float AmountOfFoodEaten;

    public Animator playerAnimator;

    public AnimationClip fatteningAnimation;

    float sizeIncrease = 0;


    public Vector3 PlayerScaleSize;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        //fatteningAnimation = playerAnimator.
    }

    // Update is called once per frame
    void Update()
    {
        sizeIncrease = AmountOfFoodEaten / 100;

        playerAnimator.Play("Fattening", 0, sizeIncrease);

        //PlayerScaleSize.Set(1 + AmountOfFoodEaten/10, 1 + AmountOfFoodEaten/10, 1 + AmountOfFoodEaten/10);
        //transform.localScale = PlayerScaleSize;


        if (Input.GetKeyDown(KeyCode.Q)) {
            AmountOfFoodEaten++;
            //fatteningAnimation.time = 5;
        }

        //transform.rotation.y = 
        //transform.rotation.y
    }
    
}
