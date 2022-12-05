package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"
)

// Priority values is index + 1
var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

// Get values of first part of challenge
func firstPart(content string) int {
	compartments := []string{content[:len(content)/2], content[len(content)/2:]}

	for _, r := range compartments[0] {
		if strings.ContainsRune(compartments[1], r) {
			return strings.IndexRune(alphabet, r) + 1
		}
	}

	return 0
}

// Get values of second part of challenge
func secondPart(first, second, third string) int {
	for _, r := range first {
		if strings.ContainsRune(second, r) && strings.ContainsRune(third, r) {
			return strings.IndexRune(alphabet, r) + 1
		}
	}

	return 0
}

func main() {
	file, err := os.Open("rucksacks.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	priority := 0
	badgePriority := 0

	rucksacks := make([]string, 0)
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		rucksack := scanner.Text()

		priority += firstPart(rucksack)
		rucksacks = append(rucksacks, rucksack)
	}

	// Iterate between all rucksacks and jump every 3 index
	for i := 0; i < len(rucksacks); i += 3 {
		badgePriority += secondPart(rucksacks[i], rucksacks[i+1], rucksacks[i+2])

		if i == len(rucksacks)-3 {
			break
		}
	}

	fmt.Printf("The sum of supply what appears in both compartments is: %d\n", priority)
	fmt.Printf("The sum of priority supply in three group Elves is: %d\n", badgePriority)
}
