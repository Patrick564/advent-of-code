package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"

	"golang.org/x/exp/slices"
)

func main() {
	file, err := os.Open("calories.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	sum := 0
	result := make([]int, 0)
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		text := scanner.Text()

		if text == "\n" || text == "" {
			result = append(result, sum)
			sum = 0
			continue
		}

		calorie, err := strconv.Atoi(text)
		if err != nil {
			log.Fatal(err)
		}

		sum = sum + calorie
	}

	slices.Sort(result)

	fmt.Println(result)

	fmt.Println(result[len(result)-1])
	fmt.Println(result[len(result)-1] + result[len(result)-2] + result[len(result)-3])
}
