use std::iter::zip;

use anyhow::{Ok, Result};

use crate::utils::read_file;

#[derive(Debug)]
struct LocationID {
    left: i32,
    right: i32,
}

impl LocationID {
    fn new(left: i32, right: i32) -> Self {
        return LocationID { left, right };
    }

    fn distance(&self) -> i32 {
        return (self.left - self.right).abs();
    }

    fn similarity_score(self) -> i32 {
        return self.left * self.right;
    }
}

fn part_01(left_pairs: &Vec<i32>, right_pairs: &Vec<i32>) {
    let mut locations: Vec<LocationID> = Vec::new();

    for pair in zip(left_pairs.clone(), right_pairs.clone()) {
        locations.push(LocationID::new(pair.0, pair.1));
    }

    let result = locations.iter().clone().map(|x| x.distance()).sum::<i32>();

    println!("Total distances between lists:{:#?}", result);
}

fn part_02(left_pairs: &Vec<i32>, right_pairs: &Vec<i32>) {
    let mut locations: Vec<LocationID> = Vec::new();

    for num in left_pairs {
        let pairs: i32 = right_pairs.iter().filter(|x| x == &num).count() as i32;

        locations.push(LocationID::new(*num, pairs));
    }

    println!(
        "Similarity score is: {:#?}",
        locations
            .into_iter()
            .map(|x| x.similarity_score())
            .sum::<i32>()
    )
}

pub fn solution(day: &str, file: &str) -> Result<()> {
    let content = read_file(day, file)?;

    let mut left_pairs: Vec<i32> = Vec::new();
    let mut right_pairs: Vec<i32> = Vec::new();

    for line in &content {
        let values: Vec<i32> = line
            .split("   ")
            .map(|x| x.parse::<i32>().unwrap())
            .collect();

        left_pairs.push(values[0]);
        right_pairs.push(values[1]);
    }

    left_pairs.sort();
    right_pairs.sort();

    part_01(&left_pairs, &right_pairs);
    part_02(&left_pairs, &right_pairs);

    Ok(())
}
