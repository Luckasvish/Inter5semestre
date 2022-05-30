using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Manager : MonoBehaviour
{
    internal Animator animator ;

    void Start(){ animator = GetComponentInChildren<Animator>();}
    public void Take_Put(){  animator.Play("Catch");}
    public void SetCut(bool isCutting){  animator.SetBool("cutting", isCutting);}
     public void SetWalk(bool isWalking){ animator.SetBool("moving", isWalking);}

}
