package main

import (
	"fmt"
	"log"
	"os"
)

var moves = map[string]string{
	"<": ">",
	">": "<",
	"^": "v",
	"v": "^",
}

func main() {
	_, err := os.ReadFile("moves.txt")
	if err != nil {
		log.Fatal(err)
	}

	file := []byte("^>v<")
	count := 0

	for idx, b := range file {
		if idx == len(file)-1 {
			break
		}

		if moves[string(b)] == string(file[idx+1]) && moves[string(b)] != string(file[idx-1]) {
			continue
		}

		count++
	}

	fmt.Println(count + 1)
}
