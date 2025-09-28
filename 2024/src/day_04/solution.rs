use anyhow::Result;

use crate::utils::read_file;

pub fn solution(day: &str, file: &str) -> Result<()> {
    let raw_content = read_file(day, file)?;
    let word_search: Vec<Vec<char>> = raw_content
        .iter()
        .map(|row| row.chars().collect())
        .collect();

    let word_search_reversed: Vec<Vec<char>> = word_search
        .clone()
        .into_iter()
        .map(|mut row| {
            row.reverse();
            row
        })
        .collect();

    println!("{:#?}", word_search);

    Ok(())
}
