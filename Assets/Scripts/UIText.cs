using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class UIText : MonoBehaviour
    {
        [SerializeField] private PlaySFXEvent _playSFXEvent;
        [SerializeField] private AudioClip _UITextSound;
       public void PlaySound() => _playSFXEvent.RaiseSFXEvent(_UITextSound);
    }
}
