using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBeat : MonoBehaviour
{
    private static readonly Vector3 ScaleSize = new Vector3(0.25f, 0.25f, 0.25f);

    [SerializeField]
    private SpriteRenderer ArrowSprite;
    private RhythmManager Manager;

    public bool HasPressed = false;
    private int Lifetime = 5;

    public void PressBeat()
    {
        HasPressed = true;
    }

    private void Update()
    {
        if (HasPressed)
        {
            Lifetime--;
            ArrowSprite.color = Color.Lerp(ArrowSprite.color, Color.black, 0.2f);
            transform.localScale = Vector3.Lerp(transform.localScale, ScaleSize, 0.2f);
            if (Lifetime == 0)
            {
                Destroy(gameObject);
            }
        } else
        {
            transform.position += Vector3.up * Manager.NoteSpeed * Time.deltaTime;
        }
    }

    public void SetRhythmManager(RhythmManager manager)
    {
        Manager = manager;
    }
}
