using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatButton : MonoBehaviour
{
    [SerializeField]
    public string Button;

    private Color StandardColour;
    private Color ActivatedColour;
    [SerializeField]
    private SpriteRenderer OuterImage;
    [SerializeField]
    private SpriteRenderer InnerImage;
    [SerializeField]
    private RhythmManager AssignedManager;

    private int HoldBuffer = 0;
    private const int HoldBufferValue = 3;
    private bool Pressed = false;

    private List<ArrowBeat> CurrentBeats = new List<ArrowBeat>();

    // Start is called before the first frame update
    void Start()
    {
        StandardColour = OuterImage.color;
        ActivatedColour = Color.Lerp(StandardColour, Color.black, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        if (HoldBuffer > 0) {
            HoldBuffer--;
            if (CurrentBeats.Count > 0)
            {
                var arrow = CurrentBeats[0];
                int timing = (int)(100 - Mathf.Abs(transform.position.y - Mathf.Round(arrow.transform.position.y)));

                int score;
                if (timing < 30)
                {
                    score = 0;
                }
                else if (timing < 50)
                {
                    score = 50;
                }
                else if (timing < 70)
                {
                    score = 100;
                }
                else if (timing < 90)
                {
                    score = 500;
                }
                else
                {
                    score = 1000;
                }

                arrow.transform.position = Vector3.Lerp(arrow.transform.position, transform.position, 0.3f);
                AssignedManager.AddScore(score);
                CurrentBeats.RemoveAt(0);
                arrow.PressBeat();
            }
        }

        if (Input.GetButtonDown(Button) && !Pressed)
        {
            OuterImage.color = ActivatedColour;
            InnerImage.color = Color.Lerp(StandardColour, Color.black, 0.1f);
            Pressed = true;

            HoldBuffer = HoldBufferValue;
        }
        if (Input.GetButtonUp(Button) && Pressed)
        {
            OuterImage.color = StandardColour;
            InnerImage.color = Color.white;
            Pressed = false;

            HoldBuffer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ArrowBeat beat = collision.GetComponent<ArrowBeat>();
        if (!beat.HasPressed)
        {
            CurrentBeats.Add(beat);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ArrowBeat beat = collision.GetComponent<ArrowBeat>();
        if (CurrentBeats.Contains(beat))
        {
            CurrentBeats.Remove(collision.GetComponent<ArrowBeat>());
            Destroy(collision.gameObject);
        }
    }
}
