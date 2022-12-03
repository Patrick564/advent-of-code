package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"

	"golang.org/x/exp/slices"
)

func main() {
	file, err := os.Open("rucksacks.txt")
	if err != nil {
		log.Fatal(err)
	}

	rucksacks := make([][]string, 0)
	alphabet := strings.Split("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "")
	scanner := bufio.NewScanner(file)
	priority := 0
	badgePriority := 0

	for scanner.Scan() {
		rucksack := scanner.Text()
		compartments := [][]string{strings.Split(rucksack[:(len(rucksack)/2)], ""), strings.Split(rucksack[len(rucksack)/2:], "")}

		rucksacks = append(rucksacks, strings.Split(rucksack, ""))

		for _, c := range compartments[0] {
			if slices.Contains(compartments[1], c) {
				priority = priority + slices.Index(alphabet, c) + 1
				break
			}
		}
	}

	for i := 0; i < len(rucksacks); {
		for _, s := range rucksacks[i] {
			if slices.Contains(rucksacks[i+1], s) && slices.Contains(rucksacks[i+2], s) {
				badgePriority = badgePriority + (slices.Index(alphabet, s) + 1)
				break
			}
		}

		if i == len(rucksacks)-3 {
			break
		}

		i = i + 3
	}

	fmt.Println(priority)
	fmt.Println(badgePriority)
}
