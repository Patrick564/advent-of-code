package main

import (
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"

	"golang.org/x/exp/slices"
)

func sumFileSizes(dir []string, dirs [][]string) int {
	count := 0

	for _, d := range dir[1:] {
		i, err := strconv.Atoi(d)
		if err != nil {
			for idx, f := range dirs {
				if d == f[0] {
					count += sumFileSizes(f, dirs[idx+1:])
					break
				}
			}
			continue
		}

		count += i
	}

	return count
}

// type directory struct {
// 	name    string
// 	cat     string
// 	size    int
// 	content []string
// }

// type filesystem []*directory

func main() {
	file, err := os.ReadFile("output.txt")
	if err != nil {
		log.Fatal(err)
	}

	commands := strings.Split(string(file), "\n")

	dir := make([]string, 0)
	dirs := make([][]string, 0)

	depth := 0

	// di := new(directory)
	// fs := make(filesystem, 0)
	// // lastDir := ""

	// for _, c := range commands {
	// 	line := strings.Split(c, " ")

	// 	if line[0] == "$" && line[1] == "cd" {
	// 		if line[2] != ".." {
	// 			fs = append(fs, &directory{})
	// 			// lastDir = line[2]
	// 			continue
	// 		}
	// 	}

	// 	if line[0] == "dir" {
	// 		// fs[lastDir].content = append(fs[lastDir].content, line[1])

	// 	}

	// 	strconv.Atoi(line[0])
	// }

	// depth = 0

	for idx, c := range commands {
		line := strings.Split(c, " ")

		if line[0] == "$" {
			if line[1] == "ls" {
				continue
			}

			if line[1] == "cd" {
				if line[2] == ".." {
					depth--
					continue
				}

				if len(dir) > 0 {
					dirs = append(dirs, dir)
					dir = make([]string, 0)
				}

				dir = append(dir, line[2])
				depth++
				continue
			}
		}

		if line[0] == "dir" {
			dir = append(dir, line[1])
			continue
		}

		dir = slices.Insert(dir, 1, line[0])

		if idx == len(commands)-1 {
			dirs = append(dirs, dir)
			break
		}
	}

	result := make([]int, 0)

	for idx, d := range dirs {
		result = append(result, sumFileSizes(d, dirs[idx+1:]))
	}

	count := 0

	for _, r := range result {
		if r <= 100000 {
			count += r
		}
	}

	res := 0

	for _, d := range dirs {
		for _, f := range d {
			i, err := strconv.Atoi(f)
			if err != nil {
				continue
			}

			res += i
		}
	}

	unusedSpaceDisk := 70000000 - res
	sol := 0

	slices.Sort(result)
	slices.SortFunc(result, func(a, b int) bool {
		return a > b
	})

	for idx, r := range result {
		if unusedSpaceDisk+r < 30000000 {
			sol += result[idx-1]
			break
		}
	}

	fmt.Println(result)
	fmt.Println(count)

	fmt.Println(res)
	fmt.Println(unusedSpaceDisk)
	fmt.Println(sol)

	// fmt.Println("----")

	// for k, v := range fs {
	// 	fmt.Println(k, v)
	// }
}
