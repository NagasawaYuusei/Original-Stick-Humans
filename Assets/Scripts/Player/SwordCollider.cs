using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{

   [SerializeField] GameObject obj;
    private AnimatorStateInfo stateInfo;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Base Layer.Player_Sword1") || stateInfo.IsName("Base Layer.Player_Sword2") || stateInfo.IsName("Base Layer.Player_Sword3"))
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }

    }
}