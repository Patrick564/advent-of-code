package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"

	"golang.org/x/exp/slices"
)

type stacks map[int][]string

type move struct {
	Crates int
	From   int
	To     int
}

func createStacks(content [][]string) stacks {
	stks := make(stacks, 0)

	for _, s := range content[0] {
		i, _ := strconv.Atoi(s)

		for _, c := range content[1:] {
			if len(c) < i || c[i-1] == "-" {
				continue
			}

			stks[i] = append(stks[i], c[i-1])
		}
	}

	return stks
}

func makeCopy(s stacks) stacks {
	copyStack := make(stacks, len(s))

	for k, v := range s {
		copyStack[k] = make([]string, len(v))
		copy(copyStack[k], v)
	}

	return copyStack
}

func firstPart(s stacks, moves []move) {
	for _, m := range moves {
		for i := 1; i <= m.Crates; i++ {
			s[m.To] = append(s[m.To], s[m.From][len(s[m.From])-1])
			s[m.From] = s[m.From][:len(s[m.From])-1]
		}
	}
}

func secondPart(s stacks, moves []move) {
	for _, m := range moves {
		s[m.To] = append(s[m.To], s[m.From][len(s[m.From])-m.Crates:]...)
		s[m.From] = s[m.From][:len(s[m.From])-m.Crates]
	}
}

func main() {
	file, err := os.Open("crates.txt")
	if err != nil {
		log.Fatal(err)
	}

	moves := make([]move, 0)
	rawStacks := make([][]string, 0)
	replacer := strings.NewReplacer("    ", "-", "[", "", "]", "", " ", "")

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		if line == "" {
			continue
		}

		if string(line[0]) == " " || string(line[0]) == "[" {
			crates := strings.Split(replacer.Replace(line), "")
			rawStacks = slices.Insert(rawStacks, 0, crates)
			continue
		}

		instruction := strings.Split(line, " ")
		m := make([]int, 0, 3)

		for _, i := range instruction {
			c, err := strconv.Atoi(i)
			if err != nil {
				continue
			}

			m = append(m, c)
		}

		moves = append(moves, move{Crates: m[0], From: m[1], To: m[2]})
	}

	stacks := createStacks(rawStacks)
	copyStacks := makeCopy(stacks)

	firstPart(stacks, moves)
	secondPart(copyStacks, moves)

	fmt.Printf("First rearrangement: %+v\n", stacks)
	fmt.Printf("Second rearrangement: %+v\n", copyStacks)
}
