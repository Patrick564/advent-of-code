use anyhow::Result;

use crate::utils::read_file;

#[derive(Debug, PartialEq)]
enum ReportStatus {
    Safe,
    Unsafe,
}

fn calculate_reports_status(report: &Vec<i32>) -> ReportStatus {
    let descending = report[0] > report[1];

    for level in report.windows(2) {
        let diff = level[0] - level[1];
        let abs_diff = diff.abs();

        if (diff < 0) == descending {
            return ReportStatus::Unsafe;
        }

        if (abs_diff) > 3 || (abs_diff) < 1 {
            return ReportStatus::Unsafe;
        }
    }

    return ReportStatus::Safe;
}

fn calculate_reports_status_with_dampener(report: &Vec<i32>) -> ReportStatus {
    for i in 0..report.len() {
        let mut test_report = Vec::with_capacity(report.len() - 1);

        test_report.extend_from_slice(&report[..i]);
        test_report.extend_from_slice(&report[i + 1..]);

        if calculate_reports_status(&test_report) == ReportStatus::Safe {
            return ReportStatus::Safe;
        }
    }

    return ReportStatus::Unsafe;
}

pub fn solution(day: &str, file: &str) -> Result<()> {
    let content = read_file(day, file)?;

    let mut reports: Vec<Vec<i32>> = vec![];

    for report in &content {
        reports.push(
            report
                .split(" ")
                .map(|x| x.parse::<i32>().unwrap())
                .collect(),
        );
    }

    let safe_count = reports
        .iter()
        .filter(|report| calculate_reports_status(report) == ReportStatus::Safe)
        .count();

    println!("Safe reports: {:#?}", safe_count);

    let total_safe_count = reports
        .iter()
        .filter(|report| {
            calculate_reports_status(report) == ReportStatus::Safe
                || calculate_reports_status_with_dampener(report) == ReportStatus::Safe
        })
        .count();

    println!("Safe reports with dampener: {:#?}", total_safe_count);

    Ok(())
}
