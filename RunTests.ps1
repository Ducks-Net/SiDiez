$strings = @("AppointmentsController", "CagesController", "EmployeesController", "MedicalRecordsController", "MedicineController", "TreatmentController")

# Loop over the array of strings
foreach ($string in $strings) {
    # Call a command on each string
    # dotnet-coverage collec"t 'dotnet test' -f xml -o 'coverage.xml'
    dotnet-coverage collect "dotnet test --filter 'FullyQualifiedName~DucksNet.IntegrationTests.$(string)'" -f xml -o 'coverage.xml'
    Move-Item tests/DucksNet.IntegrationTests/coverage.xml tests/DucksNet.IntegrationTests/$(string).coverage.xml; \
}
