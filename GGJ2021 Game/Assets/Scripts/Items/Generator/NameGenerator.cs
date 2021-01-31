using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NameGenerator
{
    private static List<string> Adjectives = readFile("Assets/Resources/Text/adjectives.txt");
    private static List<string> Names = readFile("Assets/Resources/Text/names.txt");

    private static List<string> readFile(string file)
    {
        //var test = Resources.Load(file);
        var stream = new StreamReader(file);
        var lines = new List<string>();
        while (!stream.EndOfStream)
            lines.Add(FirstCharToUpper(stream.ReadLine()));
        return lines;
    }

    public static string GenerateName()
    {
        var adjectiveIndex = Random.Range(0, Adjectives.Count);
        var nameIndex = Random.Range(0, Names.Count);
        return Adjectives[adjectiveIndex] + " " + Names[nameIndex];
    }

    private static string FirstCharToUpper(string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}