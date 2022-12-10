$tests = @("AppointmentsController", "CagesController", "EmployeesController", "MedicalRecordsController", "MedicineController", "TreatmentController")

foreach ($test in $tests) {
    dotnet-coverage collect "dotnet test --filter 'FullyQualifiedName~DucksNet.IntegrationTests.$($test)'" -f xml -o 'coverage.xml'
    Move-Item tests/DucksNet.IntegrationTests/coverage.xml tests/DucksNet.IntegrationTests/$($test).coverage.xml; \
}
