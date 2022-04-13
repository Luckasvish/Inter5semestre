using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBehaviour
{
    public enum BehaviourType { Calm, Impatient, Angry, Rage }

    //public IEnumerator BehaviourManager();
    public enum BehaviourState { Walk, Sit, WaitingForOrder, Order, WaitingFood, Eat, PayOrder, PayTip, Exit };
    public void Walk(Action<bool> callback);
    public void Sit(Action<bool> callback);
    public IEnumerator WaitingForOrder(Action<bool> callback);
    public void Order(Action<bool> callback);
    //public void ChangeOrder(Action<bool> callback);
    public IEnumerator InteractWithClients(Action<bool> callback);
    public IEnumerator WaitingFood(Action<bool> callback);
    public IEnumerator Eat(Action<bool> callback);
    public IEnumerator PayOrder(Action<bool> callback);
    public IEnumerator PayTip(Action<bool> callback);
    public void StartExit(Action<bool> callback);
    public IEnumerator EndExit(Action<bool> callback);
    public void Rage(Action<bool> callback);
    




}
