using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Word(string name, string desc)
    {
        Name = name;
        Description = desc;
    }
}
