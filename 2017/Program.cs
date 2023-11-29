﻿namespace _2017;

internal abstract class _2017
{
    private static void Main(string[] args)
    {
        var day01 = new Day01.Day01();
        var day02 = new Day02.Day02();
        var day03 = new Day03.Day03();
        var day04 = new Day04.Day04();
        var day05 = new Day05.Day05();
        var day06 = new Day06.Day06();
        var day07 = new Day07.Day07();
        var day08 = new Day08.Day08();
        var day09 = new Day09.Day09();
        var day10 = new Day10.Day10();
        var day11 = new Day11.Day11();
        var day12 = new Day12.Day12();
        var day13 = new Day13.Day13();
        
        switch (int.Parse(args[0]))
        {
            case 1:
                day01.Part01();
                day01.Part02();
                break;
            case 2:
                day02.Part01();
                day02.Part02();
                break;
            case 3:
                day03.Part01();
                day03.Part02();
                break;
            case 4:
                day04.Part01();
                day04.Part02();
                break;
            case 5:
                day05.Part01();
                day05.Part02();
                break;
            case 6:
                day06.Part01();
                break;
            case 7:
                day07.Part01();
                day07.Part02();
                break;
            case 8:
                day08.Part01();
                break;
            case 9:
                day09.Part01();
                day09.Part02();
                break;
            case 10:
                day10.Part01();
                day10.Part02();
                break;
            case 11:
                day11.Part01();
                break;
            case 12:
                day12.Part01();
                break;
            case 13:
                day13.Part01();
                day13.Part02();
                break;
            case 14:
                break;
            case 15:
                break;
        }
    }
}
