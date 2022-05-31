using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Manager : MonoBehaviour
{
    internal Animator animator ;

    internal PJ_Character main;
    void Start(){ animator = GetComponent<Animator>();}
    public void Take_Put(){  animator.Play("Catch");}
    public void SetCut(bool isCutting){  animator.SetBool("cutting", isCutting);}
     public void SetWalk(bool isWalking){ animator.SetBool("moving", isWalking);}


    void OnAnimatorIK(int LayerIndex)
    {


        if(main.itenInHand !=null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand ,1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand ,1);

        }
        else
        {   
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand ,0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand ,0);
            
        }
        
        animator.SetIKPosition(AvatarIKGoal.LeftHand , main.itenPosition.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand , main.itenPosition.position);


    }



}
