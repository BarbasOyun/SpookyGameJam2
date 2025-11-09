using UnityEngine;
using UnityEngine.Serialization;

public class PNJ : MonoBehaviour
{
    public static Animator animator;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator > ();
    }

    public static void UpdateAnimation(int i)
    {
        animator.SetInteger("FearLevel", i);
    }
}
