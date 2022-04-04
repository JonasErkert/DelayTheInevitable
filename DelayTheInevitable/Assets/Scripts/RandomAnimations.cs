using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator roofPlateAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoofPlateAnimation());
    }

    IEnumerator RoofPlateAnimation()
    {
        yield return new WaitForSeconds(Random.Range(5, 120));
        roofPlateAnim.SetBool("isOpen", true);
    }

}
