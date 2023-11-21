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

func rowVisible(tree int, pos int, row []int) bool {
	left := make([]int, len(row[:pos]))
	right := make([]int, len(row[pos+1:]))

	copy(left, row[:pos])
	copy(right, row[pos+1:])

	slices.Sort(left)
	slices.Sort(right)

	if tree <= left[len(left)-1] && tree <= right[len(right)-1] {
		return false
	}

	return true
}

func colVisible(tree int, pos int, row int, grid [][]int) bool {
	top := make([]int, 0)
	bottom := make([]int, 0)

	for _, g := range grid[:row] {
		top = append(top, g[pos])
	}

	for _, g := range grid[row+1:] {
		bottom = append(bottom, g[pos])
	}

	slices.Sort(top)
	slices.Sort(bottom)

	if tree <= top[len(top)-1] && tree <= bottom[len(bottom)-1] {
		return false
	}

	return true
}

func rowTrees(tree int, pos int, row []int) int {
	left := make([]int, len(row[:pos]))
	right := make([]int, len(row[pos+1:]))

	copy(left, row[:pos])
	copy(right, row[pos+1:])

	result := 1

	for idx, r := range right {
		if r >= tree {
			result *= len(right[:idx+1])
			break
		}

		if idx == len(right)-1 {
			result *= len(right)
		}
	}

	for i := len(left) - 1; i >= 0; i-- {
		if left[i] >= tree {
			result *= len(left[i:])
			break
		}

		if i == 0 {
			result *= len(left)
		}
	}

	return result
}

func colTrees(tree int, pos int, row int, grid [][]int) int {
	top := make([]int, 0)
	bottom := make([]int, 0)

	for _, g := range grid[:row] {
		top = append(top, g[pos])
	}

	for _, g := range grid[row+1:] {
		bottom = append(bottom, g[pos])
	}

	result := 1

	for idx, b := range bottom {
		if b >= tree {
			result *= len(bottom[:idx+1])
			break
		}

		if idx == len(bottom)-1 {
			result *= len(bottom)
		}
	}

	for i := len(top) - 1; i >= 0; i-- {
		if top[i] >= tree {
			result *= len(top[i:])
			break
		}

		if i == 0 {
			result *= len(top)
		}
	}

	return result
}

func main() {
	file, err := os.Open("grid.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	grid := make([][]int, 0)

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := strings.Split(scanner.Text(), "")
		g := make([]int, 0, len(line))

		for _, l := range line {
			i, err := strconv.Atoi(l)
			if err != nil {
				log.Fatal(err)
			}

			g = append(g, i)
		}

		grid = append(grid, g)
	}

	total := len(grid) * len(grid[0])
	borders := (len(grid) * 2) + ((len(grid[0]) - 2) * 2)

	visibles := 0
	scores := make([]int, 0)

	// Iterate grid by row except the edges (first and last)
	for idx, row := range grid {
		if idx == 0 || idx == len(row)-1 {
			continue
		}

		for pos, r := range row {
			if pos == 0 || pos == len(row)-1 {
				continue
			}

			scores = append(scores, rowTrees(r, pos, row)*colTrees(r, pos, idx, grid))
			if rowVisible(r, pos, row) || colVisible(r, pos, idx, grid) {
				visibles++
			}
		}
	}

	slices.Sort(scores)

	fmt.Printf("Total: %d\n", total)
	fmt.Println(borders)
	fmt.Printf("Visibles: %d\n", visibles)
	fmt.Println(borders + visibles)
	fmt.Printf("Score: %+v\n", scores[len(scores)-1])
}
