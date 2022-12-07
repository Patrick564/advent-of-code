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

type box struct {
	length int
	width  int
	height int
}

func (b box) area() int {
	return b.length * b.width * b.height
}

func (b box) surfaces() []int {
	l := []int{
		b.length * b.width,
		b.width * b.height,
		b.height * b.length,
	}

	return l
}

func (b box) smallestSurface() int {
	s := b.surfaces()
	slices.Sort(s)

	return s[0]
}

func (b box) paperArea() int {
	s := b.surfaces()

	return (2 * s[0]) + (2 * s[1]) + (2 * s[2])
}

func (b box) ribbonArea() int {
	s := []int{b.length, b.width, b.height}
	slices.Sort(s)

	return (s[0] * 2) + (s[1] * 2) + b.area()
}

func main() {
	file, err := os.Open("presents.txt")
	if err != nil {
		log.Fatal(err)
	}

	count := 0
	ribbonCount := 0

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		values := strings.Split(line, "x")

		l, _ := strconv.Atoi(values[0])
		w, _ := strconv.Atoi(values[1])
		h, _ := strconv.Atoi(values[2])

		b := box{length: l, width: w, height: h}

		count += b.paperArea() + b.smallestSurface()
		ribbonCount += b.ribbonArea()
	}

	fmt.Println(count)
	fmt.Println(ribbonCount)
}
