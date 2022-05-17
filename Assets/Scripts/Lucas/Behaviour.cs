using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class IBehaviour : MonoBehaviour
{
    public enum BehaviourType { Calm, Impatient, Angry, Rage }
    public enum BehaviourState { WaitingForChair, Walk, Sit, WaitingForOrder, Order, WaitingFood, Eat, PayOrder, PayTip, StartExit, Rage, EndExit };
    public virtual void WaitingForChair()
    { }
    public virtual IEnumerator Walk()
    { yield break; }
    public virtual void Sit()
    { }
    public virtual IEnumerator WaitingForOrder() { yield break; }
    public virtual void Order() { }
    public virtual IEnumerator WaitingFood() { yield break; }
    public virtual IEnumerator Eat() { yield break; }
    public virtual IEnumerator PayOrder() { yield break; }
    public virtual IEnumerator PayTip() { yield break; }
    public virtual void StartExit() { }
    public virtual IEnumerator EndExit() 
    { yield break; }
    public virtual void InteractWithClients() { }
    public void Rage() { }
    




}
