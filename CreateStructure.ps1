$folders = @(
    "Models",
    "Models\Reports",
    "Models\Enums",
    "Validation",
    "Calculators",
    "Storage",
    "Pipeline"
)

foreach ($folder in $folders) {
    New-Item -ItemType Directory -Path $folder -Force | Out-Null
}

$files = @(
    "Program.cs",

    "Models\Reports\Report.cs",
    "Models\Reports\DroneReport.cs",
    "Models\Reports\SoldierReport.cs",
    "Models\Reports\RadarReport.cs",
    "Models\Reports\SignalReport.cs",

    "Models\Enums\ReportStatus.cs",
    "Models\Enums\Priority.cs",
    "Models\Enums\Classification.cs",
    "Models\Enums\Language.cs",

    "Validation\IValidator.cs",
    "Validation\ValidationResult.cs",
    "Validation\DroneValidator.cs",
    "Validation\SoldierValidator.cs",
    "Validation\RadarValidator.cs",
    "Validation\SignalValidator.cs",

    "Calculators\ReliabilityCalculator.cs",
    "Calculators\PriorityCalculator.cs",
    "Calculators\ClassificationCalculator.cs",

    "Storage\ReportRepository.cs",
    "Storage\RejectedReportRepository.cs",

    "Pipeline\ReportPipeline.cs"
)

foreach ($file in $files) {
    if (!(Test-Path $file)) {
        New-Item -ItemType File -Path $file | Out-Null
    }
}

Write-Host "Project structure created successfully!"