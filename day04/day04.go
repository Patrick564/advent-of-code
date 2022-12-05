package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func firstPart(content [][]int) ([][]int, int) {
	count := 0
	sections := make([][]int, 0)

	for _, s := range content {
		if (s[0] >= s[2] && s[1] <= s[3]) || (s[2] >= s[0] && s[3] <= s[1]) {
			count++
			continue
		}

		sections = append(sections, s)
	}

	return sections, count
}

func secondPart(content [][]int) int {
	count := 0

	for _, s := range content {
		if (s[2] > s[1]) || (s[0] > s[3]) {
			count += 1
		}
	}

	return count
}

func main() {
	file, err := os.Open("sections.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	sections := make([][]int, 0)
	scanner := bufio.NewScanner(file)

	// Replace the comma to '-' to get and split [0, 2, 3, 6] and convert every to int
	for scanner.Scan() {
		line := strings.Split(strings.Replace(scanner.Text(), ",", "-", 1), "-")
		section := make([]int, 0, 4)

		for _, l := range line {
			s, err := strconv.Atoi(l)
			if err != nil {
				log.Fatal(err)
			}

			section = append(section, s)
		}

		sections = append(sections, section)
	}

	noOverlapSections, countFullOverlap := firstPart(sections)
	countNoOverlap := secondPart(noOverlapSections)

	fmt.Printf("The count for full overlap sections is: %d\n", countFullOverlap)
	fmt.Printf("The count for full and partial overlap sections is: %d\n", len(sections)-countNoOverlap)
}
