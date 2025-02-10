using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDepth : MonoBehaviour
{
    private List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    private List<int> sortingOrder = new List<int>();
    public void Init()
    {
        renderers.AddRange(
            GetComponentsInChildren<SpriteRenderer>(true) );

        for (int i = 0; i < renderers.Count; ++i)
            sortingOrder.Add(renderers[i].sortingOrder);
    }
    public void SetSortingOrder( int order )
    {
        for( int i = 0; i< renderers.Count; ++ i )
        {
            renderers[i].sortingOrder = order + sortingOrder[i];
        }
    }

    
}
