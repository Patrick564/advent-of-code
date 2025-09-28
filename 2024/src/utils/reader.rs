use std::{fs, path::Path};

use anyhow::Result;

#[allow(dead_code)]
#[derive(Debug)]
enum FileType {
    Test,
    Input,
}

pub fn read_file(day: &str, file: &str) -> Result<Vec<String>> {
    let raw_path = format!("./src/day_{}/{}.txt", day, file);
    let file_path = Path::new(&raw_path);
    let raw_content = fs::read_to_string(file_path)?;
    let content: Vec<String> = raw_content.lines().map(String::from).collect();

    Ok(content)
}
