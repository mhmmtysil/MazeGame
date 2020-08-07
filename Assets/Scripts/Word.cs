using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Timer { get; set; }

    public Word(string name, string desc, float timer = 90f)
    {
        Name = name;
        Description = desc;
        Timer = timer;
    }
}
