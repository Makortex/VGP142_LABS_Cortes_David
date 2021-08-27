using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject Bonk;
    public GameObject Blood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ActiveBlood()
    {
        Blood.SetActive(true);
    }
    void DesactiveBlood()
    {
        Blood.SetActive(false);
    }
    void ActiveBonk()
    {
        Bonk.SetActive(true);

    }
    void DesactiveBonk()
    {
        Bonk.SetActive(false);

    }
}
