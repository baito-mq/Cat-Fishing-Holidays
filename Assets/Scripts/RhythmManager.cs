using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    [SerializeField]
    private Transform LeftBeat;
    [SerializeField]
    private Transform UpBeat;
    [SerializeField]
    private Transform DownBeat;
    [SerializeField]
    private Transform RightBeat;
    [SerializeField]
    private GameObject LeftArrow;
    [SerializeField]
    private GameObject UpArrow;
    [SerializeField]
    private Transform ArrowSpawns;
    [SerializeField]
    private AudioSource Music;
    [SerializeField]
    private TextMesh ScoreText;

    [TextArea]
    public string TestInput;

    private int Score = 0;
    public float BPM { get; private set; } = 120f;
    public float NoteSpeed = 800f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        string[] args = TestInput.Split(';');
        foreach (string i in args)
        {
            if (string.IsNullOrWhiteSpace(i)) continue;

            string[] values = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] timespan = Regex.Split(values[0], "[:.]");

            int minutes;
            int seconds = 0;
            int milliseconds = 0;

            Direction pos = Direction.Left;

            int.TryParse(timespan[0], out minutes);
            if (timespan.Length > 1)
            {
                int.TryParse(timespan[1], out seconds);
            }
            if (timespan.Length > 2)
            {
                int.TryParse(timespan[2], out milliseconds);
            }

            float totalSeconds = seconds + (minutes * 60f) + (milliseconds / 1000f);
            Enum.TryParse(values[1], true, out pos);

            SpawnArrow(pos, 0 - NoteSpeed * totalSeconds);
        }
    }

    private void SpawnArrow(Direction direction, float position)
    {
        GameObject arrow = Instantiate(direction == Direction.Up || direction == Direction.Down ? UpArrow : LeftArrow, ArrowSpawns);
        switch (direction)
        {
            case Direction.Up:
                arrow.transform.localScale = UpBeat.transform.localScale * 0.9f;
                arrow.transform.position = new Vector3(UpBeat.transform.position.x, position, UpBeat.position.z);
                break;
            case Direction.Down:
                arrow.transform.localScale = UpBeat.transform.localScale * 0.9f;
                arrow.transform.position = new Vector3(DownBeat.transform.position.x, position, DownBeat.position.z);
                break;
            case Direction.Left:
                arrow.transform.localScale = LeftBeat.transform.localScale * 0.9f;
                arrow.transform.position = new Vector3(LeftBeat.transform.position.x, position, LeftBeat.position.z);
                break;
            case Direction.Right:
                arrow.transform.localScale = LeftBeat.transform.localScale * 0.9f;
                arrow.transform.position = new Vector3(RightBeat.transform.position.x, position, RightBeat.position.z);
                break;
        }

        if (direction == Direction.Right || direction == Direction.Down)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        arrow.GetComponent<ArrowBeat>().SetRhythmManager(this);
    }

    private enum Direction
    {
        Up, Down, Left, Right
    }

    public void AddScore(int score)
    {
        Score += score;
        ScoreText.text = $"Score: {Score}";
    }

    public void PlayClick()
    {
    }
    public void SpawnRandom(){
        Array dirs=Enum.GetValues(typeof(Direction));
        for(int i=0;i<20;i++){
            float position=0-(NoteSpeed+NoteSpeed*i);
            SpawnArrow( (Direction)dirs.GetValue(UnityEngine.Random.Range(0,4)),position);
        }
    }
}
