use anyhow::Result;

use crate::utils::read_file;

fn calculate_instructions_product(memory: &[char], pos: usize) -> i32 {
    let mut instruction = String::new();
    let mut product = 1;

    for i in 4..9 {
        if memory[pos + 3 + i] == ')' {
            instruction = memory[pos + 4..pos + 3 + i].iter().collect::<String>();

            break;
        }
    }

    for num in instruction.split(",") {
        match num.parse::<i32>() {
            Ok(n) => product *= n,
            Err(_) => return 0,
        }
    }

    return product;
}

pub fn solution(day: &str, file: &str) -> Result<()> {
    let content = read_file(day, file)?;
    let memory: Vec<char> = content.join("").chars().collect();

    let mut enabled = true;
    let mut multiplications = 0;
    let mut final_multiplications = 0;

    for (i, char) in memory.windows(4).enumerate() {
        if char.iter().collect::<String>() == "mul(" {
            multiplications += calculate_instructions_product(&memory, i);
        }
    }

    println!("Results of the multiplications: {}", multiplications);

    for (i, char) in memory.windows(4).enumerate() {
        let chunk = char.iter().collect::<String>();

        if chunk == "do()" {
            enabled = true;
        } else if chunk == "don'" {
            enabled = false;
        }

        if chunk == "mul(" && enabled {
            final_multiplications += calculate_instructions_product(&memory, i);
        }
    }

    println!(
        "Results of the enabled multiplications: {}",
        final_multiplications
    );

    Ok(())
}
