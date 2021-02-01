using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NameGenerator : MonoBehaviour
{
    private static string[] Adjectives;
    private static string[] Names;

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
        if (Adjectives == null || Names == null)
        {
            Adjectives = splitLines(Game.Instance.adjectives.text);
            Names = splitLines(Game.Instance.names.text);
        }

        var adjectiveIndex = Random.Range(0, Adjectives.Length);
        var nameIndex = Random.Range(0, Names.Length);
        return Adjectives[adjectiveIndex] + " " + Names[nameIndex];
    }

    private static string[] splitLines(string text)
    {
        return text.Split(
            new[] {"\r\n", "\r", "\n"},
            StringSplitOptions.None
        );
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