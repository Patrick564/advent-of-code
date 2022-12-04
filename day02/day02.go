package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"
)

var (
	values = map[string]int{
		"A": 1, // Rock
		"B": 2, // Paper
		"C": 3, // Scissors

		"X": 1, // Rock
		"Y": 2, // Paper
		"Z": 3, // Scissors
	}
	scheme = map[string][]string{
		"A": {"Z", "X", "C", "B"}, // A: Win, Draw | Win, Lose
		"C": {"Y", "Z", "B", "A"}, // C: Win, Draw | Win, Lose
		"B": {"X", "Y", "A", "C"}, // B: Win, Draw | Win, Lose
	}
)

// Calculate the score with the rules of first part of challenge
func firstPart(round []string) int {
	// You lose
	if scheme[round[0]][0] == round[1] {
		return 0 + values[round[1]]
	}

	// You draw
	if scheme[round[0]][1] == round[1] {
		return 3 + values[round[1]]
	}

	// You win
	return 6 + values[round[1]]
}

// Calculate the score with the rules of second part of challenge
func secondPart(round []string) int {
	// You lose
	if round[1] == "X" {
		return 0 + values[scheme[round[0]][2]]
	}

	// You draw
	if round[1] == "Y" {
		return 3 + values[scheme[round[0]][1]]
	}

	// You win
	return 6 + values[scheme[round[0]][3]]
}

func main() {
	file, err := os.Open("strategy.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	points := 0
	secondPoints := 0
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		round := strings.Split(scanner.Text(), " ")

		points += firstPart(round)
		secondPoints += secondPart(round)
	}

	fmt.Println(points)
	fmt.Println(secondPoints)
}
