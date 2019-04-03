using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour {

    private static WeaponManager instance;
    public static WeaponManager weaponScript
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(WeaponManager)) as WeaponManager;
            }
            return instance;
        }
    }

    [SerializeField]
    public List<GameObject> weapon = new List<GameObject>();

    public Weapon handWeapon;

    Dictionary<int, Action<int>> inputKey;

    void Awake()
    {
        inputKey = new Dictionary<int, Action<int>>()
        {
            {1, SwapWeapon},
            {2, SwapWeapon},
            {3, SwapWeapon},
            {4, SwapWeapon}
        };

    }


    void Start () {

        SwapWeapon(0);

    }

    // Update is called once per frame
    void Update () {

        if (Input.anyKeyDown)
        {
            foreach (var dic in inputKey)
            {
                if (Input.GetKeyDown(dic.Key.ToString()))
                {
                    dic.Value(dic.Key-1);
                }
            }
        }

    }

    void SwapWeapon(int select)
    {
        handWeapon = weapon[select].GetComponent<Weapon>();

        for (int i = 0; i < weapon.Count; i++)
        {
            if(i== select)
                weapon[i].SetActive(true);
            else
                weapon[i].SetActive(false);
        }
    }

}
