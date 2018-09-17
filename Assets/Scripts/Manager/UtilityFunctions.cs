using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions : MonoBehaviour {
    
    public List<GameObject> actors;


    private void Awake()
    {
    }

    public GameObject FindObjectwithTag(Transform parent, string _tag)
    {
        actors = new List<GameObject>();
        return GetChildObject(parent, _tag);
    }

    public GameObject GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                actors.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
        return actors[0];
    }

}
