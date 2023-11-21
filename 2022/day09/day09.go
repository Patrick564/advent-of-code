package main

import (
	"bufio"
	"fmt"
	"log"
	"math"
	"os"
	"strconv"
	"strings"
)

type coordinate struct {
	X, Y int
}

type board map[string]bool

type coordinates struct {
	x, y  int
	size  int
	tails []*coordinate
}

func (c *coordinates) move(direction string) {
	switch direction {
	case "L":
		c.x--
	case "R":
		c.x++
	case "U":
		c.y++
	case "D":
		c.y--
	}

	copy(c.tails, ropePosition(c.x, c.y, c.tails))
}

func (c *coordinates) String() string {
	b := strings.Builder{}
	b.WriteString(strconv.Itoa(c.tails[c.size-1].X))
	b.WriteString(":")
	b.WriteString(strconv.Itoa(c.tails[c.size-1].Y))

	return b.String()
}

func ropePosition(main, sec int, tails []*coordinate) []*coordinate {
	tails[0].X = main
	tails[0].Y = sec

	for idx, t := range tails[1:] {
		a := float64(t.X) - float64(tails[idx].X)
		b := float64(t.Y) - float64(tails[idx].Y)

		if math.Sqrt((a*a)+(b*b)) <= math.Sqrt(2) {
			continue
		}

		if tails[idx].X != t.X && tails[idx].Y != t.Y {
			if tails[idx].X-t.X < 0 {
				t.X--
			} else {
				t.X++
			}

			if tails[idx].Y-t.Y > 0 {
				t.Y++
			} else {
				t.Y--
			}

			continue
		}

		if tails[idx].X == t.X {
			if tails[idx].Y-t.Y > 0 {
				t.Y++
			} else {
				t.Y--
			}
			continue
		}

		if tails[idx].Y == t.Y {
			if tails[idx].X-t.X < 0 {
				t.X--
			} else {
				t.X++
			}
			continue
		}
	}

	return tails
}

func firstPart() (board, coordinates) {
	b := make(board, 0)
	t := make([]*coordinate, 2)
	c := coordinates{x: 0, y: 0, size: 2, tails: t}

	for i := range t {
		t[i] = new(coordinate)
	}

	return b, c
}

func secondPart() (board, coordinates) {
	b := make(board, 0)
	t := make([]*coordinate, 10)
	c := coordinates{x: 0, y: 0, size: 10, tails: t}

	for i := range t {
		t[i] = new(coordinate)
	}

	return b, c
}

func main() {
	file, err := os.Open("motions.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)

	b1, c1 := firstPart()
	b2, c2 := secondPart()

	for scanner.Scan() {
		move := strings.Split(scanner.Text(), " ")
		step, err := strconv.Atoi(move[1])
		if err != nil {
			log.Fatal(err)
		}

		for i := step; i > 0; i-- {
			// First past
			c1.move(move[0])
			b1[c1.String()] = true

			// Second past
			c2.move(move[0])
			b2[c2.String()] = true
		}
	}

	fmt.Println(len(b1))
	fmt.Println(len(b2))
}
