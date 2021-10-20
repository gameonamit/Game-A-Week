using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.01f, 0.3f)] float dotMinScale;
    [SerializeField] [Range(0.05f, 0.20f)] float dotMaxScale;

    [SerializeField] LayerMask groundLayer;

    Transform[] dotsList;
    int collidedDot;

    Vector2 pos;
    Vector2 newPos;

    float timeStamp;

    private void Start()
    {
        Hide();

        PrepareDots();

        collidedDot = dotsNumber;
    }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;

        for(int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
    }

    public void UpdateDots(Vector2 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) -
                (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList[i].position = pos;

            timeStamp += dotSpacing;
        }
    }

    //public void UpdateDots(Vector2 ballPos, Vector2 forceApplied)
    //{
    //    timeStamp = dotSpacing;
    //    Debug.Log(forceApplied);
    //    for (int i = 0; i < dotsNumber; i++)
    //    {
    //        pos.x = (ballPos.x + forceApplied.x * timeStamp);
    //        pos.y = (ballPos.y + forceApplied.y * timeStamp) -
    //            (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

    //        bool isColliding = Physics2D.OverlapCircle(pos, 0.3f, groundLayer);

    //        if (!isColliding)
    //        {
    //            dotsList[i].GetComponent<SpriteRenderer>().enabled = true;
    //            dotsList[i].position = pos;
    //        }
    //        else
    //        {
    //            dotsList[i].GetComponent<SpriteRenderer>().enabled = false;
    //            //pos.x = (ballPos.x + forceApplied.x * -timeStamp);
    //            //pos.y = (ballPos.y + forceApplied.y * timeStamp) -
    //            //    (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;
    //        }
    //        timeStamp += dotSpacing;
    //    }
    //}

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
