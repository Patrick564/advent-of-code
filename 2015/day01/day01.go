package main

import (
	"fmt"
	"log"
	"os"
)

func main() {
	file, err := os.ReadFile("instructions.txt")
	if err != nil {
		log.Fatal(err)
	}

	count := 0
	basement := make([]int, 0)

	for idx, b := range file {
		if string(b) == "(" {
			count++
			continue
		}

		count--
		if count == -1 {
			basement = append(basement, idx+1)
		}
	}

	fmt.Println(count)
	fmt.Println(basement)
}
