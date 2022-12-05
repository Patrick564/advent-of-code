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

func main() {
	file, err := os.Open("crates.txt")
	if err != nil {
		log.Fatal(err)
	}

	stacks := make(map[int][]string, 9)
	copyStacks := make(map[int][]string, len(stacks))
	moves := make([][]int, 0)

	scanner := bufio.NewScanner(file)
	replacer := strings.NewReplacer("    ", "-", "[", "", "]", "", " ", "")

	for scanner.Scan() {
		line := scanner.Text()

		if line == "" {
			continue
		}

		if string(line[0]) == " " || string(line[0]) == "[" {
			crates := strings.Split(replacer.Replace(line), "")
			if len(crates) < 9 {
				crates = append(crates, "-")
			}
			if crates[0] == "1" {
				continue
			}

			for idx, c := range crates {
				if c == "1" {
					break
				}
				if c == "-" {
					continue
				}

				stacks[idx+1] = slices.Insert(stacks[idx+1], 0, c)
				copyStacks[idx+1] = slices.Insert(copyStacks[idx+1], 0, c)
			}

			continue
		}

		instruction := strings.Split(line, " ")
		move := make([]int, 0)

		for idx, i := range instruction {
			if idx%2 != 0 {
				c, err := strconv.Atoi(i)
				if err != nil {
					log.Fatal(err)
				}

				move = append(move, c)
			}
		}

		moves = append(moves, move)
	}

	for _, m := range moves {
		for i := 1; i <= m[0]; i++ {
			stacks[m[2]] = append(stacks[m[2]], stacks[m[1]][len(stacks[m[1]])-1])
			stacks[m[1]] = stacks[m[1]][:len(stacks[m[1]])-1]
		}
	}

	for _, m := range moves {
		copyStacks[m[2]] = append(copyStacks[m[2]], copyStacks[m[1]][len(copyStacks[m[1]])-m[0]:]...)
		copyStacks[m[1]] = copyStacks[m[1]][:len(copyStacks[m[1]])-m[0]]
	}

	fmt.Println(stacks)
	fmt.Println(copyStacks)
}
