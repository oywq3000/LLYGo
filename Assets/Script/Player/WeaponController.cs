using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


interface IWeapon
{
    float Cd { get; }
    void Play();
}
public class WeaponController : MonoBehaviour
{

    public Transform playerPalm;

    private Animator _animator;
    
    //play attack for specified weapon
    private IWeapon _weapon;
    
    //private struct variable
    private bool _canAttack = true;
    private int _index; //index for selecting weapon

    private void Start()
    {
        //get weapon interface
        _weapon = playerPalm.GetChild(0).GetComponent<IWeapon>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //left mouse is default for normal attack
        if (Input.GetKey(KeyCode.Mouse0)&&_canAttack)
        {
            //play
            _weapon.Play();
            
            //animator play
            _animator.SetTrigger("Attack");
            //enter cd
            RecoverCd();
        }
        
        //switch weapon
        var axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis!=0)
        {
            if (axis>0.1)
            {
                _index++;
            }
            else if (axis<-0.1)
            {
                _index--;
            }
        }
        Debug.Log("Index:"+_index);
      
    }

    void Switch()
    {
        
    }
    

    async void RecoverCd()
    {
        _canAttack = false;
        await UniTask.Delay(TimeSpan.FromSeconds(_weapon.Cd));
        _canAttack = true;
    }
}