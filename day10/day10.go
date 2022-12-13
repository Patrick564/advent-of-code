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
	file, err := os.Open("program.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)

	cycle := 1
	strength := 1
	cycles := make(map[int]int, 0)

	for scanner.Scan() {
		line := strings.Split(scanner.Text(), " ")

		if line[0] == "addx" {
			i, _ := strconv.Atoi(line[1])

			cycles[cycle] = strength
			cycles[cycle+1] = strength
			cycles[cycle+2] = strength + i

			strength += i
			cycle += 2
		}

		if line[0] == "noop" {
			cycles[cycle] = strength
			cycle++
		}
	}

	fmt.Println(((cycles[20]) * 20) + ((cycles[60]) * 60) + ((cycles[100]) * 100) + ((cycles[140]) * 140) + ((cycles[180]) * 180) + ((cycles[220]) * 220))

	draw := strings.Builder{}
	orderCycles := make([]int, 0)
	spritePos := make([]int, 3)

	for k := range cycles {
		orderCycles = append(orderCycles, k)
	}
	slices.Sort(orderCycles)

	row := 1

	for _, c := range orderCycles {
		spritePos[0] = cycles[c]
		spritePos[1] = cycles[c] + 1
		spritePos[2] = cycles[c] + 2

		if slices.Contains(spritePos, row) {
			draw.WriteString("#")
		} else {
			draw.WriteString(".")
		}

		if c%40 == 0 && c != 0 {
			row = 1
			continue
		}

		row++
	}

	for idx, s := range draw.String() {
		if idx%40 == 0 {
			fmt.Print("\n")
		}
		fmt.Print(string(s))
	}
	fmt.Print("\n")
}
