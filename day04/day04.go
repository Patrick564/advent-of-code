package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	file, err := os.Open("sections.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	pairs := 0
	noPairs := 0
	sections := make([][]int, 0)
	scanner := bufio.NewScanner(file)

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

	freeSections := make([][]int, 0)
	for _, s := range sections {
		first := s[1] - s[0]
		second := s[3] - s[2]

		if first <= second {
			if s[0] >= s[2] && s[1] <= s[3] {
				pairs += 1
				continue
			}
		} else {
			if s[2] >= s[0] && s[3] <= s[1] {
				pairs += 1
				continue
			}
		}

		freeSections = append(freeSections, s)
	}

	for _, s := range freeSections {
		if s[2] > s[1] {
			noPairs += 1
		} else if s[0] > s[3] {
			noPairs += 1
		}
	}

	fmt.Println(pairs)
	fmt.Println(len(sections) - noPairs)
}
