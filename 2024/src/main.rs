mod day_01;
mod day_02;
mod day_03;
mod day_04;
mod utils;

use std::env;

use anyhow::Result;

fn main() -> Result<()> {
    let args: Vec<String> = env::args().collect();

    match args[1].as_str() {
        "01" => day_01::solution("01", args[2].as_str())?,
        "02" => day_02::solution("02", args[2].as_str())?,
        "03" => day_03::solution("03", args[2].as_str())?,
        "04" => day_04::solution("04", args[2].as_str())?,
        x => println!("Unknown day {} (ദ്ദി ๑>؂•̀๑)", x),
    }

    Ok(())
}
