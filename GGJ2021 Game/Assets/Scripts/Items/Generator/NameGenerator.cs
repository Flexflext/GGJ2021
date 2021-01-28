using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NameGenerator
{
    private static List<string> Adjectives = readFile("Assets/Text/adjectives.txt");
    private static List<string> Names = readFile("Assets/Text/names.txt");

    private static List<string> readFile(string file)
    {
        var stream = new StreamReader(file);
        var lines = new List<string>();
        while (!stream.EndOfStream)
            lines.Add(stream.ReadLine());
        return lines;
    }

    public static string generateName()
    {
        var adjectiveIndex = Random.Range(0, Adjectives.Count);
        var nameIndex = Random.Range(0, Names.Count);
        return Adjectives[adjectiveIndex] + " " + Names[nameIndex];
    }
}