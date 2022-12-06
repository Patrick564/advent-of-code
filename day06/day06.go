package main

import (
	"fmt"
	"log"
	"os"

	"golang.org/x/exp/slices"
)

func findSubstring(content []byte, chars int) ([]byte, int) {
	packetMarker := make([]byte, 0, chars)

	for idx, b := range content {
		if len(packetMarker) == chars {
			return packetMarker, idx
		}

		i := slices.Index(packetMarker, b)
		if i == -1 {
			packetMarker = append(packetMarker, b)
			continue
		}

		packetMarker = append(packetMarker[i+1:], b)
	}

	return []byte{}, 0
}

func main() {
	file, err := os.ReadFile("signal.txt")
	if err != nil {
		log.Fatal(err)
	}

	packet, packetPos := findSubstring(file, 4)
	message, messagePos := findSubstring(file, 14)

	fmt.Println(string(packet))
	fmt.Println(packetPos)

	fmt.Println(string(message))
	fmt.Println(messagePos)
}
