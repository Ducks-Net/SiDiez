$tests = @("AppointmentsController", "CagesController", "EmployeesController", "MedicalRecordsController", "MedicineController", "TreatmentController")

foreach ($test in $tests) {
    dotnet-coverage collect "dotnet test --filter 'FullyQualifiedName~DucksNet.IntegrationTests.$($test)Tests'" -f xml -o "$($test).coverage.xml"
}
dotnet-coverage merge --remove-input-files "DucksNet.IntegrationTests/*.coverage.xml" -f xml -o "coverage.xml"

